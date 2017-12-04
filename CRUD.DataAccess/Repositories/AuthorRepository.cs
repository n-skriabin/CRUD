using CRUD.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Repositories
{
    public class AuthorRepository : IRepositoryAuthor
    {
        ContextModel db;

        public AuthorRepository(string ConnectionString)
        {
            db = new ContextModel(ConnectionString);
        }

        public void Create(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public List<Author> Read()
        {
            return db.Authors.ToList();
        }

        public void Delete(Guid AuthorId)
        {
            var articles = db.Articles.ToList();
            var booksauthors = db.BooksAuthors.ToList();

            foreach (var article in articles)
            {
                if (article.Id == AuthorId)
                {
                    var recordArticleForDelete = db.Articles.Where(a => a.Id == AuthorId).FirstOrDefault();
                    db.Articles.Remove(recordArticleForDelete);                   
                }
            }

            foreach (var book in booksauthors)
            {
                if (book.Id == AuthorId)
                {
                    db.BooksAuthors.Remove(book);
                }
            }

            var recordAuthorForDelete = db.Authors.Where(a => a.Id == AuthorId).FirstOrDefault();
            db.Authors.Remove(recordAuthorForDelete);
            db.SaveChanges();
        }

        public void Update(Author newRecord)
        {
            db.Entry(newRecord).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Author GetAuthor(Guid AuthorId)
        {
            var author = db.Authors.Where(a => a.Id == AuthorId).FirstOrDefault();
            return author;
        }

        public List<Author> GetAuthors(Guid bookId)
        {

            var authors = new List<Author>();
            var authorbook = db.BooksAuthors.ToList();

            for (int i = 0; i < authorbook.Count; i++)
            {
                if (bookId == authorbook[i].BookId)
                {
                    Guid? authorId = authorbook[i].AuthorId;
                    var author = db.Authors.Where(a => a.Id == authorId).First();
                    authors.Add(author);
                }
            }
            return authors;
        }

        public string GetAuthorsForView(List<Guid> authors)
        {
            string authorsForView = "";
            Guid? Author = authors[0];

            var author = db.Authors.FirstOrDefault(p => p.Id == Author);
            authorsForView += author.Abbreviated;

            for (int i = 1; i < authors.Count; i++)
            {
                Author = authors[i];
                author = db.Authors.FirstOrDefault(p => p.Id == Author);
                authorsForView += ", " + author.Abbreviated;
            }

            authorsForView += ";";

            return authorsForView;
        }
    }
}
