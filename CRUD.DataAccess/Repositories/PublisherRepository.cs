using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CRUD.Domain;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CRUD.DataAccess.Repositories
{
    public class PublisherRepository
    {
        private IDbConnection _db;

        public PublisherRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Publisher> Read()
        {
            var publishers = _db.Query<Publisher>("SELECT * FROM Publishers").ToList();
            return publishers;
        }

        public void Create(Publisher publisher, List<Guid> journalsId, List<Guid> booksId)
        {
            var arrayBooksIds = booksId.ToArray();
            _db.Execute("UPDATE Books SET PublisherId = '" + publisher.Id + "' WHERE Id IN @arrayBooksIds", new { arrayBooksIds });

            var arrayJournalsIds = journalsId.ToArray();
            _db.Execute("UPDATE Journals SET PublisherId = '" + publisher.Id + "' WHERE Id IN @arrayJournalsIds", new { arrayJournalsIds });

            _db.Query<Publisher>("INSERT INTO Publishers (Id, Name) VALUES (@Id, @Name)", publisher);
        }

        public void Update(Publisher newRecord, List<Guid> journalsId, List<Guid> booksId)
        {
            PublisherIdNull(newRecord.Id);

            var arrayBooksIds = booksId.ToArray();
            _db.Execute("UPDATE Books SET PublisherId = '" + newRecord.Id + "' WHERE Id IN @arrayBooksIds", new { arrayBooksIds });

            var arrayJournalsIds = journalsId.ToArray();
            _db.Execute("UPDATE Journals SET PublisherId = '" + newRecord.Id + "' WHERE Id IN @arrayJournalsIds", new { arrayJournalsIds });

            _db.Execute("UPDATE Publishers SET Name = @Name WHERE Id = '" + newRecord.Id + "'", newRecord);
        }

        public void Delete(Guid publisherId)
        {
            PublisherIdNull(publisherId);

            _db.Query<Publisher>("DELETE FROM Publishers WHERE Id = @publisherId", new { publisherId });
        }

        public void PublisherIdNull(Guid publisherId)
        {
            var emptyGuid = Guid.Empty;
            _db.Query<Book>("UPDATE Books SET PublisherId = '" + emptyGuid + "' WHERE PublisherId = @publisherId", new { publisherId });
            _db.Query<Journal>("UPDATE Journals SET PublisherId = '" + emptyGuid + "' WHERE PublisherId = @publisherId", new { publisherId });
        }

        public void AddBooksJournals(string publisherId, List<Guid> journalsId, List<Guid> booksId)
        {
            var arrayBooksIds = booksId.ToArray();
            _db.Execute("UPDATE Books SET PublisherId = " + publisherId + " WHERE Id IN @arrayBooksIds", new { arrayBooksIds });

            var arrayJournalsIds = journalsId.ToArray();
            _db.Execute("UPDATE Journals SET PublisherId = " + publisherId + " WHERE Id IN @arrayJournalsIds", new { arrayJournalsIds });
        }
    }
}
