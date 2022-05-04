using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_des_ventes.Controllers
{
    public class ProfileController : Controller
    {
        public int ProfileId { get; set; } = 1;

        public IActionResult Items()
        {
            List<ItemViewModel> items = new();
            List<AnnouncesModel> announces = new List<AnnouncesModel>();
            for (int i = 0; i < 10; i++)
                announces.Add(new AnnouncesModel() { Id = 1, Item = "Emeraude", Seller = "Test", Price = 10, Quantity = 10 });
            for (int i = 0; i < 5; i++)
                items.Add(new ItemViewModel() { Name = "Emeraude", Description = "Une petite emeraude", Quantity = 10 });
            ProfileViewModel profile = new ProfileViewModel() { Username = "Test", Money = 1000, Items = items, Announces = announces };
            return View(profile);
        }

        public IActionResult Announces()
        {
            List<ItemViewModel> items = new();
            List<AnnouncesModel> announces = new List<AnnouncesModel>();
            for (int i = 0; i < 10; i++)
                announces.Add(new AnnouncesModel() { Id = 1, Item = "Emeraude", Seller = "Test", Price = 10, Quantity = 10 });
            for (int i = 0; i < 5; i++)
                items.Add(new ItemViewModel() { Name = "Emeraude", Description = "Une petite emeraude", Quantity = 10 });
            ProfileViewModel profile = new ProfileViewModel() { Username = "Test", Money = 1000, Items = items, Announces = announces };
            return View(profile);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserID");
            return RedirectToAction("Index", "Home");
        }
    }
}
