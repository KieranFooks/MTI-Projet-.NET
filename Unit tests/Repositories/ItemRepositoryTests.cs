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
