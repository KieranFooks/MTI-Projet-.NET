using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel_des_ventes.Controllers
{
    public class ConnectionController : Controller
    {
        private readonly ILogger<ConnectionController> _logger;

        public ConnectionController(ILogger<ConnectionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(int? AnnounceID)
        {
            TempData.Add("AnnounceID", AnnounceID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(IFormCollection fc)
        {
            //add verif mdp et userId
            if (ModelState.IsValid)
            {
                DateTimeOffset dateTime = DateTimeOffset.Now;
                dateTime = dateTime.AddDays(1);
                CookieOptions option = new CookieOptions();
                option.Expires = dateTime;
                Response.Cookies.Append("UserID", fc["Id"], option);
            }
            if (TempData["AnnounceID"] != null)
            {
                var AnnounceID = TempData["AnnounceID"];
                TempData.Remove("AnnounceID");
                return RedirectToAction("Index", "Item", new { AnnounceID = AnnounceID });
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
