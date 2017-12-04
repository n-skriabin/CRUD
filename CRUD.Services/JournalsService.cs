using CRUD.DataAccess.Interfaces;
using CRUD.DataAccess.Repositories;
using CRUD.Domain;
using CRUD.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class JournalsService
    {
        private IRepositoryJournal _repositoryJournal;
        private IRepositoryArticle _repositoryArticle;
        private IRepositoryAuthor _repositoryAuthor;

        public JournalsService(string ConnectionString)
        {
            _repositoryJournal = new JournalRepository(ConnectionString);
            _repositoryArticle = new ArticleRepository(ConnectionString);
            _repositoryAuthor = new AuthorRepository(ConnectionString);
        }

        public List<JournalViewModel> Read()
        {
            var journals = _repositoryJournal.Read();         

            List<JournalViewModel> journalsListForViewModel = new List<JournalViewModel>();
            for (int i = 0; i < journals.Count; i++)
            {
                var articles = GetArticlesList(journals[i].Id);
                JournalViewModel journalViewModel = new JournalViewModel
                {
                    Id = journals[i].Id,
                    Name = journals[i].Name,
                    Date = journals[i].Date,
                };
                if (articles != null)
                {
                    journalViewModel.ArticlesList = articles;
                }

                journalsListForViewModel.Add(journalViewModel);
            }

            return journalsListForViewModel;
        }

        public JournalViewModel Create(ResponseJournalViewModel responseJournalViewModel)
        {
            responseJournalViewModel.Id = Guid.NewGuid();
            var articlesIdList = responseJournalViewModel.ArticlesList;

            var journal = ViewModelToDomain(responseJournalViewModel);
            var journalViewModel = DomainToViewModel(responseJournalViewModel);

            _repositoryJournal.Create(journal, articlesIdList);

            return journalViewModel;
        }

        public JournalViewModel Update(ResponseJournalViewModel responseJournalViewModel)
        {
            var journal = ViewModelToDomain(responseJournalViewModel);
            var journalViewModel = DomainToViewModel(responseJournalViewModel);

            _repositoryJournal.Update(journal, responseJournalViewModel.ArticlesList);

            return journalViewModel;
        }

        public void Delete(JournalViewModel journalViewModel)
        {
            _repositoryJournal.Delete(journalViewModel.Id);
        }

        public List<Article> GetArticlesList(Guid? JournalId)
        {
            var allArticles = _repositoryArticle.Read();
            var articles = new List<Article>();

            foreach(var article in allArticles)
            {
                if (article.JournalId == JournalId)
                {
                    articles.Add(article);
                }
            }
            return articles;
        }

        public Journal ViewModelToDomain(ResponseJournalViewModel responseJournalViewModel)
        {
            Journal journal = new Journal()
            {
                Id = responseJournalViewModel.Id,
                Name = responseJournalViewModel.Name,
                Date = responseJournalViewModel.Date,
            };

            return journal;
        }

        public JournalViewModel DomainToViewModel(ResponseJournalViewModel responseJournalViewModel)
        {
            var journalViewModel = new JournalViewModel
            {
                Id = responseJournalViewModel.Id,
                Name = responseJournalViewModel.Name,
                Date = responseJournalViewModel.Date,
                ArticlesList = _repositoryArticle.GetArticles(responseJournalViewModel.ArticlesList),
            };

            return journalViewModel;
        }
    }
}
