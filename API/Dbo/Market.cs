namespace API.Dbo
{
	public class Market : IObjectWithId
    {
        public int Id { get; set; }
        public int IdSeller { get; set; }
        public int IdItem { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsSold { get; set; }

        public virtual Item IdItemNavigation { get; set; } = null!;
        public virtual User IdNavigation { get; set; } = null!;
    }
}
