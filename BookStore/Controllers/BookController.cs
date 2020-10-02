using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository, LanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
       
        
        public async Task<ViewResult> AddNewBooks(bool isSuccess=false,int bookId=0)
        {
            var model = new BookModel()
            {
                //Language = "English"
            };
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            //ViewBag.Language = new List<string> {"English", "Hindi" ,"Dutch"};
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task< IActionResult> AddNewBooks(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                if(bookModel.CoverPhoto!=null)
                {
                    string folder = "books/cover/";
                    folder += Guid.NewGuid().ToString() + "_" +bookModel.CoverPhoto.FileName;
                    bookModel.CoverImageUrl = "/"+folder;
                    string serverPholder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverPholder, FileMode.Create));
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBooks), new { isSuccess = true, bookId = id });
                }
            }
            ViewBag.Language = new SelectList (await _languageRepository.GetLanguages(),"Id","Name" );
            //ViewBag.Language = new List<string> { "English", "Hindi", "Dutch" };
            //ViewBag.IsSuccess = false;
            //ViewBag.BookId = 0;  this error can handel at view by ViewBag.IsSuccess==true
            return View();
        }

        
    }
}
