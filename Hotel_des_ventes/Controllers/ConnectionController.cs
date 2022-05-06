using API.Services.Interfaces;
using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel_des_ventes.Controllers
{
    public class ConnectionController : Controller
    {
        private readonly ILogger<ConnectionController> _logger;
        private readonly IUserService _userService;

        public ConnectionController(ILogger<ConnectionController> logger, IUserService userService)
        {
            _userService = userService;
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
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Please complete all fields";
                return View();
            }
            //TODO

            var user = _userService.Connect(Username, Password);
            if (user == null)
            {
                ViewBag.Error = "Wrong username or password";
                return View();
            }
                
            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = dateTime.AddDays(1);
            CookieOptions option = new CookieOptions();
            option.Expires = dateTime;
            Response.Cookies.Append("UserID", user.Id.ToString(), option);
            
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
        public async Task<IActionResult> Register(string Username, string Password, string ConfirmedPassword)
        {
            //add verif mdp et userId
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Please complete all fields";
                return View();
            }
            if (Password != ConfirmedPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }
            if (!_userService.IsNameAvailable(Username))
            {
                ViewBag.Error = "Username is not available";
                return View();
            }

            var new_user = await _userService.CreateUser(Username, Password);
            if (new_user == null)
            {
                ViewBag.Error = "Error in database";
                return View();
            }

            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = dateTime.AddDays(1);
            CookieOptions option = new CookieOptions();
            option.Expires = dateTime;
            Response.Cookies.Append("UserID", new_user.Id.ToString(), option);

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
