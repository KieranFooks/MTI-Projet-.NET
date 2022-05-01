namespace API.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<DataAccess.Tuser, Dbo.User>
	{
		int Count();
		Dbo.User? GetById(int id);
		Dbo.User? GetByName(string name);
	}
}
