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
				//.ForMember(d => d.Item, opt => opt.MapFrom(src => src.IdItemNavigation))
				//.ForMember(d => d.Seller, opt => opt.MapFrom(src => src.IdNavigation));
			CreateMap<Market, Tmarket>();
				//.ForMember(d => d.IdItem, opt => opt.MapFrom(src => src.Item.Id))
				//.ForMember(d => d.IdSeller, opt => opt.MapFrom(src => src.Seller.Id));
		}
	}
}
