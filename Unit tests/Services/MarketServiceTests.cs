using API.DataAccess;
using API.Dbo;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Unit_tests.Services
{
	public class MarketServiceTests
	{
		List<User> users = new List<User>
		{
			new User
			{
				Id = 1,
				Name = "XxGamerxX",
				Password = "azertyuiop",
				Money = 4242
			},
			new User
			{
				Id = 2,
				Name = "Legwarmer6486",
				Password = "3YPRgph3e@VNzzxX&c",
				Money = 0
			},
			new User
			{
				Id = 3,
				Name = "Winner4604",
				Password = "mVWbUp%oA!#f4UTda4",
				Money = 9000
			}
		};
		List<Inventory> inventory = new List<Inventory>
		{
			new Inventory
			{
				IdUser = 1,
				IdItem = 1,
				Quantity = 1
			},
			new Inventory
			{
				IdUser = 1,
				IdItem = 2,
				Quantity = 10
			}
		};

		List<Item> items = new List<Item>
		{
			new Item
			{
				Id = 1,
				Name = "Sword",
				Description = "Use to cut things, usually people"
			},
			new Item
			{
				Id = 2,
				Name = "Pen",
				Description = "Use it to write or draw"
			}
		};
		List<Market> market = new List<Market>
		{
			new Market
			{
				Id = 1,
				IdSeller = 1,
				Price = 4,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			},
			new Market
			{
				Id = 2,
				IdSeller = 2,
				Price = 10,
				IdItem = 1,
				Quantity = 1,
				IsSold = true
			},
			new Market
			{
				Id = 3,
				IdSeller = 2,
				Price = 10,
				IdItem = 2,
				Quantity = 5,
				IsSold = false
			},
			new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 10,
				IdItem = 1,
				Quantity = 1000,
				IsSold = false
			}
		};

		[Fact]
		public void GetById_Success()
		{
			var listing = market[0];
			var mock = Mock.Of<IMarketRepository>(r => r.GetById(listing.Id) == listing);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetById(listing.Id);

			Assert.NotNull(result);
			Assert.Equal(listing, result);
		}

		[Fact]
		public void GetById_NotFound()
		{
			var id = 42;
			var mock = Mock.Of<IMarketRepository>(r => r.GetById(id) == null);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetById(id);

			Assert.Null(result);
		}

		[Fact]
		public void GetMarketHistoryByUserId_Success()
		{
			var userId = 1;
			var listings = market.Where(listing => listing.IdSeller == userId);
			var mock = Mock.Of<IMarketRepository>(r => r.GetMarketHistoryByUserId(userId) == listings);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetMarketHistoryByUserId(userId);

			Assert.NotNull(result);
			Assert.Equal(listings, result);
		}

		[Fact]
		public void GetMarketHistoryByUserId_NotFound()
		{
			var userId = 42;
			var mock = Mock.Of<IMarketRepository>(r => r.GetMarketHistoryByUserId(userId) == null);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetMarketHistoryByUserId(userId);

			Assert.Null(result);
		}


		[Fact]
		public void GetOpenListingsByItemId_Success()
		{
			var itemId = 1;
			var listings = market.Where(listing => listing.IdItem == itemId && !listing.IsSold);
			var mock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == listings);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetOpenListingsByItemId(itemId);

			Assert.NotNull(result);
			Assert.Equal(listings, result);
		}

		[Fact]
		public void GetOpenListingsByItemId_NotFound()
		{
			var itemId = 42;
			var mock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == null);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetOpenListingsByItemId(itemId);

			Assert.Null(result);
		}

		[Fact]
		public void GetRecentOpenListings_Success()
		{
			var listings = market.Where(listing => !listing.IsSold);
			var mock = Mock.Of<IMarketRepository>(r => r.GetRecentOpenListings() == listings);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetRecentOpenListings();

			Assert.NotNull(result);
			Assert.Equal(listings, result);
		}

		[Fact]
		public void GetRecentOpenListings_Error()
		{
			var mock = Mock.Of<IMarketRepository>(r => r.GetRecentOpenListings() == null);
			var service = new MarketService(
				mock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var result = service.GetRecentOpenListings();

			Assert.Null(result);
		}

		[Fact]
		public void CreateListing_Success()
		{
			int userId = 1;
			int itemId = 1;
			int quantity = 1;
			int price = 1;
			var result = new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 1,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			};
			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.Insert(It.IsAny<Market>()) == Task.FromResult(result));
			var inventoryRepoMock = Mock.Of<IInventoryRepository>(r =>
				r.GetUserItem(userId, itemId) == inventory[0] &&
				r.UpdateQuantity(It.IsAny<Inventory>()) == true
			);
			var service = new MarketService(
				marketRepoMock,
				inventoryRepoMock,
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var newListing = service.CreateListing(userId, itemId, quantity, price);

			Assert.NotNull(newListing);
			Assert.Equal(result, newListing);
		}

		[Fact]
		public void CreateListing_ItemNotFound()
		{
			int userId = 1;
			int itemId = 42;
			int quantity = 1;
			int price = 1;
			var result = new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 1,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			};
			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.Insert(It.IsAny<Market>()) == Task.FromResult(result));
			var inventoryRepoMock = Mock.Of<IInventoryRepository>(r =>
				r.GetUserItem(userId, itemId) == null &&
				r.UpdateQuantity(It.IsAny<Inventory>()) == true
			);
			var service = new MarketService(
				marketRepoMock,
				inventoryRepoMock,
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var newListing = service.CreateListing(userId, itemId, quantity, price);

			Assert.Null(newListing);
		}

		[Fact]
		public void CreateListing_NotEnoughtQuantity()
		{
			int userId = 1;
			int itemId = 42;
			int quantity = 1;
			int price = 1;
			var result = new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 1,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			};
			var inventory = new Inventory
			{
				IdUser = 1,
				IdItem = 1,
				Quantity = 0
			};

			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.Insert(It.IsAny<Market>()) == Task.FromResult(result));
			var inventoryRepoMock = Mock.Of<IInventoryRepository>(r =>
				r.GetUserItem(userId, itemId) == inventory &&
				r.UpdateQuantity(It.IsAny<Inventory>()) == true
			);
			var service = new MarketService(
				marketRepoMock,
				inventoryRepoMock,
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var newListing = service.CreateListing(userId, itemId, quantity, price);

			Assert.Null(newListing);
		}

		[Fact]
		public void CreateListing_InsertError()
		{
			int userId = 1;
			int itemId = 1;
			int quantity = 1;
			int price = 1;
			var result = new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 1,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			};
			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.Insert(It.IsAny<Market>()) == Task.FromResult<Market?>(null));
			var inventoryRepoMock = Mock.Of<IInventoryRepository>(r =>
				r.GetUserItem(userId, itemId) == inventory[0] &&
				r.UpdateQuantity(It.IsAny<Inventory>()) == true
			);
			var service = new MarketService(
				marketRepoMock,
				inventoryRepoMock,
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var newListing = service.CreateListing(userId, itemId, quantity, price);

			Assert.Null(newListing);
		}

		[Fact]
		public void CreateListing_QuantityUpdateError()
		{
			int userId = 1;
			int itemId = 1;
			int quantity = 1;
			int price = 1;
			var result = new Market
			{
				Id = 4,
				IdSeller = 1,
				Price = 1,
				IdItem = 1,
				Quantity = 1,
				IsSold = false
			};
			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.Insert(It.IsAny<Market>()) == Task.FromResult<Market?>(null));
			var inventoryRepoMock = Mock.Of<IInventoryRepository>(r =>
				r.GetUserItem(userId, itemId) == inventory[0] &&
				r.UpdateQuantity(It.IsAny<Inventory>()) == false
			);
			var service = new MarketService(
				marketRepoMock,
				inventoryRepoMock,
				Mock.Of<IUserRepository>(),
				Mock.Of<IItemRepository>()
			);

			var newListing = service.CreateListing(userId, itemId, quantity, price);

			Assert.Null(newListing);
		}

		[Fact]
		public void GetAveragePriceByItemId_Success()
		{
			int itemId = 1;
			var listings = market
				.Where(x => !x.IsSold && x.IdItem == itemId);
			var item = items
				.Where(x => x.Id == itemId)
				.First();

			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == listings);
			var itemRepoMock = Mock.Of<IItemRepository>(r => r.GetById(itemId) == item);
			var service = new MarketService(
				marketRepoMock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				itemRepoMock
			);

			var average = service.GetAveragePriceByItemId(itemId);

			Assert.NotNull(average);
			Assert.Equal(9, average!.AveragePrice);
			Assert.Equal(item.Name, average.Name);
			Assert.Equal(itemId, average.Id);
		}

		[Fact]
		public void GetAveragePriceByItemId_GetListingsError()
		{
			int itemId = 1;
			var item = items
				.Where(x => x.Id == itemId)
				.First();

			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == null);
			var itemRepoMock = Mock.Of<IItemRepository>(r => r.GetById(itemId) == item);
			var service = new MarketService(
				marketRepoMock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				itemRepoMock
			);

			var average = service.GetAveragePriceByItemId(itemId);

			Assert.Null(average);
		}

		[Fact]
		public void GetAveragePriceByItemId_ItemNotFound()
		{
			int itemId = 1;
			var listings = market
				.Where(x => !x.IsSold && x.IdItem == itemId);

			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == listings);
			var itemRepoMock = Mock.Of<IItemRepository>(r => r.GetById(itemId) == null);
			var service = new MarketService(
				marketRepoMock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				itemRepoMock
			);

			var average = service.GetAveragePriceByItemId(itemId);

			Assert.Null(average);
		}

		[Fact]
		public void GetAveragePriceByItemId_NoListing()
		{
			int itemId = 1;
			var listings = new List<Market>();
			var item = items
				.Where(x => x.Id == itemId)
				.First();

			var marketRepoMock = Mock.Of<IMarketRepository>(r => r.GetOpenListingsByItemId(itemId) == listings);
			var itemRepoMock = Mock.Of<IItemRepository>(r => r.GetById(itemId) == item);
			var service = new MarketService(
				marketRepoMock,
				Mock.Of<IInventoryRepository>(),
				Mock.Of<IUserRepository>(),
				itemRepoMock
			);

			var average = service.GetAveragePriceByItemId(itemId);

			Assert.NotNull(average);
			Assert.Equal(0, average!.AveragePrice);
			Assert.Equal(item.Name, average.Name);
			Assert.Equal(itemId, average.Id);
		}
	}
}
