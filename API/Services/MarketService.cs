using API.Dbo;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class MarketService
	{
		private readonly IMarketRepository _marketRepository;
		public MarketService(IMarketRepository marketRepository)
		{
			_marketRepository = marketRepository;
		}

		IEnumerable<Market>? GetRecentOpenListings()
		{
			return _marketRepository.GetRecentOpenListings();
		}
	}
}
