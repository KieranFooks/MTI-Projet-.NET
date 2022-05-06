namespace API.Dbo
{
	public class User : IObjectWithId
    {
        public User()
        {
            Inventories = new HashSet<Inventory>();
            Markets = new HashSet<Market>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Money { get; set; }
        public string Password { get; set; } = null!;

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
