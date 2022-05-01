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

		public virtual async Task<bool> Delete(long idEntity)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<IEnumerable<ModelEntity>> Get(string includeTables)
		{
			try
			{
				List<DBEntity>? query = null;
				if (String.IsNullOrEmpty(includeTables))
				{
					query = await _set.AsNoTracking().ToListAsync();
				}
				else
				{
					query = await _set.Include(includeTables).AsNoTracking().ToListAsync();
				}

				return _mapper.Map<ModelEntity[]>(query);
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public virtual async Task<ModelEntity?> Insert(ModelEntity entity)
		{
			DBEntity dbEntity = _mapper.Map<DBEntity>(entity);
			_set.Add(dbEntity);
			try
			{
				await _context.SaveChangesAsync();
				ModelEntity newEntity = _mapper.Map<ModelEntity>(dbEntity);
				return newEntity;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public virtual async Task<ModelEntity> Update(ModelEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
