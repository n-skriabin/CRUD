using CRUD.DataAccess;
using CRUD.DataAccess.Interfaces;
using CRUD.DataAccess.Repositories;
using CRUD.Domain;
using CRUD.Views;
using CRUD.Views.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class PublishersService
    {
        private IRepositoryBook _repositoryBook;
        private IRepositoryAuthor _repositoryAuthor;
        private IRepositoryJournal _repositoryJournal;
        private IRepositoryArticle _repositoryArticle;
        private IRepositoryPublisher _repositoryPublisher;

        public PublishersService(string ConnectionString)
        {
            _repositoryBook = new BookRepository(ConnectionString);
            _repositoryAuthor = new AuthorRepository(ConnectionString);
            _repositoryJournal = new JournalRepository(ConnectionString);
            _repositoryArticle = new ArticleRepository(ConnectionString);
            _repositoryPublisher = new PublisherRepository(ConnectionString);
        }

        public List<PublisherViewModel> Read()
        {
            var publishers = _repositoryPublisher.Read();
            List<PublisherViewModel> publisherListForViewModel = new List<PublisherViewModel>();

            for (int i = 0; i < publishers.Count; i++)
            {
                var journals = _repositoryJournal.GetJournals(publishers[i].Id);
                var books = _repositoryBook.GetBooks(publishers[i].Id); 
                PublisherViewModel publisherViewModel = new PublisherViewModel
                {
                    Id = publishers[i].Id,
                    Name = publishers[i].Name,
                    BooksList = _repositoryBook.GetBooks(publishers[i].Id),
                    JournalsList = _repositoryJournal.GetJournals(publishers[i].Id),
                };
                publisherListForViewModel.Add(publisherViewModel);
            }
            return publisherListForViewModel;
        }

        public PublisherViewModel Create(ResponsePublisherViewModel responsePublisherViewModel)
        {
            responsePublisherViewModel.Id = Guid.NewGuid();

            var publisher = ViewModelToDomain(responsePublisherViewModel);
            _repositoryPublisher.Create(publisher, responsePublisherViewModel.JournalsListId, responsePublisherViewModel.BooksListId);

            var publisherViewModel = DomainToViewModel(responsePublisherViewModel, publisher.Id);

            return publisherViewModel;
        }

        public PublisherViewModel Update(ResponsePublisherViewModel responsePublisherViewModel)
        {
            var publisher = ViewModelToDomain(responsePublisherViewModel);
            _repositoryPublisher.Update(publisher, responsePublisherViewModel.JournalsListId, responsePublisherViewModel.BooksListId);

            var publisherViewModel = DomainToViewModel(responsePublisherViewModel, publisher.Id);

            return publisherViewModel;
        }

        public void Delete(PublisherViewModel publisherViewModel)
        {
            _repositoryJournal.Delete(publisherViewModel.Id);
        }     
        
        public Publisher ViewModelToDomain(ResponsePublisherViewModel responsePublisherViewModel)
        {
            Publisher publisher = new Publisher
            {
                Id = responsePublisherViewModel.Id,
                Name = responsePublisherViewModel.Name,
            };

            return publisher;
        }

        public PublisherViewModel DomainToViewModel(ResponsePublisherViewModel responsePublisherViewModel, Guid publisherId)
        {
            PublisherViewModel publisherViewModel = new PublisherViewModel
            {
                Id = responsePublisherViewModel.Id,
                Name = responsePublisherViewModel.Name,
                BooksList = _repositoryBook.GetBooks(publisherId),
                JournalsList = _repositoryJournal.GetJournals(publisherId),
            };

            return publisherViewModel;
        }
    }
}
