using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IMarketRepository : IRepository<Tmarket, Market>
	{
		IEnumerable<Market>? GetRecentOpenListings();
		Market? GetById(int id);
		IEnumerable<Market>? GetOpenListingsByItemId(int itemId);
		IEnumerable<Market>? GetMarketHistoryByUserId(int userId);
		public bool RemoveListing(int id);
	}
}
