using CRUD.DataAccess.Interfaces;
using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CRUD.DataAccess.Repositories
{
    public class BookRepository : IRepositoryBook
    {
        ContextModel db;

        public BookRepository(string ConnectionString)
        {
            db = new ContextModel(ConnectionString);
        }

        public void Create(Book book, List<Guid> authorsListId)
        {
            for (int i = 0; i < authorsListId.Count; i++)
            {
                var bookAuthor = new BooksAuthors
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    AuthorId = authorsListId[i],
                };
                db.BooksAuthors.Add(bookAuthor);
            }

            db.Books.Add(book);
            db.SaveChanges();
        }

        public void Update(Book newRecord, List<Guid> authorsListId)
        {
            DeleteBook(newRecord.Id);

            for (int i = 0; i < authorsListId.Count; i++)
            {
                var bookAuthor = new BooksAuthors
                {
                    Id = Guid.NewGuid(),
                    BookId = newRecord.Id,
                    AuthorId = authorsListId[i],
                };
                db.BooksAuthors.Add(bookAuthor);
            }

            db.Entry(newRecord).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Book> Read()
        {
            return db.Books.ToList();
        }

        public void Delete(Guid BookId)
        {
            var booksauthors = db.BooksAuthors.ToList();

            foreach (var book in booksauthors)
            {
                if (book.BookId == BookId)
                {
                    db.BooksAuthors.Remove(book);
                }
            }
            var recordBookForDelete = db.Books.Where(b => b.Id == BookId).First();
            db.Books.Remove(recordBookForDelete);
            db.SaveChanges();
        }

        public void DeleteBook(Guid BookId)
        {
            var booksauthors = db.BooksAuthors.ToList();

            foreach (var book in booksauthors)
            {
                if (book.BookId == BookId)
                {
                    db.BooksAuthors.Remove(book);
                }
            }
            db.SaveChanges();
        }

        public List<Book> GetBooks(List<Guid> booksListId)
        {
            var booksList = new List<Book>();
            foreach (var bookId in booksListId)
            {
                booksList.Add(db.Books.Where(b => b.Id == bookId).First());
            }
            return booksList;
        }

        public List<Book> GetBooks(Guid PublisherId)
        {

            var books = Read();
            var booksList = new List<Book>();

            foreach (var book in books)
            {
                if (book.PublisherId == PublisherId)
                {
                    booksList.Add(book);
                }
            }

            return booksList;
        }
    }
}
