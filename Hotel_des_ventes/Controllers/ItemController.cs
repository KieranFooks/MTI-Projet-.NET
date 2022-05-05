using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_des_ventes.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index(int AnnounceID, string? errorMessage)
        {
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection", new { AnnounceID = AnnounceID });
            if (errorMessage != null)
                ViewBag.Error = errorMessage;

            ItemOfferModel itemOfferModel = new ItemOfferModel() { Id = AnnounceID, Name = "Item 1", Price = 100, Quantity = 10, Seller = "Seller 1" };
            return View(itemOfferModel);
        }

        public IActionResult SellItem(int itemId)
        {
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection");
            var sellItem = new SellItemViewModel() { Id = itemId, Name = "Item 1", AveragePrice= 50 };
            return View(sellItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SellItem(string price, string quantity, int itemId)
        {
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity))
            {
                ViewBag.Error = "Please fill all the fields";
                return SellItem(itemId);
            }
            try
            {
                int quantityValue = int.Parse(quantity);
                int priceValue = int.Parse(price);
            }
            catch
            {
                ViewBag.Error = "Please fill the area with numbers";
                return SellItem(itemId);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy(int itemId)
        {
            //TODO: Check if the user as enough money
            //if ()
            //return RedirectToAction("Index", new { AnnounceID = int.Parse(fc["Id"]), errorMessage = "You don't have the funds to buy this item" });
            return RedirectToAction("Index", "Home");
        }
    }
}
