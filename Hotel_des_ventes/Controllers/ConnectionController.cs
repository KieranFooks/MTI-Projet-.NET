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
            TempData.Remove("AnnounceID");
            TempData.Add("AnnounceID", AnnounceID);
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Username, string Password)
        {
            //add verif mdp et userId
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Please complete all fields";
                return View();
            }
                
            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = dateTime.AddDays(1);
            CookieOptions option = new CookieOptions();
            option.Expires = dateTime;
            Response.Cookies.Append("UserID", Username, option);
            
            if (TempData["AnnounceID"] != null)
            {
                var AnnounceID = TempData["AnnounceID"];
                TempData.Remove("AnnounceID");
                return RedirectToAction("Index", "Item", new { AnnounceID = AnnounceID });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string Username, string Password, string ConfirmedPassword)
        {
            //add verif mdp et userId
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Please complete all fields";
                return View();
            }
            if (Password != ConfirmedPassword)
            {
                ViewBag.Error = "Veuillez remplir tous les champs";
                return View();

            }
            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = dateTime.AddDays(1);
            CookieOptions option = new CookieOptions();
            option.Expires = dateTime;
            Response.Cookies.Append("UserID", Username, option);

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
