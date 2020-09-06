using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController:Controller
    {
        [ViewData]
        public string CustoProperty { get; set; } //its for ViewData Property

        [ViewData]
        public string Title { get; set; } //its ViewData Property for setting title dynamically without rwiting at view page
        public ViewResult Index()

        {
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
            CustoProperty  = "Custom Value"; //its ViewData Property
            Title = "Home Page From Controller";//This title will mapp with layout page directly without writing it at view page
            return View();
        }
        public ViewResult AboutUs()

        {
            return View();
        }
        public ViewResult ContactUs()
        {           
            return View();
        }
    }
}
