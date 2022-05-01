using API.DataAccess;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
	public class UserRepository : Repository<DataAccess.User, Dbo.User>, IUserRepository
	{
		public UserRepository(Hotel_des_ventesContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public int Count()
		{
			return _set.Count();
		}

		public Dbo.User? GetById(int id)
		{
			DataAccess.User? user = _set.FirstOrDefault(user => user.Id == id);
			return _mapper.Map<Dbo.User?>(user);
		}
	}
}
