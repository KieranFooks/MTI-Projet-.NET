using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class ItemRepository : Repository<Titem, Item>, IItemRepository
	{
		public ItemRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<ItemRepository> logger) : base(context, mapper, logger)
		{
		}

		public Item? GetById(int id)
		{
			try
			{
				Titem? user = _set
					.AsNoTracking()
					.FirstOrDefault(item => item.Id == id);
				return _mapper.Map<Item?>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}
	}
}
