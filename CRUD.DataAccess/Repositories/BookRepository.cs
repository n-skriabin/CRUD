using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CRUD.DataAccess.Repositories
{
    public class BookRepository
    {
        private ContextModel _db;

        public BookRepository(string ConnectionString)
        {
            _db = new ContextModel(ConnectionString);
        }

        public void Create(Book book, List<Guid> authorsListId)
        {
            foreach(var authorID in authorsListId)
            {
                var bookAuthor = new BooksAuthors
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    AuthorId = authorID,
                };
                _db.BooksAuthors.Add(bookAuthor);
            }

            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void Update(Book newRecord, List<Guid> authorsListIds)
        {
            DeleteBook(newRecord.Id);

            foreach (var authorId in authorsListIds)
            {
                var bookAuthor = new BooksAuthors
                {
                    Id = Guid.NewGuid(),
                    BookId = newRecord.Id,
                    AuthorId = authorId,
                };
                _db.BooksAuthors.Add(bookAuthor);
            }

            _db.Entry(newRecord).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public List<Book> Read()
        {
            return _db.Books.ToList();
        }

        public void Delete(Guid BookId)
        {
            var listForDelete = _db.BooksAuthors.Where(ba => ba.BookId == BookId).ToList();

            foreach (var record in listForDelete)
            {
                _db.BooksAuthors.Remove(record);
            }

            var recordBookForDelete = _db.Books.Where(b => b.Id == BookId).FirstOrDefault();
            _db.Books.Remove(recordBookForDelete);
            _db.SaveChanges();
        }

        public void DeleteBook(Guid BookId)
        {
            var booksauthors = _db.BooksAuthors.Where(ba => ba.BookId == BookId).ToList();

            foreach (var book in booksauthors)
            {
                _db.BooksAuthors.Remove(book);    
            }
            _db.SaveChanges();
        }

        public List<Book> GetBooks(List<Guid> booksListIds)
        {
            var booksList = new List<Book>();
            foreach (var bookId in booksListIds)
            {
                booksList.Add(_db.Books.Where(b => b.Id == bookId).FirstOrDefault());
            }
            return booksList;
        }

        public List<Book> GetBooks(Guid PublisherId)
        {

            var booksList = _db.Books.Where(b => b.PublisherId == PublisherId).ToList();

            return booksList;
        }
    }
}
