using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Repository
{

    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        //public int AddNewBook(BookModel model)
        //{
        //    var newBook = new Books()
        //    {
        //        Author = model.Author,
        //        Title = model.Title,
        //        Description = model.Description,
        //        TotalPages = model.TotalPages,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOn=DateTime.UtcNow,
        //        Category=model.Category,
        //        Language=model.Language
        //    };
        //    _context.Books.Add(newBook);
        //    _context.SaveChanges();
        //    return newBook.id;
        //}
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Category = model.Category,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            //var gallery = new List<BookGallery>();
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        //Language = book.Language.Name,
                        TotalPages = book.TotalPages,
                        Title = book.Title,
                        id = book.id,
                        CoverImageUrl = book.CoverImageUrl
                    });
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count) //its for View Component
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    //Language = book.Language.Name,
                    TotalPages = book.TotalPages,
                    Title = book.Title,
                    id = book.id,
                    CoverImageUrl = book.CoverImageUrl
                }).Take(count).ToListAsync();
        }



        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.id == id)
                .Select(book => new BookModel()

                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    TotalPages = book.TotalPages,
                    Title = book.Title,
                    id = book.id,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();


            //return DataSource().Where(x => x.id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
            //return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains (authorName)).ToList();
        }

        public string GetAppName()
        {
            return "Book Store App";
        }

    }

}
