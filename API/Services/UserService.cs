using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class UserService: IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Dbo.User?> CreateUser(string username, string password)
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

		public Dbo.User? GetUserById(int id)
		{
			return _userRepository.GetById(id);
		}

		public bool IsNameAvailable(string username)
		{
			return _userRepository.GetByName(username) == null;
		}
	}
}
