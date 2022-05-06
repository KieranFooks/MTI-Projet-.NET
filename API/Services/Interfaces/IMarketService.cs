using API.Dbo;

namespace API.Services.Interfaces
{
	public interface IMarketService
	{
		IEnumerable<Market>? GetRecentOpenListings();
		Market? GetById(int id);
		IEnumerable<Market>? GetOpenListingsByItemId(int itemId);
		IEnumerable<Market>? GetMarketHistoryByUserId(int userId);
		Market? CreateListing(int userId, int itemId, int quantity, int price);
		Task<bool> UserBuyListing(int buyerId, int marketId);
	}
}
