using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Interfaces
{
    public interface IRepositoryPublisher
    {
        List<Publisher> Read();
        void Create(Publisher book, List<Guid> journalsId, List<Guid> booksId);
        void Update(Publisher book, List<Guid> journalsId, List<Guid> booksId);
        void Delete(Guid PublisherId);
        void PublisherIdNull(Guid PublisherId);
    }
}
