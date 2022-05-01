namespace API.Services.Interfaces
{
	public interface IUserService
	{
		int GetNumberOfUsers();
		Task<Dbo.User?> CreateUser(string name, string password);
		Dbo.User? GetUserById(int id);
		bool IsNameAvailable(string name);
	}
}
