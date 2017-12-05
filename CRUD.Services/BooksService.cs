using CRUD.DataAccess;
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
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;

        public BooksService(string ConnectionString)
        {
            _bookRepository = new BookRepository(ConnectionString);
            _authorRepository = new AuthorRepository(ConnectionString);
        }

        public List<BookViewModel> Read()
        {
            var books = _bookRepository.Read();
            var booksListForViewModel = new List<BookViewModel>();

            if (books == null || books.Count == 0)
            {
                return booksListForViewModel;
            }

            foreach (var book in books)
            {
                var authors = _authorRepository.GetAuthors(book.Id);
                if (authors.Count == 0)
                {
                    _bookRepository.Delete(book.Id);
                    continue;
                }
                BookViewModel bookViewModel = new BookViewModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Year = book.Year,
                    AuthorsList = authors,
                };
                booksListForViewModel.Add(bookViewModel);
            }
            return booksListForViewModel;
        }

        public BookViewModel Create(ResponseBookViewModel responseBookViewModel)
        {
            responseBookViewModel.Id = Guid.NewGuid();

            var book = ViewModelToDomain(responseBookViewModel);
            var bookViewModel = DomainToViewModel(responseBookViewModel);

            _bookRepository.Create(book, responseBookViewModel.AuthorsList);

            return bookViewModel;
        }

        public BookViewModel Update(ResponseBookViewModel responseBookViewModel)
        {
            var book = ViewModelToDomain(responseBookViewModel);
            var bookViewModel = DomainToViewModel(responseBookViewModel);

            _bookRepository.Update(book, responseBookViewModel.AuthorsList);

            return bookViewModel;
        }

        public void Delete(BookViewModel bookViewModel)
        {
            _bookRepository.Delete(bookViewModel.Id);
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
                AuthorsList = _authorRepository.GetAuthors(responseBookViewModel.Id),
            };

            return bookViewModel;
        }
    }
}