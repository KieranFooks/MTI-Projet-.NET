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
            if (Request.Cookies["UserID"] == null)
            {
                ViewBag.Error = "You must be logged in to access your profile";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                int userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);
                var dbUser = _userService.GetUserById(userId);
                var dbInventory = _userService.GetUserInventory(userId);
                var dbMarket = _marketService.GetMarketHistoryByUserId(userId);
                var username = dbUser.Name;
                var inventory = _mapper.Map<List<ItemViewModel>>(dbInventory);
                var announces = _mapper.Map<List<AnnouncesModel>>(dbMarket);
                ProfileViewModel profile = new ProfileViewModel() { Username = username, Items = inventory, Announces = announces };
                return View(profile);
            }
            catch
            {
                ViewBag.Error = "Cannot parse UserID to int";
                return View();
            }
        }

        public IActionResult Announces()
        {
            if (Request.Cookies["UserID"] == null)
            {
                ViewBag.Error = "You must be logged in to access your profile";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                int userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);
                var dbUser = _userService.GetUserById(userId);
                var dbInventory = _userService.GetUserInventory(userId);
                var dbMarket = _marketService.GetMarketHistoryByUserId(userId);
                var username = dbUser.Name;
                var inventory = _mapper.Map<List<ItemViewModel>>(dbInventory);
                var announces = _mapper.Map<List<AnnouncesModel>>(dbMarket);
                ProfileViewModel profile = new ProfileViewModel() { Username = username, Items = inventory, Announces = announces };
                return View(profile);
            }
            catch
            {
                ViewBag.Error = "Cannot parse UserID to int";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserID");
            return RedirectToAction("Index", "Home");
        }
    }
}
