namespace API.Dbo
{
	public class Item : IObjectWithId
    {
        public Item()
        {
            Inventories = new HashSet<Inventory>();
            Markets = new HashSet<Market>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
