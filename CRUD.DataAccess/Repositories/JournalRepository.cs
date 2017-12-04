using CRUD.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CRUD.Domain;

namespace CRUD.DataAccess.Repositories
{
    public class JournalRepository : IRepositoryJournal
    {
        ContextModel db;

        public JournalRepository(string ConnectionString)
        {
            db = new ContextModel(ConnectionString);
        }

        public void Create(Journal journal, List<Guid> articlesId)
        {
            var articlesList = db.Articles.ToList();
            foreach (var articleId in articlesId)
            {
                Guid? articleIdString = articleId;
                var article = db.Articles.Where(a => a.Id == articleIdString).First();
                article.JournalId = journal.Id;
                db.Entry(article).State = EntityState.Modified;
            }

            db.Journals.Add(journal);
            db.SaveChanges();
        }

        public void Update(Journal journal, List<Guid> articlesId)
        {
            var articlesList = db.Articles.ToList();
            foreach (var articleId in articlesId)
            {
                Guid? articleIdString = articleId;
                var article = db.Articles.Where(a => a.Id == articleIdString).First();
                article.JournalId = journal.Id;
                db.Entry(article).State = EntityState.Modified;
            }

            db.Entry(journal).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Guid JournalId)
        {
            var recordForDelete = db.Journals.Where(b => b.Id == JournalId).First();
            var articles = db.Articles.ToList();

            foreach (var article in articles)
            {
                if (article.Id == JournalId)
                {
                    article.JournalId = Guid.Empty;
                    db.Entry(article).State = EntityState.Modified;                   
                }
            }

            db.Journals.Remove(recordForDelete);
            db.SaveChanges();
        }

        public List<Journal> Read()
        {
            return db.Journals.ToList();
        }

        public List<Journal> GetJournals(List<Guid> journalsListId)
        {
            var journals = new List<Journal>();

            foreach (var journalId in journalsListId)
            {
                journals.Add(db.Journals.Where(j => j.Id == journalId).First());
            }

            return journals;
        }

        public List<Journal> GetJournals(Guid PublisherId)
        {
            var journals = Read();
            var journalsList = new List<Journal>();
            foreach (var journal in journals)
            {
                if (journal.PublisherId == PublisherId)
                {
                    journalsList.Add(journal);
                }
            }

            return journalsList;
        }
    }
}
