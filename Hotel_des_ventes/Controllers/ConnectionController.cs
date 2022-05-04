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
            if (AnnounceID != null)
            {
                TempData.Remove("AnnounceID");
                TempData.Add("AnnounceID", AnnounceID);
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(IFormCollection fc)
        {
            if (string.IsNullOrEmpty(fc["Username"]) || string.IsNullOrEmpty(fc["Password"]))
            {
                Console.WriteLine("Login POST empty");
                ViewBag.Error = "Please complete all fields";
                return View();
            }
            else
            {
                string username = fc["Username"];
                string password = fc["Password"];

                //DataBase check
                //using (var db = new DatabaseContext())
                //{
                //    var user = db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
                //    if (user != null)
                //    {
                //        DateTimeOffset dateTime = DateTimeOffset.Now;
                //        dateTime = dateTime.AddDays(1);
                //        CookieOptions option = new CookieOptions();
                //        option.Expires = dateTime;
                //        Response.Cookies.Append("UserID", fc["Username"], option);

                //        if (TempData["AnnounceID"] != null)
                //        {
                //            var AnnounceID = TempData["AnnounceID"];
                //            TempData.Remove("AnnounceID");
                //            return RedirectToAction("Index", "Item", new { AnnounceID = AnnounceID });
                //        }
                //        return RedirectToAction("Index", "Home");
                //    }
                //    else
                //    {
                //        ViewBag.Error = "Wrong username or password";
                //        return View();
                //    }
                //}
            }

            //REMOVE ME

            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = dateTime.AddDays(1);
            CookieOptions option = new CookieOptions();
            option.Expires = dateTime;
            Response.Cookies.Append("UserID", fc["Username"], option);

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
        public IActionResult Register(IFormCollection fc)
        {
            if (string.IsNullOrEmpty(fc["Username"]) || string.IsNullOrEmpty(fc["Password"]) || string.IsNullOrEmpty(fc["ConfirmedPassword"]))
            {
                ViewBag.Error = "Please complete all fields";
                return View();
            }
            else
            {
                //check if Password and ConfirmedPassword are the same
                if (fc["Password"] != fc["ConfirmedPassword"])
                {
                    ViewBag.Error = "Passwords are not the same";
                    return View();
                }
                else
                {
                    //check if Username is already used
                    //using (var db = new DatabaseContext())
                    //{
                    //    var user = db.Users.Where(u => u.Username == fc["Username"]).FirstOrDefault();
                    //    if (user != null)
                    //    {
                    //        ViewBag.Error = "Username already used";
                    //        return View();
                    //    }
                    //    else
                    //    {
                    //        //create new user
                    //        UserModel newUser = new UserModel();
                    //        newUser.Username = fc["Username"];
                    //        newUser.Password = fc["Password"];
                    //
                    //        DateTimeOffset dateTime = DateTimeOffset.Now;
                    //        dateTime = dateTime.AddDays(1);
                    //        CookieOptions option = new CookieOptions();
                    //        option.Expires = dateTime;
                    //        Response.Cookies.Append("UserID", fc["Username"], option);
                    //
                    //        if (TempData["AnnounceID"] != null)
                    //        {
                    //            var AnnounceID = TempData["AnnounceID"];
                    //            TempData.Remove("AnnounceID");
                    //            return RedirectToAction("Index", "Item", new { AnnounceID = AnnounceID });
                    //        }
                    //        return RedirectToAction("Index", "Home");
                    //    }

                    //REMOVE ME
                    DateTimeOffset dateTime = DateTimeOffset.Now;
                    dateTime = dateTime.AddDays(1);
                    CookieOptions option = new CookieOptions();
                    option.Expires = dateTime;
                    Response.Cookies.Append("UserID", fc["Username"], option);

                    if (TempData["AnnounceID"] != null)
                    {
                        var AnnounceID = TempData["AnnounceID"];
                        TempData.Remove("AnnounceID");
                        return RedirectToAction("Index", "Item", new { AnnounceID = AnnounceID });
                    }
                    
                    return RedirectToAction("Index", "Home");

                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
