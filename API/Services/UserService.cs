using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class UserService: IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IInventoryRepository _inventoryRepository;

		public UserService(IUserRepository userRepository, IInventoryRepository inventoryRepository)
		{
			_userRepository = userRepository;
			_inventoryRepository = inventoryRepository;
		}

		public async Task<User?> CreateUser(string username, string password)
		{
			var user = new Dbo.User
			{
				Name = username,
				Password = password,
				Money = 100
			};

			// TODO: Give items to the user

			return await _userRepository.Insert(user);
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
