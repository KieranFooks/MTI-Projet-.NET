using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories
{
	public class MarketRepository : Repository<Tmarket, Market>, IMarketRepository
	{
		public MarketRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<UserRepository> logger) : base(context, mapper, logger)
		{
		}

		public IEnumerable<Market>? GetRecentOpenListings()
		{
			try
			{
				List<Tmarket>? listings = _set.Where(x => !x.IsSold)
					.OrderByDescending(x => x.Id)
					.ToList();
				return _mapper.Map<List<Market>>(listings);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}
	}
}
