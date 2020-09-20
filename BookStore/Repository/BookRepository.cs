using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
   
    public class BookRepository
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
                TotalPages = model.TotalPages.HasValue? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Category = model.Category,
                Language = model.Language
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any()==true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                     Author=book.Author,
                     Category=book.Category,
                     Description=book.Description,
                     Language=book.Language,
                     TotalPages=book.TotalPages,
                     Title=book.Title,
                     id=book.id,
                    });
                }
            }
            return books;
        }
        public  async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book!=null)
            {
                var bookDetails=new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Language = book.Language,
                    TotalPages = book.TotalPages,
                    Title = book.Title,
                    id = book.id,
                };
                return (bookDetails);
            }
            return null;
            //return DataSource().Where(x => x.id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains (authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
             new BookModel(){id=1,Title="MVC",Author="Yogendra", Description="This is description of MVC Book", Category="Programing",Language="English",TotalPages=250},
             new BookModel(){id=2,Title=".netCore",Author="Nitiesh" , Description="This is description of .NetCore Book",Category="Development",Language="US English",TotalPages=450},
             new BookModel(){id=3,Title="Java",Author="Sunny", Description="This is description of Java Book",Category="Learning",Language="Japani",TotalPages=150},
             new BookModel(){id=4,Title="PHP",Author="Sumen", Description="This is description of PHP Book",Category="Training",Language="Hindi",TotalPages=350},
             new BookModel(){id=5,Title="Angular",Author="Sumit", Description="This is description of Angular Book",Category="Demo",Language="English",TotalPages=220},
             new BookModel(){id=6,Title="C#",Author="Venket", Description="This is description of C# Book",Category="Skill",Language="Russian",TotalPages=155},
            };
        }
            
    }

}
