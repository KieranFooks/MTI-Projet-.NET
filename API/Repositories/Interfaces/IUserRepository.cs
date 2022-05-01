namespace API.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<DataAccess.User, Dbo.User>
	{
		public int Count();
	}
}
