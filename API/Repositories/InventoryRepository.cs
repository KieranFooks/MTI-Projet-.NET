using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
	public class InventoryRepository : Repository<Tinventory, Inventory>, IInventoryRepository
	{
		public InventoryRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<UserRepository> logger) : base(context, mapper, logger)
		{
		}

		public int GetItemQuantityOfUser(int userId, int itemId)
		{
			try
			{
				Tinventory? item = _set
					.FirstOrDefault(x => x.IdUser == userId && x.IdItem == itemId);
				if (item == null)
				{
					return 0;
				}
				return item.Quantity;
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return -1;
			}
		}

		public bool UpdateQuantity(Inventory item)
		{
			try
			{
				var inventory = _set.FirstOrDefault(x => x.IdItem == item.IdItem && x.IdUser == item.IdUser);
				if (inventory != null)
				{
					inventory.Quantity = item.Quantity;
					_context.SaveChanges();
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return false;
			}
		}
	}
}
