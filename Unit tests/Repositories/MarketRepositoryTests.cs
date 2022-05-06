﻿using API.DataAccess;
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
	public class MarketRepositoryTests
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

		private MarketRepository GetRepo(string dbName)
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
			var logger = Mock.Of<ILogger<MarketRepository>>();

			return new MarketRepository(context, mapper, logger);
		}

		[Fact]
		public void GetById_Success()
		{
			var id = 1;
			var repo = GetRepo(nameof(GetById_Success));
			var listing = repo.GetById(id);

			Assert.NotNull(listing);
			Assert.Equal(1, listing!.Id);
		}

		[Fact]
		public void GetById_NotFound()
		{
			var id = 42;
			var repo = GetRepo(nameof(GetById_NotFound));
			var user = repo.GetById(id);

			Assert.Null(user);
		}

		[Fact]
		public void GetOpenListingsByItemId_Success()
		{
			var id = 1;
			var repo = GetRepo(nameof(GetOpenListingsByItemId_Success));
			var listings = repo.GetOpenListingsByItemId(id);

			Assert.NotNull(listings);
			Assert.Single(listings);
		}

		[Fact]
		public void GetOpenListingsByItemId_NotFound()
		{
			var id = 42;
			var repo = GetRepo(nameof(GetOpenListingsByItemId_NotFound));
			var listings = repo.GetOpenListingsByItemId(id);

			Assert.NotNull(listings);
			Assert.Empty(listings);
		}
	}
}