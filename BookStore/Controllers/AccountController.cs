﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if(!result.Succeeded)
                {
                    foreach(var errormessage in result.Errors)
                    {
                        ModelState.AddModelError("", errormessage.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
            }
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task< IActionResult> Login(SignInModel signInModel,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignIn(signInModel);

                 if(result.Succeeded)
                 {
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                 }
                ModelState.AddModelError("", "Invalid Credentials");
            }
                
            return View();
        }

        public async Task<IActionResult> Logout()
        {
           await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}