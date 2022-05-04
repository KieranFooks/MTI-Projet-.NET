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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy(IFormCollection fc)
        {
            //TODO: Check if the user as enough money
            //if ()
            //return RedirectToAction("Index", new { AnnounceID = int.Parse(fc["Id"]), errorMessage = "You don't have the funds to buy this item" });
            return RedirectToAction("Index", "Home");
        }
    }
}
