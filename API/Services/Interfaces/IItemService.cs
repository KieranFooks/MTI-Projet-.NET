using API.Dbo;

namespace API.Services.Interfaces
{
	public interface IItemService
	{
		Item? GetById(int id);
		Task<IEnumerable<Item>?> GetAll();
	}
}
