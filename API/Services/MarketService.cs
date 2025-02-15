﻿using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class MarketService: IMarketService
	{
		private readonly IMarketRepository _marketRepository;
		private readonly IInventoryRepository _inventoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

		public MarketService(IMarketRepository marketRepository, IInventoryRepository inventoryRepository, IUserRepository userRepository, IItemRepository itemRepository)
		{
			_marketRepository = marketRepository;
			_inventoryRepository = inventoryRepository;
			_itemRepository = itemRepository;
			_userRepository = userRepository;
		}

		public Market? GetById(int id)
		{
			return _marketRepository.GetById(id);
		}

		public IEnumerable<Market>? GetMarketHistoryByUserId(int userId)
		{
			return _marketRepository.GetMarketHistoryByUserId(userId);
		}

		public IEnumerable<Market>? GetOpenListingsByItemId(int itemId)
		{
			return _marketRepository.GetOpenListingsByItemId(itemId);
		}

		public IEnumerable<Market>? GetRecentOpenListings()
		{
			return _marketRepository.GetRecentOpenListings();
		}

		public Market? CreateListing(int userId, int itemId, int quantity, int price)
		{
			// Check items are available
			Inventory? item = _inventoryRepository.GetUserItem(userId, itemId);
			if (item == null || item.Quantity < quantity)
			{
				return null;
			}

			// Add market listing
			Market market = new Market
			{
				IdItem = itemId,
				Quantity = quantity,
				Price = price,
				IdSeller = userId,
				IsSold = false
			};
			Market? listing = _marketRepository.Insert(market).Result;
			if (listing == null)
			{
				return null;
			}

			// Remove items from inventory
			var inventory = new Inventory
			{
				IdItem = itemId,
				IdUser = userId,
				Quantity = item.Quantity - quantity
			};
			bool update = _inventoryRepository.UpdateQuantity(inventory);
			if (!update)
			{
				// TODO: Revert listing
				return null;
			}

			return listing;
		}

		public async Task<bool> UserBuyListing(int buyerId, int marketId)
		{ 
			// Get listing
			Market? listing = _marketRepository.GetById(marketId);
			if (listing == null)
			{
				return false;
			}

			// Transfer money from buyer to seller
			bool moneyTransfered = _userRepository.TransferUserMoney(buyerId, listing.IdSeller, listing.Price);
			if (!moneyTransfered)
			{
				return false;
			}

			// Add item in inventory (create if not present)
			Inventory? item = _inventoryRepository.GetUserItem(buyerId, listing.IdItem);
			if (item == null)
			{
				var newItem = new Inventory
				{
					IdItem = listing.IdItem,
					IdUser = buyerId,
					Quantity = listing.Quantity
				};
				await _inventoryRepository.Insert(newItem);
			}
			else
			{
				item.Quantity += listing.Quantity;
				_inventoryRepository.UpdateQuantity(item);
			}

			// Set listing to sold
			_marketRepository.RemoveListing(marketId);

			return true;
		}

        public ItemAveragePrice? GetAveragePriceByItemId(int itemId)
        {
            var listings = _marketRepository.GetOpenListingsByItemId(itemId);
			var item = _itemRepository.GetById(itemId);

			if (listings == null || item == null)
            {
                return null;
            }
            
            int totalPrice = 0;
			int numberOfItems = 0;
            foreach (var listing in listings)
            {
                totalPrice += listing.Price * listing.Quantity;
				numberOfItems += listing.Quantity;
            }
            var averagePrice = numberOfItems <= 0 ? 0 : totalPrice / numberOfItems;

            return new ItemAveragePrice() { Id = itemId, Name = item.Name, AveragePrice = averagePrice };
        }
    }
}
