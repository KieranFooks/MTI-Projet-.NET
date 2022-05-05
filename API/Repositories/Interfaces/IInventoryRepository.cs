using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IInventoryRepository : IRepository<Tinventory, Inventory>
	{
		Inventory? GetUserItem(int userId, int itemId);
		bool UpdateQuantity(Inventory item);
		public IEnumerable<Inventory>? GetUserInventory(int userId);
	}
}
