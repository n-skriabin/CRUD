using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Interfaces
{
    public interface IRepositoryArticle
    {
        List<Article> Read();
        void Create(Article article);
        void Update(Article article);
        void Delete(Guid ArticleId);
        Article GetArticle(Guid ArticleId);
        List<Article> GetArticles(List<Guid> ArticlesId);
    }
}
