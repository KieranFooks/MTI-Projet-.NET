using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class UserRepository : Repository<Tuser, User>, IUserRepository
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

		public User? GetById(int id)
		{
			try
			{
				Tuser? user = _set
					.AsNoTracking()
					.FirstOrDefault(user => user.Id == id);
				return _mapper.Map<User?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public User? GetByName(string name)
		{
			try
			{
				Tuser? user = _set
					.AsNoTracking()
					.FirstOrDefault(user => user.Name.Equals(name));
				return _mapper.Map<User?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public User? GetUserByNameAndPassword(string username, string password)
		{
			try
			{
				Tuser? user = _set
					.AsNoTracking()
					.Where(user => user.Name.Equals(username))
					.Where(user => user.Password.Equals(password))
					.FirstOrDefault();
				return _mapper.Map<User?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public bool TransferUserMoney(int senderId, int receiverId, int money)
		{
			try
			{
				Tuser? sender = _set.FirstOrDefault(user => user.Id == senderId);
				if (sender == null || sender.Money - money < 0)
				{
					return false;
				}
				Tuser? receiver = _set.FirstOrDefault(user => user.Id == receiverId);
				if (receiver == null)
				{
					return false;
				}

				sender.Money -= money;
				receiver.Money += money;
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return false;
			}
		}
	}
}
