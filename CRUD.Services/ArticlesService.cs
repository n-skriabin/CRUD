using CRUD.DataAccess;
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
        private ArticleRepository _articleRepository;
        private AuthorRepository _authorRepository;

        public ArticlesService(string ConnectionString)
        {
            _articleRepository = new ArticleRepository(ConnectionString);
            _authorRepository = new AuthorRepository(ConnectionString);
        }

        public List<ArticleViewModel> Read()
        {
            var articlesList = _articleRepository.Read();

            List<ArticleViewModel> articlesForView = new List<ArticleViewModel>();

            foreach(var article in articlesList)
            {
                var guidId = ToGuid(article.AuthorId);
                var author = _authorRepository.GetAuthor(guidId);
                
                var articleViewModel = DomainToViewModel(article, author.Abbreviated);
                articlesForView.Add(articleViewModel);
            }
            return articlesForView;
        }

        public void Create(ArticleViewModel articleViewModel)
        {
            articleViewModel.Id = Guid.NewGuid();
            articleViewModel.Abbreviated = _authorRepository.GetAuthor(articleViewModel.AuthorId).Abbreviated;
            var article = ViewModelToDomain(articleViewModel);
            _articleRepository.Create(article);
        }

        public void Update(ArticleViewModel articleViewModel)
        {
            articleViewModel.Abbreviated = _authorRepository.GetAuthor(articleViewModel.AuthorId).Abbreviated;
            var article = ViewModelToDomain(articleViewModel);
            _articleRepository.Update(article);
        }

        public List<ArticleViewModel> GetArticlesForView()
        {
            var articlesList = _articleRepository.Read();

            List<ArticleViewModel> articlesForView = new List<ArticleViewModel>();
            foreach (var article in articlesList)
            {
                var guidId = ToGuid(article.AuthorId);
                var author = _authorRepository.GetAuthor(guidId);
                var articleViewModel = DomainToViewModel(article, author.Abbreviated);
                articlesForView.Add(articleViewModel);
            }
            return articlesForView;
        }

        public void Delete(ArticleViewModel articleViewModel)
        {
            _articleRepository.Delete(articleViewModel.Id);
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
