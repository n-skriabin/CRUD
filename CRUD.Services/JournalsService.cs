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
        private JournalRepository _journalRepository;
        private ArticleRepository _articleRepository;
        private AuthorRepository _authorRepository;

        public JournalsService(string ConnectionString)
        {
            _journalRepository = new JournalRepository(ConnectionString);
            _articleRepository = new ArticleRepository(ConnectionString);
            _authorRepository = new AuthorRepository(ConnectionString);
        }

        public List<JournalViewModel> Read()
        {
            var journals = _journalRepository.Read();         

            List<JournalViewModel> journalsListForViewModel = new List<JournalViewModel>();
            foreach(var journal in journals)
            {
                var articles = GetArticlesList(journal.Id);
                JournalViewModel journalViewModel = new JournalViewModel
                {
                    Id = journal.Id,
                    Name = journal.Name,
                    Date = journal.Date,
                    ArticlesList = articles,
                };

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

            _journalRepository.Create(journal, articlesIdList);

            return journalViewModel;
        }

        public JournalViewModel Update(ResponseJournalViewModel responseJournalViewModel)
        {
            var journal = ViewModelToDomain(responseJournalViewModel);
            var journalViewModel = DomainToViewModel(responseJournalViewModel);

            _journalRepository.Update(journal, responseJournalViewModel.ArticlesList);

            return journalViewModel;
        }

        public void Delete(JournalViewModel journalViewModel)
        {
            _journalRepository.Delete(journalViewModel.Id);
        }

        public List<Article> GetArticlesList(Guid? JournalId)
        {
            var articles = _articleRepository.GetArticles(JournalId);
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
                ArticlesList = _articleRepository.GetArticles(responseJournalViewModel.ArticlesList),
            };

            return journalViewModel;
        }
    }
}
