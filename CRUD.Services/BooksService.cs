using CRUD.DataAccess;
using CRUD.DataAccess.Interfaces;
using CRUD.DataAccess.Repositories;
using CRUD.Domain;
using CRUD.Views;
using CRUD.Views.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD.Services
{
    public class BooksService
    {
        private IRepositoryBook _repositoryBook;
        private IRepositoryAuthor _repositoryAuthor;

        public BooksService(string ConnectionString)
        {
            _repositoryBook = new BookRepository(ConnectionString);
            _repositoryAuthor = new AuthorRepository(ConnectionString);
        }

        public List<BookViewModel> Read()
        {
            var books = _repositoryBook.Read();
            var booksListForViewModel = new List<BookViewModel>();

            if (books.Count != 0)
            {
                for (int i = 0; i < books.Count; i++)
                {
                    var authors = _repositoryAuthor.GetAuthors(books[i].Id);
                    if (authors == null)
                    {
                        _repositoryBook.Delete(books[i].Id);
                        continue;
                    }
                    BookViewModel bookViewModel = new BookViewModel
                    {
                        Id = books[i].Id,
                        Name = books[i].Name,
                        Year = books[i].Year,
                        AuthorsList = authors,
                    };
                    booksListForViewModel.Add(bookViewModel);
                }
            }
            return booksListForViewModel;
        }

        public BookViewModel Create(ResponseBookViewModel responseBookViewModel)
        {
            responseBookViewModel.Id = Guid.NewGuid();

            var book = ViewModelToDomain(responseBookViewModel);
            var bookViewModel = DomainToViewModel(responseBookViewModel);

            _repositoryBook.Create(book, responseBookViewModel.AuthorsList);

            return bookViewModel;
        }

        public BookViewModel Update(ResponseBookViewModel responseBookViewModel)
        {
            var book = ViewModelToDomain(responseBookViewModel);
            var bookViewModel = DomainToViewModel(responseBookViewModel);

            _repositoryBook.Update(book, responseBookViewModel.AuthorsList);

            return bookViewModel;
        }

        public void Delete(BookViewModel bookViewModel)
        {
            _repositoryBook.Delete(bookViewModel.Id);
        }

        public Book ViewModelToDomain(ResponseBookViewModel responseBookViewModel)
        {
            Book book = new Book()
            {
                Id = responseBookViewModel.Id,
                Name = responseBookViewModel.Name,
                Year = responseBookViewModel.Year,
            };

            return book;
        }

        public BookViewModel DomainToViewModel(ResponseBookViewModel responseBookViewModel)
        {
            BookViewModel bookViewModel = new BookViewModel
            {
                Id = responseBookViewModel.Id,
                Name = responseBookViewModel.Name,
                Year = responseBookViewModel.Year,
                AuthorsList = _repositoryAuthor.GetAuthors(responseBookViewModel.Id),
            };

            return bookViewModel;
        }
    }
}