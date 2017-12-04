using CRUD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CRUD.DataAccess.Repositories;
using CRUD.Views;

namespace CRUD.Services
{
    public class AuthorsService
    {
        private IRepositoryAuthor _reposytoryAuthor;

        public AuthorsService(string ConnectionString)
        {
            _reposytoryAuthor = new AuthorRepository(ConnectionString);
        }

        public List<Author> Read()
        { 
            return _reposytoryAuthor.Read();
        }

        public void Create(AuthorViewModel authorViewModel)
        {
            authorViewModel.Id = Guid.NewGuid();
            Author author = new Author
            {
                Id = authorViewModel.Id,
                FirstName = authorViewModel.FirstName,
                LastName = authorViewModel.LastName,
                Patronymic = authorViewModel.Patronymic,
                Abbreviated = GenerateAbbreviated(authorViewModel)
            };
            _reposytoryAuthor.Create(author);
        }

        public void Update(AuthorViewModel authorViewModel)
        {
            Author newRecord = new Author
            {
                Id = authorViewModel.Id,
                FirstName = authorViewModel.FirstName,
                LastName = authorViewModel.LastName,
                Patronymic = authorViewModel.Patronymic,
                Abbreviated = GenerateAbbreviated(authorViewModel)
            };
            _reposytoryAuthor.Update(newRecord);
        }

        public void Delete(AuthorViewModel authorViewModel)
        {
            _reposytoryAuthor.Delete(authorViewModel.Id);
        }

        public string GenerateAbbreviated(AuthorViewModel authorViewModel)
        {
            string Abbreviated = "";

            if (authorViewModel.Patronymic != null)
            {
                Abbreviated = authorViewModel.FirstName[0] + "." + authorViewModel.Patronymic[0] + ". " + authorViewModel.LastName;
            }

            if (authorViewModel.Patronymic == null)
            {
                Abbreviated = authorViewModel.FirstName[0] + ". " + authorViewModel.LastName;
            }
            return Abbreviated;
        }
    }
}
