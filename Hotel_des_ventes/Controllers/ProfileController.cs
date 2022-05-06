using API.Services.Interfaces;
using AutoMapper;
using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_des_ventes.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarketService _marketService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(ILogger<HomeController> logger, IMarketService marketService, IUserService userService, IItemService itemService, IMapper mapper)
        {
            _logger = logger;
            _marketService = marketService;
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Items()
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
            {
                var errorMessage = "You must be logged in to access your profile";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return RedirectToAction("Login", "Connection");
            }
            int userId;
            try
            {
                userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);
            }
            catch
            {
                var errorMessage = "Cannot parse UserID to int";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                return View();
            }

            string username;
            var dbUser = _userService.GetUserById(userId);
            if (dbUser == null)
            {
                var errorMessage = "User not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                username = "";
            }
            else
            {
                username = dbUser.Name;
            }

            List<ItemViewModel> inventory;
            var dbInventory = _userService.GetUserInventory(userId);
            if (dbInventory == null)
            {
                var errorMessage = "Inventory not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                inventory = new List<ItemViewModel>();
            }
            else
            {
                inventory = _mapper.Map<List<ItemViewModel>>(dbInventory);
            }

            ProfileViewModel profile = new ProfileViewModel() { Username = username, Items = inventory };
            return View(profile);
        }

        public IActionResult Announces()
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
            {
                var errorMessage = "You must be logged in to access your profile";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return RedirectToAction("Login", "Connection");
            }
            int userId;
            try
            {
                userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);
            }
            catch
            {
                var errorMessage = "Cannot parse UserID to int";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                return View();
            }

            string username;
            var dbUser = _userService.GetUserById(userId);
            if (dbUser == null)
            {
                var errorMessage = "User not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                username = "";
            }
            else
            {
                username = dbUser.Name;
            }

            List<AnnouncesModel> announces;
            var dbMarket = _marketService.GetMarketHistoryByUserId(userId);
            if (dbMarket == null)
            {
                var errorMessage = "Announces not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
                announces = new List<AnnouncesModel>();
            }
            else
            {
                announces = _mapper.Map<List<AnnouncesModel>>(dbMarket);
            }

            ProfileViewModel profile = new ProfileViewModel() { Username = username, Announces = announces };
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
