using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains (authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
             new BookModel(){id=1,Title="MVC",Author="Yogendra"},
             new BookModel(){id=2,Title=".netCore",Author="Nitiesh"},
             new BookModel(){id=3,Title="Java",Author="Sumen"},
             new BookModel(){id=4,Title="PHP",Author="Sunny"},
             new BookModel(){id=5,Title="Angular",Author="Sumit"},
            };
        }
            
    }

}
