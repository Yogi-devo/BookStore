using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using BookStore.Service;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public HomeController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [ViewData]
        public string CustoProperty { get; set; } //its for ViewData Property

        [ViewData]
        public string Title { get; set; } //its ViewData Property for setting title dynamically without rwiting at view page
        public ViewResult Index()
        {
            var userId = _userService.GetUserId();
            var IsLoggedIn = _userService.IsAuthenticated();
            var alert = _configuration.GetValue<bool>("NewBookAllert:DisplayNewBookAllert");
            var bookName = _configuration.GetValue<string>("NewBookAllert:BookName");

            ViewBag.Title = 123;
            ViewBag.Title = "Yogi Tech";
            dynamic data = new ExpandoObject();
            data.id = 1;
            data.Name = "Yogendra";
            ViewBag.Data = data;
            //ViewBag.Data = new { id = 1, Name = "Yogi" };
            //return View("TempView/YogiTemp.cshtml");

            ViewData["Property1"] = "Yogendra Yadav";
            ViewData["book"] = new BookModel() { Author = "yogi", id = 1 };
            CustoProperty = "Custom Value"; //its ViewData Property
            Title = "Home Page From Controller";//This title will mapp with layout page directly without writing it at view page
            return View();
        }

        [Route("about-us")]
        public ViewResult AboutUs()

        {
            return View();
        }

        //[HttpGet("contact-us/{name:alpha:minlength(3)}")]
        public ViewResult ContactUs()
        {
            return View();
        }
        [Route("test/a{a}")]
        public string Test(string a)
        {
            return a;
        }
        [Route("test/b{a}")]
        public string Test1(string a)
        {
            return a;
        }
        [Route("test/c{a}")]
        public string Test2(string a)
        {
            return a;
        }
    }
}
