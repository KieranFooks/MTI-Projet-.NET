using API.Dbo;

namespace API.Services.Interfaces
{
	public interface IMarketService
	{
		IEnumerable<Market>? GetRecentOpenListings();
	}
}
