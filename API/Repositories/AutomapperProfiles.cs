using AutoMapper;

namespace API.Repositories
{
	public class AutomapperProfiles : Profile
	{
		public AutomapperProfiles()
		{
			CreateMap<DataAccess.Inventory, Dbo.Inventory>();
			CreateMap<Dbo.Inventory, DataAccess.Inventory>();

			CreateMap<DataAccess.User, Dbo.User>();
			CreateMap<Dbo.User, DataAccess.User>();

			CreateMap<DataAccess.Item, Dbo.Item>();
			CreateMap<Dbo.Item, DataAccess.Item>();

			CreateMap<DataAccess.Market, Dbo.Market>();
			CreateMap<Dbo.Market, DataAccess.Market>();
		}
	}
}
