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
    public class PublisherRepository : IRepositoryPublisher
    {
        ContextModel db;

        public PublisherRepository(string ConnectionString)
        {
            db = new ContextModel(ConnectionString);
        }

        public List<Publisher> Read()
        {
            return db.Publishers.ToList();
        }

        public void Create(Publisher publisher, List<Guid> journalsId, List<Guid> booksId)
        {
            for (int i = 0; i < journalsId.Count; i++)
            {
                Guid journalId = journalsId[i];
                var journal = db.Journals.Where(p => p.Id == journalId).First();
                journal.PublisherId = publisher.Id;
                db.Entry(journal).State = EntityState.Modified;
            }

            for (int i = 0; i < booksId.Count; i++)
            {
                Guid bookId = booksId[i];
                var book = db.Books.Where(p => p.Id == bookId).First();
                book.PublisherId = publisher.Id;
                db.Entry(book).State = EntityState.Modified;
            }

            db.Publishers.Add(publisher);
            db.SaveChanges();
        }

        public void Update(Publisher newRecord, List<Guid> journalsId, List<Guid> booksId)
        {
            Guid oldPublisherId = newRecord.Id;
            PublisherIdNull(oldPublisherId);

            for (int i = 0; i < journalsId.Count; i++)
            {
                Guid journalId = journalsId[i];
                var journal = db.Journals.Where(p => p.Id == journalId).First();
                journal.PublisherId = newRecord.Id;
                db.Entry(journal).State = EntityState.Modified;
            }

            for (int i = 0; i < booksId.Count; i++)
            {
                Guid bookId = booksId[i];
                var book = db.Books.Where(p => p.Id == bookId).First();
                book.PublisherId = newRecord.Id;
                db.Entry(book).State = EntityState.Modified;
            }

            db.Entry(newRecord).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Guid PublisherId)
        {
            PublisherIdNull(PublisherId);

            db.Publishers.Remove(db.Publishers.Where(p => p.Id == PublisherId).First());
            db.SaveChanges();
        }

        public void PublisherIdNull(Guid PublisherId)
        {
            var books = db.Books.ToList();
            var journals = db.Journals.ToList();

            foreach (var book in books)
            {
                if (book.PublisherId == PublisherId)
                {
                    book.PublisherId = null;
                    db.Entry(book).State = EntityState.Modified;
                   
                }
            }

            foreach (var journal in journals)
            {
                if (journal.PublisherId == PublisherId)
                {
                    journal.PublisherId = null;
                    db.Entry(journal).State = EntityState.Modified;
                    
                }
            }

            db.SaveChanges();
        }
    }
}
