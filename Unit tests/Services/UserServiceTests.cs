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
	public class UserServiceTests
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

		[Fact]
		public void GetUserById_Success()
		{
			int id = 1;
			var mock = Mock.Of<IUserRepository>(r => r.GetById(id) == users[0]);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(),Mock.Of<IItemRepository>());

			var user = service.GetUserById(id);

			Assert.NotNull(user);
			Assert.Equal(users[0], user);
		}

		[Fact]
		public void GetUserById_NotFound()
		{
			int id = 42;
			var mock = Mock.Of<IUserRepository>(r => r.GetById(id) == null);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var user = service.GetUserById(id);

			Assert.Null(user);
		}

		[Fact]
		public void GetNumberOfUsers_Success()
		{
			var count = 42;
			var mock = Mock.Of<IUserRepository>(r => r.Count() == count);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var returnedCount = service.GetNumberOfUsers();

			Assert.Equal(count, returnedCount);
		}

		[Fact]
		public void IsNameAvailable_Success()
		{
			var name = "TestName";
			var mock = Mock.Of<IUserRepository>(r => r.GetByName(name) == null);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var available = service.IsNameAvailable(name);

			Assert.True(available);
		}

		[Fact]
		public void IsNameAvailable_NotAvailable()
		{
			var name = users[0].Name;
			var mock = Mock.Of<IUserRepository>(r => r.GetByName(name) == users[0]);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var available = service.IsNameAvailable(name);

			Assert.False(available);
		}

		[Fact]
		public void GetUserMoney_Success()
		{
			var user = users[0];
			var mock = Mock.Of<IUserRepository>(r => r.GetById(user.Id) == user);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var userMoney = service.GetUserMoney(user.Id);

			Assert.NotNull(userMoney);
			Assert.Equal(user.Money, userMoney);
		}

		[Fact]
		public void GetUserMoney_NotFound()
		{
			var id = 42;
			var mock = Mock.Of<IUserRepository>(r => r.GetById(id) == null);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var userMoney = service.GetUserMoney(id);

			Assert.Null(userMoney);
		}

		[Fact]
		public void Connect_Success()
		{
			var user = users[0];
			var mock = Mock.Of<IUserRepository>(r => r.GetUserByNameAndPassword(user.Name, user.Password) == user);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var connectedUser = service.Connect(user.Name, user.Password);

			Assert.NotNull(connectedUser);
			Assert.Equal(user, connectedUser);
		}

		[Fact]
		public void Connect_Error()
		{
			var user = users[0];
			var pwd = "123456789";
			var mock = Mock.Of<IUserRepository>(r => r.GetUserByNameAndPassword(user.Name, pwd) == null);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var connectedUser = service.Connect(user.Name, pwd);

			Assert.Null(connectedUser);
		}

		[Fact]
		public void Connect_NotFound()
		{
			var name = "Waaaaaa";
			var pwd = "123456789";
			var mock = Mock.Of<IUserRepository>(r => r.GetUserByNameAndPassword(name, pwd) == null);
			var service = new UserService(mock, Mock.Of<IInventoryRepository>(), Mock.Of<IItemRepository>());

			var connectedUser = service.Connect(name, pwd);

			Assert.Null(connectedUser);
		}

		[Fact]
		public void GetUserInventory_Success()
		{
			int id = 1;
			var mock = Mock.Of<IInventoryRepository>(r => r.GetUserInventory(id) == inventory);
			var service = new UserService(Mock.Of<IUserRepository>(), mock, Mock.Of<IItemRepository>());

			var userInventory = service.GetUserInventory(id);

			Assert.NotNull(userInventory);
			Assert.NotEmpty(userInventory);
			Assert.Equal(inventory, userInventory);
		}

		[Fact]
		public void GetUserInventory_Empty()
		{
			int id = 1;
			var mock = Mock.Of<IInventoryRepository>(r => r.GetUserInventory(id) == new List<Inventory>());
			var service = new UserService(Mock.Of<IUserRepository>(), mock, Mock.Of<IItemRepository>());

			var userInventory = service.GetUserInventory(id);

			Assert.NotNull(userInventory);
			Assert.Empty(userInventory);
		}

		[Fact]
		public void GetUserInventory_NotFound()
		{
			int id = 42;
			var mock = Mock.Of<IInventoryRepository>(r => r.GetUserInventory(id) == null);
			var service = new UserService(Mock.Of<IUserRepository>(), mock, Mock.Of<IItemRepository>());

			var userInventory = service.GetUserInventory(id);

			Assert.Null(userInventory);
		}
	}
}
