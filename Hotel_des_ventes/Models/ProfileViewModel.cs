namespace Hotel_des_ventes.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public IEnumerable<ItemViewModel> Items { get; set; }
        public IEnumerable<AnnouncesModel> Announces { get; set; }

    }
}
