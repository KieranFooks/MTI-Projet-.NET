using API.DataAccess;
using API.Dbo;
using AutoMapper;

namespace API.Repositories
{
	public class AutomapperProfiles : Profile
	{
		public AutomapperProfiles()
		{
			CreateMap<Tinventory, Inventory>();
			CreateMap<Inventory, Tinventory>();

			CreateMap<Tuser, User>();
			CreateMap<User, Tuser>();

			CreateMap<Titem, Item>();
			CreateMap<Item, Titem>();

			CreateMap<Tmarket, Market>();
			CreateMap<Market, Tmarket>();
		}
	}
}
