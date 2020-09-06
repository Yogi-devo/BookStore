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
