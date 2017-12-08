using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Domain;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CRUD.DataAccess.Repositories
{
    public class ArticleRepository 
    {
        private IDbConnection _db;

        public ArticleRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Article> Read()
        {
            var articles = _db.Query<Article>("SELECT * FROM Articles").ToList();
            return articles;
        }

        public void Create(Article article)
        {
            _db.Query<Article>("INSERT INTO Articles (Id, Name, Year, AuthorId) VALUES(@Id, @Name, @Year, @AuthorId);", article);
        }

        public void Update(Article newRecord)
        {
            _db.Execute("UPDATE Articles SET Name = @Name, Year = @Year, AuthorId = @AuthorId WHERE Id = @Id", newRecord);
        }

        public void Delete(Guid id)
        {
            _db.Query<Article>("DELETE FROM Articles WHERE Id = @id", new { id });
        }

        public Article GetArticle(Guid id)
        {
            var article = _db.Query<Article>("SELECT * FROM Articles WHERE Id = @id", id).FirstOrDefault();
            return article;
        }

        public List<Article> GetArticles(List<Guid> articlesIds)
        {
            var arrayIds = articlesIds.ToArray();
            var articles = _db.Query<Article>("SELECT * FROM Articles WHERE Id IN @arrayIds", new { arrayIds }).ToList();
            return articles;
        }

        public List<Article> GetArticles(Guid? journalId)
        {
            var articles = _db.Query<Article>("SElECT * FROM Articles WHERE JournalId = @journalId", new { journalId }).ToList();
            return articles;
        }
    }
}
