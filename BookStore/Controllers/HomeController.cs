﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController:Controller
    {
        public ViewResult Index()

        {
            //return View("TempView/YogiTemp.cshtml");
            return View();
        }
        public ViewResult About()

        {
            return View();
        }
        public ViewResult ContactUs()
                   {
            return View();
        }
    }
}
