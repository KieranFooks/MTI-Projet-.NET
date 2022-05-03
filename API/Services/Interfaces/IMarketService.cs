using API.Dbo;

namespace API.Services.Interfaces
{
	public interface IMarketService
	{
		IEnumerable<Market>? GetRecentOpenListings();
		Market? GetById(int id);
		IEnumerable<Market>? GetOpenListingsByItemName(string itemName);
		IEnumerable<Market>? GetMarketHistoryByUserId(int userId);
		Market? CreateListing(int userId, int itemId, int quantity, int price);
	}
}
