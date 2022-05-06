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

		private ItemService GetService(IItemRepository itemRepository)
		{
			return new ItemService(itemRepository);
		}

		[Fact]
		public void GetById_Success()
		{
			int id = 1;
			var mock = Mock.Of<IItemRepository>(r => r.GetById(id) == items.ElementAt(0));
			var service = new ItemService(mock);

			var item = service.GetById(id);

			Assert.NotNull(item);
			Assert.Equal(items.ElementAt(0), item);
		}

		[Fact]
		public void GetById_NotFound()
		{
			int id = 42;
			var mock = Mock.Of<IItemRepository>(r => r.GetById(id) == null);
			var service = new ItemService(mock);

			var item = service.GetById(id);

			Assert.Null(item);
		}

		[Fact]
		public async Task GetAll_Success()
		{
			var mock = Mock.Of<IItemRepository>(r => r.Get("") == Task.FromResult(items));
			var service = new ItemService(mock);

			var getAllItems = await service.GetAll();

			Assert.NotNull(getAllItems);
			Assert.Equal(items, getAllItems);
		}

		[Fact]
		public async Task GetAll_Error()
		{
			var mock = Mock.Of<IItemRepository>(r => r.Get("") == Task.FromResult<IEnumerable<Item>?>(null));
			var service = new ItemService(mock);

			var getAllItems = await service.GetAll();

			Assert.Null(getAllItems);
		}
	}
}
