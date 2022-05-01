using AutoMapper;

namespace API.Repositories
{
	public class AutomapperProfiles : Profile
	{
		public AutomapperProfiles()
		{
			CreateMap<DataAccess.Tinventory, Dbo.Inventory>();
			CreateMap<Dbo.Inventory, DataAccess.Tinventory>();

			CreateMap<DataAccess.Tuser, Dbo.User>();
			CreateMap<Dbo.User, DataAccess.Tuser>();

			CreateMap<DataAccess.Titem, Dbo.Item>();
			CreateMap<Dbo.Item, DataAccess.Titem>();

			CreateMap<DataAccess.Tmarket, Dbo.Market>();
			CreateMap<Dbo.Market, DataAccess.Tmarket>();
		}
	}
}
