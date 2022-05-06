using API.DataAccess;
using API.Dbo;
using API.Repositories;
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

namespace Unit_tests.Repositories
{
	public class ItemRepositoryTests
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

			List<Tuser> users = new List<Tuser>
			{
				new Tuser
				{
					Name = "XxGamerxX",
					Password = "azertyuiop",
					Money = 4242
				},
				new Tuser
				{
					Name = "Legwarmer6486",
					Password = "3YPRgph3e@VNzzxX&c",
					Money = 0
				},
				new Tuser
				{
					Name = "Winner4604",
					Password = "mVWbUp%oA!#f4UTda4",
					Money = 9000
				}
			};
			List<Titem> items = new List<Titem>
			{
				new Titem
				{
					Name = "Sword",
					Description = "Use to cut things, usually people"
				},
				new Titem
				{
					Name = "Pen",
					Description = "Use it to write or draw"
				}
			};
			List<Tmarket> market = new List<Tmarket>
			{
				new Tmarket
				{
					Id = 1,
					IdSeller = 1,
					Price = 100,
					IdItem = 1,
					Quantity = 5,
					IsSold = false
				},
				new Tmarket
				{
					Id = 2,
					IdSeller = 2,
					Price = 10,
					IdItem = 1,
					Quantity = 1,
					IsSold = true
				},
				new Tmarket
				{
					Id = 3,
					IdSeller = 2,
					Price = 10,
					IdItem = 2,
					Quantity = 5,
					IsSold = false
				}
			};
			context.Tusers.AddRange(users);
			context.Titems.AddRange(items);
			context.Tmarkets.AddRange(market);
			context.SaveChanges();

			return options;
		}

		private ItemRepository GetRepo(string dbName)
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
			var logger = Mock.Of<ILogger<ItemRepository>>();

			return new ItemRepository(context, mapper, logger);
		}

		[Fact]
		public void GetById_Success()
		{
			int id = 1;
			var repo = GetRepo(nameof(GetById_Success));
			var item = repo.GetById(id);

			Assert.NotNull(item);
			Assert.Equal(id, item!.Id);
		}

		[Fact]
		public void GetById_NotFound()
		{
			int id = 42;
			var repo = GetRepo(nameof(GetById_NotFound));
			var item = repo.GetById(id);

			Assert.Null(item);
		}
	}
}
