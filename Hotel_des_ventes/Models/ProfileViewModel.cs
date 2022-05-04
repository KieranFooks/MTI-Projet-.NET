namespace Hotel_des_ventes.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public int Money { get; set; }
        public IEnumerable<ItemViewModel> Items { get; set; }
        public IEnumerable<AnnouncesModel> Announces { get; set; }

    }
}
