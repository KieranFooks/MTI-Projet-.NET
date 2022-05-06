using API.Services.Interfaces;
using AutoMapper;
using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_des_ventes.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarketService _marketService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(ILogger<HomeController> logger, IMarketService marketService, IUserService userService, IItemService itemService, IMapper mapper)
        {
            _logger = logger;
            _marketService = marketService;
            _userService = userService;
            _itemService = itemService;
            _mapper = mapper;
        }

        public IActionResult Index(int AnnounceID)
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection", new { AnnounceID = AnnounceID });

            try
            {
                int id = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(id);
            }
            catch
            {
                var errorMessage = "Cannot parse UserID to int";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
            }

            var dbItem = _marketService.GetById(AnnounceID);
            ItemOfferModel itemOfferModel;
            if (dbItem == null)
            {
                var message = "Error in database: Announce not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + message : message;
                itemOfferModel = new ItemOfferModel();
            }
            else
            {
                itemOfferModel = _mapper.Map<ItemOfferModel>(dbItem);
            }
            return View(itemOfferModel);
        }

        public IActionResult SellItem(int itemId)
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection");
            
            try
            {
                int id = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(id);
            }
            catch
            {
                var errorMessage = "Cannot parse UserID to int";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error + "\n" + errorMessage : errorMessage;
            }


            var dbItem = _marketService.GetAveragePriceByItemId(itemId);
            SellItemViewModel sellItemViewModel;
            if (dbItem == null)
            {
                var errorMessage = "Error in database: Item not found";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                sellItemViewModel = new SellItemViewModel();
            }
            else
            {
                sellItemViewModel = _mapper.Map<SellItemViewModel>(dbItem);
            }
            return View(sellItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SellItem(string price, string quantity, int itemId)
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
            {
                var errorMessage = "You must be logged in to sell an item";
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
                return View(itemId);
            }

            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity))
            {
                var errorMessage = "Please fill all the fields";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return SellItem(itemId);
            }
            try
            {
                int quantityValue = int.Parse(quantity);
                int priceValue = int.Parse(price);
                var market = _marketService.CreateListing(userId!, itemId, quantityValue, priceValue);
                if (market == null)
                {
                    var errorMessage = "You don't have enough items";
                    ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                    TempData.Remove("errorMessage");
                    TempData.Add("errorMessage", ViewBag.Error);
                    return SellItem(itemId);
                }
            }
            catch
            {
                var errorMessage = "Please fill all the fields with numbers";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return SellItem(itemId);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int AnnounceID)
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
                TempData.Remove("errorMessage");
            }
            
            if (Request.Cookies["UserID"] == null)
            {
                var errorMessage = "You must be logged in to buy an item";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return RedirectToAction("Index", "Home");
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
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return RedirectToAction("Index", new { AnnounceID = AnnounceID });
            }

            if (!await _marketService.UserBuyListing(userId, AnnounceID ))
            {
                var errorMessage = "You don't have enough money";
                ViewBag.Error = ViewBag.Error != null ? ViewBag.Error += "\n" + errorMessage : errorMessage;
                TempData.Remove("errorMessage");
                TempData.Add("errorMessage", ViewBag.Error);
                return RedirectToAction("Index", new { AnnounceID = AnnounceID });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
