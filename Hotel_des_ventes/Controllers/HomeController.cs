using API.Services.Interfaces;
using AutoMapper;
using Hotel_des_ventes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel_des_ventes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarketService _marketService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMarketService marketService, IUserService userService, IItemService itemService, IMapper mapper)
        {
            _logger = logger;
            _marketService = marketService;
            _userService = userService;
            _itemService = itemService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? itemId)
        {
            if (Request.Cookies["UserID"] != null)
            {
                try
                {
                    int id = int.Parse(Request.Cookies["UserID"]);
                    ViewBag.Money = _userService.GetUserMoney(id);
                }
                catch
                {
                    ViewBag.Error = "Cannot parse UserID to int";
                }
            }

            var dbItems = await _itemService.GetAll();
            var items = _mapper.Map<List<ItemViewModel>>(dbItems);
            items.Insert(0, new ItemViewModel() { Id = -1, Name = "None" });

            var selectedItem = -1;

            List<AnnouncesModel> announces;
            if (itemId != null && itemId != -1)
            {
                var dbAnnounces = _marketService.GetOpenListingsByItemId((int)itemId);
                announces = _mapper.Map<List<AnnouncesModel>>(dbAnnounces);
            }
            else
            {
                var dbAnnounces = _marketService.GetRecentOpenListings();
                announces = _mapper.Map<List<AnnouncesModel>>(dbAnnounces);
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