using API.DataAccess;
using AutoMapper;

namespace API.Repositories
{
	public class UserRepository : Repository<DataAccess.User, Dbo.User>, Interfaces.IUserRepository
	{
		public UserRepository(Hotel_des_ventesContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public int Count()
		{
			return _set.Count();
		}
	}
}
