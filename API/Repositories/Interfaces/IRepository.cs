namespace API.Repositories.Interfaces
{
	public interface IRepository<DBEntity, ModelEntity>
	{
		Task<IEnumerable<ModelEntity>?> Get(string includeTables = "");
		Task<ModelEntity?> Insert(ModelEntity entity);
	}
}
