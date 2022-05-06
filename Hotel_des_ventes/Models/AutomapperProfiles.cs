using API.Dbo;
using AutoMapper;

namespace Hotel_des_ventes.Models
{
    public class AutomapperProfiles : Profile
	{
		public AutomapperProfiles()
		{
			CreateMap<Item, ItemViewModel>()
				.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<Market, ItemOfferModel>()
				.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(d => d.Name, opt => opt.MapFrom(src => src.IdItemNavigation.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(d => d.Seller, opt => opt.MapFrom(src => src.IdNavigation.Name));

			CreateMap<Market, AnnouncesModel>()
				.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(d => d.Item, opt => opt.MapFrom(src => src.IdItemNavigation.Name))
				.ForMember(d => d.Seller, opt => opt.MapFrom(src => src.IdNavigation.Name))
                .ForMember(d => d.Is_Sold, opt => opt.MapFrom(src => src.IsSold));

            CreateMap<Inventory, ItemViewModel>()
				.ForMember(d => d.Id, opt => opt.MapFrom(src => src.IdItem))
				.ForMember(d => d.Name, opt => opt.MapFrom(src => src.IdItemNavigation.Name))
				.ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(d => d.Description, opt => opt.MapFrom(src => src.IdItemNavigation.Description));


        }
	}
}
