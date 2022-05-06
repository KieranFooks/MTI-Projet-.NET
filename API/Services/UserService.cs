using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class UserService: IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IInventoryRepository _inventoryRepository;
		private readonly IItemRepository _itemRepository;

		public UserService(IUserRepository userRepository, IInventoryRepository inventoryRepository, IItemRepository itemRepository)
		{
			_userRepository = userRepository;
			_inventoryRepository = inventoryRepository;
			_itemRepository = itemRepository;
		}

		public async Task<User?> CreateUser(string username, string password)
		{
			var items = await _itemRepository.Get();
			var user = new User
			{
				Name = username,
				Password = password,
				Money = 100
			};
			var user_insert = await _userRepository.Insert(user);
			if (user_insert == null)
				return null;
			// TODO: Give items to the user
			List<Inventory> inventories = new List<Inventory>();
			
			foreach (var item in items)
			{
				var starting_items = new Inventory
				{
					IdItem = item.Id,
					IdUser = user_insert.Id,
					Quantity = 1
				};
				inventories.Add(starting_items);
			}
			await _inventoryRepository.InsertRange(inventories);
			return user_insert;
		}

		public int GetNumberOfUsers()
		{
			return _userRepository.Count();
		}

		public User? GetUserById(int id)
		{
			return _userRepository.GetById(id);
		}

		public bool IsNameAvailable(string username)
		{
			return _userRepository.GetByName(username) == null;
		}

		public int? GetUserMoney(int userId)
		{
			return _userRepository.GetById(userId)?.Money;
		}

		public User? Connect(string name, string password)
		{
			return _userRepository.GetUserByNameAndPassword(name, password);
		}

		public IEnumerable<Inventory>? GetUserInventory(int userId)
		{
			return _inventoryRepository.GetUserInventory(userId);
		}
	}
}
