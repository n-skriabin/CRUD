using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CRUD.DataAccess.Repositories
{
    public class BookRepository
    {
        private IDbConnection _db;

        public BookRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Book> Read()
        {
            var books = _db.Query<Book>("SELECT * FROM Books").ToList();
            return books;
        }

        public void Create(Book book, List<Guid> authorsListIds)
        {
            AddBookInBooksAuthors(book, authorsListIds);

            _db.Query("INSERT INTO Books (Id, Name, Year) VALUES ('"+book.Id+"', '"+book.Name+"', '"+book.Year+"')");
        }

        public void Update(Book newRecord, List<Guid> authorsListIds)
        {
            DeleteBook(newRecord.Id);

            AddBookInBooksAuthors(newRecord, authorsListIds);

            _db.Execute("UPDATE Books SET Name = @Name, Year = @Year WHERE Id = @Id", newRecord);
        }

        public void Delete(Guid bookId)
        {
            DeleteBook(bookId);
            string stringBookId = bookId.ToString();
            _db.Query<Book>("DELETE FROM Books WHERE Id = @stringBookId", new { stringBookId });
        }

        public void DeleteBook(Guid bookId)
        {
            var stringBookId = bookId.ToString();
            _db.Query<BooksAuthors>("DELETE FROM BooksAuthors WHERE BookId = @stringBookId", new { stringBookId });
        }

        public List<Book> GetBooks(List<Guid> booksListIds)
        {
            var arrayIds = booksListIds.ToArray();
            var booksList = _db.Query<Book>("SELECT * FROM Books WHERE Id IN @arrayIds", arrayIds).ToList();
            return booksList;
        }

        public List<Book> GetBooks(Guid publisherId)
        {
            var booksList = _db.Query<Book>("SELECT * FROM Books WHERE PublisherId = @publisherId", new { publisherId }).ToList();
            return booksList;
        }

        public void AddBookInBooksAuthors(Book book, List<Guid> authorsListId)
        {
            foreach (var authorID in authorsListId)
            {
                var bookAuthor = new BooksAuthors
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    AuthorId = authorID,
                };
                _db.Query<BooksAuthors>("INSERT INTO BooksAuthors (Id, BookId, AuthorId) VALUES (@Id, @BookId, @AuthorId)", bookAuthor);
            }
        }
    }
}
