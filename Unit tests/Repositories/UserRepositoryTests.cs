using API.DataAccess;
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
		private DbContextOptions<Hotel_des_ventesContext> options;

		public UserRepositoryTests()
		{
			options = new DbContextOptionsBuilder<Hotel_des_ventesContext>().
				UseInMemoryDatabase(databaseName: "db")
				.EnableSensitiveDataLogging()
				.Options;
			InitData();
		}

		private void InitData()
		{
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
			context.Tusers.AddRange(users);
			context.SaveChanges();
		}

		private UserRepository GetRepo()
		{
			var context = new Hotel_des_ventesContext(options);
			var mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Tuser, User>();
			}));
			var logger = Mock.Of<ILogger<UserRepository>>();

			return new UserRepository(context, mapper, logger);
		}

		[Fact]
		public void CountTest()
		{
			var repo = GetRepo();
			var count = repo.Count();

			Assert.Equal(3, count);
		}

		[Fact]
		public void GetByIdTest_Success()
		{
			var id = 1;
			var repo = GetRepo();
			var user = repo.GetById(id);

			Assert.NotNull(user);
			Assert.Equal(id, user!.Id);
		}

		[Fact]
		public void GetByIdTest_NotFound()
		{
			var id = 42;
			var repo = GetRepo();
			var user = repo.GetById(id);

			Assert.Null(user);
		}

		[Fact]
		public void GetByNameTest_Success()
		{
			var name = "Legwarmer6486";
			var repo = GetRepo();
			var user = repo.GetByName(name);

			Assert.NotNull(user);
			Assert.Equal(name, user!.Name);
		}

		[Fact]
		public void GetByNameTest_NotFound()
		{
			var name = "Anonymous";
			var repo = GetRepo();
			var user = repo.GetByName(name);

			Assert.Null(user);
		}

		[Fact]
		public void TransferUserMoney_Success()
		{
			var amount = 100;
			var senderId = 1;
			var receiverId = 2;

			var repo = GetRepo();
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.True(transfer);

			var sender = repo.GetById(senderId);
			var receiver = repo.GetById(receiverId);

			Assert.NotNull(sender);
			Assert.NotNull(receiver);
			Assert.Equal(4142, sender!.Money);
			Assert.Equal(100, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_NotEnoughMoney()
		{
			var amount = 100;
			var senderId = 2;
			var receiverId = 1;

			var repo = GetRepo();
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var sender = repo.GetById(senderId);
			var receiver = repo.GetById(receiverId);

			Assert.NotNull(sender);
			Assert.NotNull(receiver);
			Assert.Equal(0, sender!.Money);
			Assert.Equal(4242, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_SenderNotFound()
		{
			var amount = 100;
			var senderId = 42;
			var receiverId = 1;

			var repo = GetRepo();
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var receiver = repo.GetById(receiverId);

			Assert.NotNull(receiver);
			Assert.Equal(4242, receiver!.Money);
		}

		[Fact]
		public void TransferUserMoney_ReceiverNotFound()
		{
			var amount = 100;
			var senderId = 1;
			var receiverId = 42;

			var repo = GetRepo();
			var transfer = repo.TransferUserMoney(senderId, receiverId, amount);

			Assert.False(transfer);

			var sender = repo.GetById(senderId);

			Assert.NotNull(sender);
			Assert.Equal(4242, sender!.Money);
		}
	}
}
