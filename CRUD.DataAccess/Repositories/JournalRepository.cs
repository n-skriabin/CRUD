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
    public class JournalRepository
    {
        private IDbConnection _db;

        public JournalRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Journal> Read()
        {
            var journals = _db.Query<Journal>("SELECT * FROM Journals").ToList();
            return journals;
        }

        public void Create(Journal journal, List<Guid> articlesIds)
        {
            UpdateArticles(journal.Id, articlesIds);
            _db.Query<Journal>("INSERT INTO Journals (Id, Name, Date) VALUES (@Id, @Name, @Date)", journal);
        }

        public void Update(Journal journal, List<Guid> articlesId)
        {
            var arrayArticlesIds = articlesId.ToArray();
            var emptyGuid = Guid.Empty;
            var journalId = journal.Id;
            _db.Query<Article>("UPDATE Articles SET JournalId = '"+emptyGuid+"' WHERE JournalId = @journalId", new { journalId });
            _db.Query<Article>("UPDATE Articles SET JournalId = '" + journal.Id + "' WHERE Id IN @arrayArticlesIds", new { arrayArticlesIds });
        }

        public void Delete(Guid journalId)
        {
            var emptyGuid = Guid.Empty;
            _db.Query<Article>("UPDATE Articles SET JournalId = '" + emptyGuid + "' WHERE JournalId = @journalId", new { journalId });
            _db.Query<Journal>("DELETE FROM Journals WHERE Id = @journalId", new { journalId });
        }

        public List<Journal> GetJournals(List<Guid> journalsListId)
        {
            var arrayJournalsIds = journalsListId.ToArray();
            var journals = _db.Query<Journal>("SELECT * FROM Journals WHERE Id IN @arrayJournalsIds", new { arrayJournalsIds }).ToList();
            return journals;
        }

        public List<Journal> GetJournals(Guid publisherId)
        {
            var journalsList = _db.Query<Journal>("SELECT * FROM Journals WHERE PublisherId = @publisherId", new { publisherId }).ToList();
            return journalsList;
        }

        public void UpdateArticles(Guid id, List<Guid> articlesIds)
        {
            var arrayArticlesIds = articlesIds.ToArray();

            foreach (var articleId in arrayArticlesIds)
            {
                _db.Query<Article>("UPDATE Articles SET JournalId = '" + id + "' WHERE Id = @articleId", new { articleId });
            }
        }
    }
}
