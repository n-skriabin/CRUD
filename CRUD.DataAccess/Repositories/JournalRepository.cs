using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CRUD.Domain;

namespace CRUD.DataAccess.Repositories
{
    public class JournalRepository
    {
        private ContextModel _db;

        public JournalRepository(string ConnectionString)
        {
            _db = new ContextModel(ConnectionString);
        }

        public void Create(Journal journal, List<Guid> articlesId)
        {
            foreach (var articleId in articlesId)
            {
                Guid? articleIdString = articleId;
                var article = _db.Articles.Where(a => a.Id == articleIdString).FirstOrDefault();
                article.JournalId = journal.Id;
                _db.Entry(article).State = EntityState.Modified;
            }

            _db.Journals.Add(journal);
            _db.SaveChanges();
        }

        public void Update(Journal journal, List<Guid> articlesId)
        {
            foreach (var articleId in articlesId)
            {
                Guid? articleIdString = articleId;
                var article = _db.Articles.Where(a => a.Id == articleIdString).FirstOrDefault();
                article.JournalId = journal.Id;
                _db.Entry(article).State = EntityState.Modified;
            }

            _db.Entry(journal).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Guid JournalId)
        {
            var recordForDelete = _db.Journals.Where(b => b.Id == JournalId).FirstOrDefault();
            var articles = _db.Articles.Where(a => a.JournalId == JournalId).ToList();

            foreach (var article in articles)
            {
                article.JournalId = Guid.Empty;
                _db.Entry(article).State = EntityState.Modified;                   
            }

            _db.Journals.Remove(recordForDelete);
            _db.SaveChanges();
        }

        public List<Journal> Read()
        {
            return _db.Journals.ToList();
        }

        public List<Journal> GetJournals(List<Guid> journalsListId)
        {
            var journals = new List<Journal>();

            foreach (var journalId in journalsListId)
            {
                journals.Add(_db.Journals.Where(j => j.Id == journalId).FirstOrDefault());
            }

            return journals;
        }

        public List<Journal> GetJournals(Guid PublisherId)
        {
            var journalsList = _db.Journals.Where(j => j.PublisherId == PublisherId).ToList();
            return journalsList;
        }
    }
}
