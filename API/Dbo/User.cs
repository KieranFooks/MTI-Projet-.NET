namespace API.Dbo
{
	public class User : IObjectWithId
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int Money { get; set; }
		public string Password { get; set; } = null!;
	}
}
