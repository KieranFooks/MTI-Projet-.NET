namespace Hotel_des_ventes.Models
{
    public class HomeViewModel
    {
        public int selectedItem { get; set; }
        public List<AnnouncesModel> Announces { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}
