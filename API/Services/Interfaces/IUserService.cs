﻿using API.Dbo;

namespace API.Services.Interfaces
{
	public interface IUserService
	{
		int GetNumberOfUsers();
		Task<User?> CreateUser(string name, string password);
		User? GetUserById(int id);
		bool IsNameAvailable(string name);
		User? Connect(string name, string password);
		public IEnumerable<Inventory>? GetUserInventory(int userId);
		public int? GetUserMoney(int userId);

	}
}
