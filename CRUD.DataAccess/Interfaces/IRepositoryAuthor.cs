using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Repositories
{
    public interface IRepositoryAuthor
    {
        List<Author> Read();
        void Create(Author author);
        void Update(Author author);
        void Delete(Guid AuthorId);
        Author GetAuthor(Guid AuthorId);
        List<Author> GetAuthors(Guid bookId);
        string GetAuthorsForView(List<Guid> authors);
    }
}
