﻿using API.DataAccess;
using API.Dbo;
using API.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Unit_tests.Repositories
{
	public class InventoryRepositoryTests
	{
		private DbContextOptions<Hotel_des_ventesContext> InitData(string dbName)
		{
			var options = new DbContextOptionsBuilder<Hotel_des_ventesContext>().
				UseInMemoryDatabase(databaseName: dbName)
				.EnableSensitiveDataLogging()
				.Options;
			using var context = new Hotel_des_ventesContext(options);
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();		
			return options;
		}

		private InventoryRepository GetRepo(string dbName)
		{
			var options = InitData(dbName);
			var context = new Hotel_des_ventesContext(options);
			var mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Tinventory, Inventory>();
				cfg.CreateMap<Inventory, Tinventory>();
				cfg.CreateMap<Tuser, User>();
				cfg.CreateMap<User, Tuser>();
				cfg.CreateMap<Titem, Item>();
				cfg.CreateMap<Item, Titem>();
				cfg.CreateMap<Tmarket, Market>();
				cfg.CreateMap<Market, Tmarket>();
			}));
			var logger = Mock.Of<ILogger<InventoryRepository>>();

			return new InventoryRepository(context, mapper, logger);
		}

		[Fact]
		public void GetUserItem_Success()
		{
			int userId = 1;
			int itemId = 2;
			var repo = GetRepo(nameof(GetUserItem_Success));

			var item = repo.GetUserItem(userId, itemId);

			Assert.NotNull(item);
			Assert.Equal(userId, item!.IdUser);
			Assert.Equal(itemId, item.IdItem);
			Assert.Equal(1, item.Quantity);
		}

		[Fact]
		public void GetUserItem_UserNotFound()
		{
			int userId = 42;
			int itemId = 2;
			var repo = GetRepo(nameof(GetUserItem_UserNotFound));

			var item = repo.GetUserItem(userId, itemId);

			Assert.Null(item);
		}

		[Fact]
		public void GetUserItem_ItemNotFound()
		{
			int userId = 1;
			int itemId = 42;
			var repo = GetRepo(nameof(GetUserItem_ItemNotFound));

			var item = repo.GetUserItem(userId, itemId);

			Assert.Null(item);
		}

		[Fact]
		public void GetUserItem_ItemAndUserNotFound()
		{
			int userId = 42;
			int itemId = 42;
			var repo = GetRepo(nameof(GetUserItem_ItemAndUserNotFound));

			var item = repo.GetUserItem(userId, itemId);

			Assert.Null(item);
		}

		[Fact]
		public void GetUserItem_NoItem()
		{
			int userId = 2;
			int itemId = 2;
			var repo = GetRepo(nameof(GetUserItem_NoItem));

			var item = repo.GetUserItem(userId, itemId);

			Assert.Null(item);
		}

		[Fact]
		public void UpdateQuantity_Success()
		{
			var inventory = new Inventory
			{
				IdItem = 1,
				IdUser = 1,
				Quantity = 10
			};
			var repo = GetRepo(nameof(UpdateQuantity_Success));

			var updated = repo.UpdateQuantity(inventory);

			Assert.True(updated);

			var item = repo.GetUserItem(inventory.IdUser, inventory.IdItem);

			Assert.NotNull(item);
			Assert.Equal(inventory.IdUser, item!.IdUser);
			Assert.Equal(inventory.IdItem, item.IdItem);
			Assert.Equal(10, item.Quantity);
		}

		[Fact]
		public void UpdateQuantity_NoInventory()
		{
			var inventory = new Inventory
			{
				IdItem = 2,
				IdUser = 2,
				Quantity = 10
			};
			var repo = GetRepo(nameof(UpdateQuantity_NoInventory));

			var updated = repo.UpdateQuantity(inventory);

			Assert.False(updated);

			var item = repo.GetUserItem(inventory.IdUser, inventory.IdItem);

			Assert.Null(item);
		}

		[Fact]
		public void GetUserInventory_Success()
		{
			int userId = 1;
			var repo = GetRepo(nameof(GetUserInventory_Success));

			var inventory = repo.GetUserInventory(userId);

			Assert.NotNull(inventory);
			Assert.Equal(2, inventory!.Count());
			Assert.Equal(1, inventory!.ElementAt(0).IdUser);
			Assert.Equal(1, inventory!.ElementAt(0).IdItem);
			Assert.Equal(1, inventory!.ElementAt(0).Quantity);
			Assert.Equal(1, inventory!.ElementAt(1).IdUser);
			Assert.Equal(2, inventory!.ElementAt(1).IdItem);
			Assert.Equal(1, inventory!.ElementAt(1).Quantity);
		}

		[Fact]
		public void GetUserInventory_EmptyInventory()
		{
			int userId = 3;
			var repo = GetRepo(nameof(GetUserInventory_EmptyInventory));

			var inventory = repo.GetUserInventory(userId);

			Assert.NotNull(inventory);
			Assert.Empty(inventory!);
		}

		[Fact]
		public void GetUserInventory_UserNotFound()
		{
			int userId = 42;
			var repo = GetRepo(nameof(GetUserInventory_UserNotFound));

			var inventory = repo.GetUserInventory(userId);

			Assert.NotNull(inventory);
			Assert.Empty(inventory);
		}
	}
}
