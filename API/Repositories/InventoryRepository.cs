using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class InventoryRepository : Repository<Tinventory, Inventory>, IInventoryRepository
	{
		public InventoryRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<UserRepository> logger) : base(context, mapper, logger)
		{
		}

		public Inventory? GetUserItem(int userId, int itemId)
		{
			try
			{
				Tinventory? item = _set
					.AsNoTracking()
					.FirstOrDefault(x => x.IdUser == userId && x.IdItem == itemId);
				return _mapper.Map<Inventory>(item);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public bool UpdateQuantity(Inventory item)
		{
			try
			{
				var inventory = _set
					.Where(x => x.IdItem == item.IdItem)
					.Where(x => x.IdUser == item.IdUser)
					.FirstOrDefault();
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

		public IEnumerable<Inventory>? GetUserInventory(int userId)
		{
			try
			{
				List<Tinventory>? item = _set
					.Where(x => x.IdUser == userId)
					.Where(x => x.Quantity != 0)
					.AsNoTracking()
					.ToList();
				return _mapper.Map<List<Inventory>>(item);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}
	}
}
