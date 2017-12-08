using CRUD.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Repositories
{
    public class AuthorRepository
    {
        private IDbConnection _db;

        public AuthorRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Author> Read()
        {
            var authors = _db.Query<Author>("SELECT * FROM Authors").ToList();
            return authors;
        }

        public void Create(Author author)
        {

            _db.Query<Author>("INSERT INTO Authors (Id, FirstName, LastName, Patronymic, Abbreviated) VALUES(@Id, @FirstName, @LastName, @Patronymic, @Abbreviated);", author);
        }

        public void Delete(Guid AuthorId)
        {
            var deletingAuthorBooks = _db.Query<BooksAuthors>("SELECT * FROM BooksAuthors WHERE AuthorId = @AuthorId", new { AuthorId });
            _db.Query("DELETE FROM BooksAuthors WHERE AuthorId = @AuthorId", new { AuthorId });

            foreach (var bookAuthor in deletingAuthorBooks)
            {
                var list = _db.Query<BooksAuthors>("SELECT * FROM BooksAuthors WHERE BookId = '" + bookAuthor.BookId + "'").ToList();

                if (list.Count == 0) {
                    _db.Query("DELETE FROM Books WHERE Id = '" + bookAuthor.BookId + "'");
                }
            }

            _db.Query("DELETE FROM Authors WHERE Id = @AuthorId", new { AuthorId });
            _db.Query("DELETE FROM Articles WHERE AuthorId = @AuthorId", new { AuthorId });
        }

        public void Update(Author newRecord)
        {
            _db.Execute("UPDATE Authors SET FirstName = @FirstName, LastName = @LastName, Patronymic = @Patronymic WHERE Id = @Id", newRecord);
        }

        public Author GetAuthor(Guid AuthorId)
        {
            var author = _db.Query<Author>("SELECT * FROM Authors WHERE Id = @AuthorId", new { AuthorId }).FirstOrDefault();
            return author;
        }

        public List<Author> GetAuthors(Guid bookId)
        {
            var authorIds = _db.Query<BooksAuthors>("SELECT * FROM BooksAuthors WHERE BookId = @bookId", new { bookId }).ToArray();

            List<string> authorId = new List<string>();
            foreach(var item in authorIds)
            {
                authorId.Add(item.AuthorId.ToString());
            }
            var authors = _db.Query<Author>("SELECT * FROM Authors WHERE Id IN @authorId", new { authorId });
            return authors.ToList();
        }
    }
}