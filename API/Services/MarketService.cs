using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class MarketService: IMarketService
	{
		private readonly IMarketRepository _marketRepository;
		public MarketService(IMarketRepository marketRepository)
		{
			_marketRepository = marketRepository;
		}

		public Market? GetById(int id)
		{
			return _marketRepository.GetById(id);
		}

		public IEnumerable<Market>? GetMarketHistoryByUserId(int userId)
		{
			return _marketRepository.GetMarketHistoryByUserId(userId);
		}

		public IEnumerable<Market>? GetOpenListingsByItemName(string itemName)
		{
			return _marketRepository.GetOpenListingsByItemName(itemName);
		}

		public IEnumerable<Market>? GetRecentOpenListings()
		{
			return _marketRepository.GetRecentOpenListings();
		}
	}
}
