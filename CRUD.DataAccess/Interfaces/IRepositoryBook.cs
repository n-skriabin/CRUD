using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Interfaces
{
    public interface IRepositoryBook
    {
        List<Book> Read();
        void Create(Book book, List<Guid> authors);
        void Update(Book book, List<Guid> authors);
        void Delete(Guid BookId);
        
        List<Book> GetBooks(List<Guid> booksList);
        List<Book> GetBooks(Guid PublisherId);
    }
}
