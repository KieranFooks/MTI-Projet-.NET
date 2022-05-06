﻿using API.Services.Interfaces;
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
        private readonly IMapper _mapper;

        public ItemController(ILogger<HomeController> logger, IMarketService marketService, IUserService userService, IItemService itemService, IMapper mapper)
        {
            _logger = logger;
            _marketService = marketService;
            _userService = userService;
            _mapper = mapper;
        }
        
        public IActionResult Index(int AnnounceID, string? errorMessage)
        {
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection", new { AnnounceID = AnnounceID });

            try
            {
                int id = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(id);
            }
            catch
            {
                ViewBag.Error = "Cannot parse UserID to int";
            }

            if (errorMessage != null)
                ViewBag.Error = errorMessage;

            var dbItem = _marketService.GetById(AnnounceID);
            var itemOfferModel = _mapper.Map<ItemOfferModel>(dbItem);
            return View(itemOfferModel);
        }

        public IActionResult SellItem(int itemId)
        {
            if (Request.Cookies["UserID"] != null)
            {
                ViewBag.Money = "5000";
            }
            
            if (Request.Cookies["UserID"] == null)
                return RedirectToAction("Login", "Connection");
            var sellItem = new SellItemViewModel() { Id = itemId, Name = "Item 1", AveragePrice= 50 };
            return View(sellItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SellItem(string price, string quantity, int itemId)
        {
            if (Request.Cookies["UserID"] == null)
            {
                ViewBag.Error = "You must be logged in to sell an item";
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                int userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);

                if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity))
                {
                    ViewBag.Error = "Please fill all the fields";
                    return SellItem(itemId);
                }
                try
                {
                    int quantityValue = int.Parse(quantity);
                    int priceValue = int.Parse(price);
                    _marketService.CreateListing(userId!, itemId, quantityValue, priceValue);
                }
                catch
                {
                    ViewBag.Error = "Please fill the area with numbers";
                    return SellItem(itemId);
                }

            }
            catch
            {
                ViewBag.Error = "Cannot parse UserID to int";
                return View();
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int itemId)
        {
            if (Request.Cookies["UserID"] == null)
            {
                ViewBag.Error = "You must be logged in to buy an item";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                int userId = int.Parse(Request.Cookies["UserID"]);
                ViewBag.Money = _userService.GetUserMoney(userId);
                if (!await _marketService.UserBuyListing(userId, itemId))
                {
                    ViewBag.Error = "You don't have enough money";
                }
            }
            catch
            {
                ViewBag.Error = "Cannot parse UserID to int";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
