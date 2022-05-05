using API.DataAccess;
using API.Dbo;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class MarketRepository : Repository<Tmarket, Market>, IMarketRepository
	{
		public MarketRepository(Hotel_des_ventesContext context, IMapper mapper, ILogger<MarketRepository> logger) : base(context, mapper, logger)
		{
		}

		public Market? GetById(int id)
		{
			try
			{
				Tmarket? listing = _set
					.AsNoTracking()
					.FirstOrDefault(x => x.Id == id);
				return _mapper.Map<Market?>(listing);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public IEnumerable<Market>? GetOpenListingsByItemName(string itemName)
		{
			try
			{
				List<Tmarket>? listings = _set
					.Where(x => !x.IsSold)
					.Where(x => x.IdItemNavigation.Name == itemName)
					.OrderByDescending(x => x.Id)
					.AsNoTracking()
					.ToList();
				return _mapper.Map<List<Market>>(listings);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public IEnumerable<Market>? GetMarketHistoryByUserId(int userId)
		{
			try
			{
				List<Tmarket>? listings = _set
					.Where(x => x.IdNavigation.Id == userId)
					.OrderByDescending(x => x.Id)
					.AsNoTracking()
					.ToList();
				return _mapper.Map<List<Market>>(listings);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public IEnumerable<Market>? GetRecentOpenListings()
		{
			try
			{
				List<Tmarket>? listings = _set
					.Where(x => !x.IsSold)
					.OrderByDescending(x => x.Id)
					.Take(10)
					.AsNoTracking()
					.ToList();
				return _mapper.Map<List<Market>>(listings);
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return null;
			}
		}

		public bool RemoveListing(int id)
		{
			try
			{
				Tmarket? listing = _set.Find(id);
				if (listing == null)
				{
					return false;
				}

				listing.IsSold = true;
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("error on db", ex);
				return false;
			}
		}
	}
}
