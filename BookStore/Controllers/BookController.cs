﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var gab= await _bookRepository.GetAllBooks();
            return View(gab);
        }
        public async Task<ViewResult> GetBook(int id)
        {
            var bookdtl = await _bookRepository.GetBookById(id);
            return View(bookdtl);
        }
        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        public ViewResult AddNewBooks(bool isSuccess=false,int bookId=0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> AddNewBooks(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBooks), new { isSuccess = true, bookId = id });
                }
            }

            //ViewBag.IsSuccess = false;
            //ViewBag.BookId = 0;  this error can handel at view by ViewBag.IsSuccess==true
            return View();
        }
    }
}
