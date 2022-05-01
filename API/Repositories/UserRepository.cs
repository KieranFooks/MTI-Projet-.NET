using API.DataAccess;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
	public class UserRepository : Repository<DataAccess.User, Dbo.User>, IUserRepository
	{
		public UserRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<UserRepository> logger) : base(context, mapper, logger)
		{
		}

		public int Count()
		{
			try
			{
				return _set.Count();
			}
			catch(Exception ex)
			{
				_logger.LogError("error on db", ex);
				return -1;
			}
		}

		public Dbo.User? GetById(int id)
		{
			try
			{
				DataAccess.User? user = _set.FirstOrDefault(user => user.Id == id);
				return _mapper.Map<Dbo.User?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public Dbo.User? GetByName(string name)
		{
			try
			{
				DataAccess.User? user = _set.FirstOrDefault(user => user.Name.Equals(name));
				return _mapper.Map<Dbo.User?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}
	}
}
