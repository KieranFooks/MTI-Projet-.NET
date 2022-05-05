using API.Dbo;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
	public class ItemService : IItemService
	{
		private readonly IItemRepository _itemRepository;

		public ItemService(IItemRepository itemRepository)
		{
			_itemRepository = itemRepository;
		}

		public Item? GetById(int id)
		{
			return _itemRepository.GetById(id);
		}

		public async Task<IEnumerable<Item>?> GetAll()
		{
			return await _itemRepository.Get();
		} 
	}
}
