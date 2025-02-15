﻿using API.DataAccess;
using API.Dbo;
using API.Repositories;
using API.Repositories.Interfaces;
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
	public class UserRepositoryTests
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

		private UserRepository GetRepo(string dbName)
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
			var logger = Mock.Of<ILogger<UserRepository>>();

			return new UserRepository(context, mapper, logger);
		}

		[Fact]
		public void CountTest()
		{
			var repo = GetRepo(nameof(CountTest));
			var count = repo.Count();

			Assert.Equal(4, count);
		}

		[Fact]
		public void GetByIdTest_Success()
		{
			var id = 1;
			var repo = GetRepo(nameof(GetByIdTest_Success));
			var user = repo.GetById(id);

			Assert.NotNull(user);
			Assert.Equal(id, user!.Id);
		}

		[Fact]
		public void GetByIdTest_NotFound()
		{
			var id = 42;
			var repo = GetRepo(nameof(GetByIdTest_NotFound));
			var user = repo.GetById(id);

			Assert.Null(user);
		}

		[Fact]
		public void GetByNameTest_Success()
		{
			var name = "Gabriel";
			var repo = GetRepo(nameof(GetByNameTest_Success));
			var user = repo.GetByName(name);

			Assert.NotNull(user);
			Assert.Equal(name, user!.Name);
		}

		[Fact]
		public void GetByNameTest_NotFound()
		{
			var name = "Anonymous";
			var repo = GetRepo(nameof(GetByNameTest_NotFound));
			var user = repo.GetByName(name);

			Assert.Null(user);
		}

		[Fact]
		public void TransferUserMoney_Success()
		{
			var amount = 100;
			var senderId = 1;
			var receiverId = 2;

			var repo = GetRepo(nameof(TransferUserMoney_Success));
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.True(transfer);

			var sender = repo.GetById(senderId);
			var receiver = repo.GetById(receiverId);

			Assert.NotNull(sender);
			Assert.NotNull(receiver);
			Assert.Equal(4900, sender!.Money);
			Assert.Equal(5100, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_NotEnoughMoney()
		{
			var amount = 6000;
			var senderId = 2;
			var receiverId = 1;

			var repo = GetRepo(nameof(TransferUserMoney_NotEnoughMoney));
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var sender = repo.GetById(senderId);
			var receiver = repo.GetById(receiverId);

			Assert.NotNull(sender);
			Assert.NotNull(receiver);
			Assert.Equal(5000, sender!.Money);
			Assert.Equal(5000, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_SenderNotFound()
		{
			var amount = 100;
			var senderId = 42;
			var receiverId = 1;

			var repo = GetRepo(nameof(TransferUserMoney_SenderNotFound));
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var receiver = repo.GetById(receiverId);

			Assert.NotNull(receiver);
			Assert.Equal(5000, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_ReceiverNotFound()
		{
			var amount = 100;
			var senderId = 1;
			var receiverId = 42;

			var repo = GetRepo(nameof(TransferUserMoney_ReceiverNotFound));
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var sender = repo.GetById(senderId);

			Assert.NotNull(sender);
			Assert.Equal(5000, sender!.Money);
		}
	}
}
