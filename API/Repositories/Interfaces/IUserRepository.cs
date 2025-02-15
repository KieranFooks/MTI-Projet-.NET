﻿using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<Tuser, User>
	{
		int Count();
		User? GetById(int id);
		User? GetByName(string name);
		bool TransferUserMoney(int senderId, int receiverId, int money);
		User? GetUserByNameAndPassword(string username, string password);
	}
}
