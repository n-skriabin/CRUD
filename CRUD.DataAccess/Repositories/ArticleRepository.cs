using CRUD.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Domain;
using System.Data.Entity;

namespace CRUD.DataAccess.Repositories
{
    public class ArticleRepository : IRepositoryArticle
    {
        ContextModel db;

        public ArticleRepository(string ConnectionString)
        {
            db = new ContextModel(ConnectionString);
        }

        public void Create(Article article)
        {
            var author = db.Authors.Where(a => a.Id == article.AuthorId).First();
            db.Articles.Add(article);
            db.SaveChanges();
        }

        public void Delete(Guid ArticleId)
        {
            var recordForDelete = db.Articles.Where(a => a.Id == ArticleId).First();
            db.Articles.Remove(recordForDelete);
            db.SaveChanges();
        }

        public List<Article> Read()
        {
            return db.Articles.ToList();
        }

        public void Update(Article oldRecord)
        {
            db.Entry(oldRecord).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Article GetArticle(Guid ArticleId)
        {
            var article = db.Articles.Where(a => a.Id == ArticleId).FirstOrDefault();
            return article;
        }

        public List<Article> GetArticles(List<Guid> ArticlesId)
        {
            var articles = new List<Article>();
            foreach (var id in ArticlesId) {
                articles.Add(db.Articles.Where(a => a.Id == id).FirstOrDefault());
            }
            return articles;
        }
    }
}
