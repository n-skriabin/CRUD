using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess.Interfaces
{
    public interface IRepositoryJournal
    {
        List<Journal> Read();
        void Create(Journal book, List<Guid> articles);
        void Delete(Guid JournalId);
        void Update(Journal book, List<Guid> articles);
        List<Journal> GetJournals(List<Guid> journalslistId);
        List<Journal> GetJournals(Guid PublisherId);
    }
}
