using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
	public class ItemRepository : Repository<Titem, Item>, IItemRepository
	{
		public ItemRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
		{
		}
	}
}
