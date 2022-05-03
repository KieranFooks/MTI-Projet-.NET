using API.DataAccess;
using API.Dbo;

namespace API.Repositories.Interfaces
{
	public interface IMarketRepository : IRepository<Tmarket, Market>
	{
		IEnumerable<Market>? GetRecentOpenListings();
	}
}
