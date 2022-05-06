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
	public class ItemServiceTests
	{
		private DbContextOptions<Hotel_des_ventesContext> options;
		IEnumerable<Item> items = new List<Item>
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

		public ItemServiceTests()
		{
			options = new DbContextOptionsBuilder<Hotel_des_ventesContext>().
				UseInMemoryDatabase(databaseName: "db")
				.EnableSensitiveDataLogging()
				.Options;
		}

		private ItemService GetService(IItemRepository itemRepository)
		{
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

			var userRepoLogger = Mock.Of<ILogger<UserRepository>>();
			var inventoryRepoLogger = Mock.Of<ILogger<InventoryRepository>>();

			return new ItemService(itemRepository);
		}

		[Fact]
		public void GetById_Success()
		{
			int id = 1;
			var mock = Mock.Of<IItemRepository>(r => r.GetById(id) == items.ElementAt(0));
			var service = GetService(mock);

			var item = service.GetById(id);

			Assert.NotNull(item);
			Assert.Equal(items.ElementAt(0), item);
		}

		[Fact]
		public void GetById_NotFound()
		{
			int id = 42;
			var mock = Mock.Of<IItemRepository>(r => r.GetById(id) == null);
			var service = GetService(mock);

			var item = service.GetById(id);

			Assert.Null(item);
		}

		[Fact]
		public async Task GetAll_Success()
		{
			var mock = Mock.Of<IItemRepository>(r => r.Get("") == Task.FromResult(items));
			var service = GetService(mock);

			var getAllItems = await service.GetAll();

			Assert.NotNull(getAllItems);
			Assert.Equal(items, getAllItems);
		}

		[Fact]
		public async Task GetAll_Error()
		{
			var mock = Mock.Of<IItemRepository>(r => r.Get("") == Task.FromResult<IEnumerable<Item>?>(null));
			var service = GetService(mock);

			var getAllItems = await service.GetAll();

			Assert.Null(getAllItems);
		}
	}
}
