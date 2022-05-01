namespace API.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<DataAccess.User, Dbo.User>
	{
		int Count();
		Dbo.User? GetById(int id);
	}
}
