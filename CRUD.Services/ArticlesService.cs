using CRUD.DataAccess;
using CRUD.DataAccess.Interfaces;
using CRUD.DataAccess.Repositories;
using CRUD.Domain;
using CRUD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class ArticlesService
    {
        private IRepositoryArticle _repositoryArticle;
        private IRepositoryAuthor _repositoryAuthor;

        public ArticlesService(string ConnectionString)
        {
            _repositoryArticle = new ArticleRepository(ConnectionString);
            _repositoryAuthor = new AuthorRepository(ConnectionString);
        }

        public List<ArticleViewModel> Read()
        {
            var articlesList = _repositoryArticle.Read();

            List<ArticleViewModel> articlesForView = new List<ArticleViewModel>();

            for (int i = 0; i < articlesList.Count; i++)
            {
                var author = _repositoryAuthor.GetAuthor(ToGuid(articlesList[i].AuthorId));
                var articleViewModel = DomainToViewModel(articlesList[i], author.Abbreviated);
                articlesForView.Add(articleViewModel);
            }
            return articlesForView;
        }

        public void Create(ArticleViewModel articleViewModel)
        {
            articleViewModel.Id = Guid.NewGuid();
            articleViewModel.Abbreviated = _repositoryAuthor.GetAuthor(articleViewModel.AuthorId).Abbreviated;
            var article = ViewModelToDomain(articleViewModel);
            _repositoryArticle.Create(article);
        }

        public void Update(ArticleViewModel articleViewModel)
        {
            articleViewModel.Abbreviated = _repositoryAuthor.GetAuthor(articleViewModel.AuthorId).Abbreviated;
            var article = ViewModelToDomain(articleViewModel);
            _repositoryArticle.Update(article);
        }

        public List<ArticleViewModel> GetArticlesForView()
        {
            var articlesList = _repositoryArticle.Read();

            List<ArticleViewModel> articlesForView = new List<ArticleViewModel>();
            for (int i = 0; i < articlesList.Count; i++)
            {
                var author = _repositoryAuthor.GetAuthor(ToGuid(articlesList[i].AuthorId));
                var articleViewModel = DomainToViewModel(articlesList[i], author.Abbreviated);
                articlesForView.Add(articleViewModel);
            }
            return articlesForView;
        }

        public void Delete(ArticleViewModel articleViewModel)
        {
            _repositoryArticle.Delete(articleViewModel.Id);
        }

        public Guid ToGuid(Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public Article ViewModelToDomain(ArticleViewModel articleViewModel)
        {
            Article article = new Article()
            {
                Id = articleViewModel.Id,
                Name = articleViewModel.Name,
                Year = articleViewModel.Year,
                AuthorId = articleViewModel.AuthorId,
            };

            return article;
        }

        public ArticleViewModel DomainToViewModel(Article article, string Abbreviated)
        {
            ArticleViewModel articleViewModel = new ArticleViewModel
            {
                Id = article.Id,
                AuthorId = ToGuid(article.AuthorId),
                Name = article.Name,
                Year = article.Year,
                Abbreviated = Abbreviated,
            };

            return articleViewModel;
        }
    }
}
