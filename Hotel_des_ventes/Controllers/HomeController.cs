using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel_des_ventes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? itemId)
        {
            var items = new List<ItemViewModel>();
            items.Add(new ItemViewModel() { Id = -1, Name = "None" });

            items.Add(new ItemViewModel() { Id = 0, Name = "Emeraude" });
            items.Add(new ItemViewModel() { Id = 1, Name = "Rubis" });
            items.Add(new ItemViewModel() { Id = 2, Name = "Saphyr" });
            items.Add(new ItemViewModel() { Id = 3, Name = "Stone" });

            var selectedItem = -1;
            
            var announces = new List<AnnouncesModel>();

            if (itemId != null && itemId != -1)
            {
                selectedItem = (int)itemId;
                for (int i = 0; i < 10; i++)
                    announces.Add(new AnnouncesModel() { Id = 1, Item = "Rubis", Seller = "Hugo", Price = 10, Quantity = 10 });

            }
            else
            {
                for (int i = 0; i < 10; i++)
                    announces.Add(new AnnouncesModel() { Id = 1, Item = "Emeraude", Seller = "Eliott", Price = 10, Quantity = 10 });
            }
            var homeViewModel = new HomeViewModel() { selectedItem = selectedItem, Announces = announces, Items = items };
            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}