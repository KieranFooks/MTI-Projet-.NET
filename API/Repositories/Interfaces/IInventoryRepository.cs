using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IInventoryRepository : IRepository<Tinventory, Inventory>
	{
		int GetItemQuantityOfUser(int userId, int itemId);
		bool UpdateQuantity(Inventory item);
	}
}
