using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IItemRepository : IRepository<Titem, Item>
	{
		Item? GetById(int id);
	}
}
