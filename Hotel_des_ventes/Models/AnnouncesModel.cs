namespace Hotel_des_ventes.Models
{
    public class AnnouncesModel
    {
        public int Id { get; set; }
        public string Seller { get; set; }
        public string Item { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool Is_Sold { get; set; }
    }
}
