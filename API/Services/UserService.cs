using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class UserService: IUserService
	{
		private IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public int GetNumberOfUsers()
		{
			return 0;
		}
	}
}
