using API.DataAccess;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class Repository<DBEntity, ModelEntity> : IRepository<DBEntity, ModelEntity>
		where DBEntity : class, new()
		where ModelEntity : class, new()
	{
		protected DbSet<DBEntity> _set;
		protected Hotel_des_ventesContext _context;
		protected readonly IMapper _mapper;

		public Repository(Hotel_des_ventesContext context, IMapper mapper)
		{
			_mapper = mapper;
			_context = context;
			_set = context.Set<DBEntity>();
		}

		Task<bool> IRepository<DBEntity, ModelEntity>.Delete(long idEntity)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<ModelEntity>> IRepository<DBEntity, ModelEntity>.Get(string includeTables)
		{
			throw new NotImplementedException();
		}

		Task<ModelEntity> IRepository<DBEntity, ModelEntity>.Insert(ModelEntity entity)
		{
			throw new NotImplementedException();
		}

		Task<ModelEntity> IRepository<DBEntity, ModelEntity>.Update(ModelEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
