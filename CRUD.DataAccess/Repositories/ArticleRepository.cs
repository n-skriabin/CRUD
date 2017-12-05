using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Domain;
using System.Data.Entity;

namespace CRUD.DataAccess.Repositories
{
    public class ArticleRepository 
    {
        private ContextModel _db;

        public ArticleRepository(string ConnectionString)
        {
            _db = new ContextModel(ConnectionString);
        }

        public void Create(Article article)
        {
            var author = _db.Authors.Where(a => a.Id == article.AuthorId).FirstOrDefault();
            _db.Articles.Add(article);
            _db.SaveChanges();
        }

        public void Delete(Guid ArticleId)
        {
            var recordForDelete = _db.Articles.Where(a => a.Id == ArticleId).FirstOrDefault();
            _db.Articles.Remove(recordForDelete);
            _db.SaveChanges();
        }

        public List<Article> Read()
        {
            return _db.Articles.ToList();
        }

        public void Update(Article oldRecord)
        {
            _db.Entry(oldRecord).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Article GetArticle(Guid ArticleId)
        {
            var article = _db.Articles.Where(a => a.Id == ArticleId).FirstOrDefault();
            return article;
        }

        public List<Article> GetArticles(List<Guid> articlesIds)
        {
            var articles = new List<Article>();
            foreach (var id in articlesIds) {
                articles.Add(_db.Articles.Where(a => a.Id == id).FirstOrDefault());
            }
            return articles;
        }
    }
}
