using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class MarketService: IMarketService
	{
		private readonly IMarketRepository _marketRepository;
		private readonly IInventoryRepository _inventoryRepository;

		public MarketService(IMarketRepository marketRepository, IInventoryRepository inventoryRepository)
		{
			_marketRepository = marketRepository;
			_inventoryRepository = inventoryRepository;
		}

		public Market? GetById(int id)
		{
			return _marketRepository.GetById(id);
		}

		public IEnumerable<Market>? GetMarketHistoryByUserId(int userId)
		{
			return _marketRepository.GetMarketHistoryByUserId(userId);
		}

		public IEnumerable<Market>? GetOpenListingsByItemName(string itemName)
		{
			return _marketRepository.GetOpenListingsByItemName(itemName);
		}

		public IEnumerable<Market>? GetRecentOpenListings()
		{
			return _marketRepository.GetRecentOpenListings();
		}

		public Market? CreateListing(int userId, int itemId, int quantity, int price)
		{
			// Check items are available
			int ownedItems = _inventoryRepository.GetItemQuantityOfUser(userId, itemId);
			if (ownedItems < quantity)
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
				Quantity = ownedItems - quantity
			};
			bool update = _inventoryRepository.UpdateQuantity(inventory);
			if (!update)
			{
				// TODO: Revert listing
				return null;
			}

			return listing;
		}
	}
}
