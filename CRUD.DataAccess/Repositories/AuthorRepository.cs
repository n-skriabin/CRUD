using CRUD.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Repositories
{
    public class AuthorRepository
    {
        private ContextModel _db;

        public AuthorRepository(string ConnectionString)
        {
            _db = new ContextModel(ConnectionString);
        }

        public void Create(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }

        public List<Author> Read()
        {
            return _db.Authors.ToList();
        }

        public void Delete(Guid AuthorId)
        {
            var articles = _db.Articles.Where(a => a.AuthorId == AuthorId).ToList();
            var booksauthors = _db.BooksAuthors.Where(ba => ba.AuthorId == AuthorId).ToList();
            var booksAuthorsList = _db.BooksAuthors.Where(ba => ba.AuthorId == AuthorId).ToList();

            foreach (var article in articles)
            {
                _db.Articles.Remove(article);
            }

            foreach (var bookauthor in booksauthors)
            {
                var recordForDelete = _db.Books.Where(b => b.Id == bookauthor.BookId).FirstOrDefault();
                _db.BooksAuthors.Remove(bookauthor);               
            }

            var recordAuthorForDelete = _db.Authors.Where(a => a.Id == AuthorId).FirstOrDefault();
            _db.Authors.Remove(recordAuthorForDelete);
            _db.SaveChanges();
        }

        public void Update(Author newRecord)
        {
            _db.Entry(newRecord).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Author GetAuthor(Guid AuthorId)
        {
            var author = _db.Authors.Where(a => a.Id == AuthorId).FirstOrDefault();
            return author;
        }

        public List<Author> GetAuthors(Guid bookId)
        {
            var authors = new List<Author>();
            var authorbookIds = _db.BooksAuthors.Where(ba => ba.BookId == bookId).ToList();

            foreach (var authorbook in authorbookIds)
            {
                var author = _db.Authors.Where(a => a.Id == authorbook.AuthorId).FirstOrDefault();
                authors.Add(author);
            }
            return authors;
        }
    }
}
