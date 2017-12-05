using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CRUD.Domain;

namespace CRUD.DataAccess.Repositories
{
    public class PublisherRepository
    {
        private ContextModel _db;

        public PublisherRepository(string ConnectionString)
        {
            _db = new ContextModel(ConnectionString);
        }

        public List<Publisher> Read()
        {
            return _db.Publishers.ToList();
        }

        public void Create(Publisher publisher, List<Guid> journalsId, List<Guid> booksId)
        {
            foreach(var journalId in journalsId )
            {
                var journal = _db.Journals.Where(p => p.Id == journalId).FirstOrDefault();
                journal.PublisherId = publisher.Id;
                _db.Entry(journal).State = EntityState.Modified;
            }

            foreach(var bookId in booksId)
            {
                var book = _db.Books.Where(p => p.Id == bookId).FirstOrDefault();
                book.PublisherId = publisher.Id;
                _db.Entry(book).State = EntityState.Modified;
            }

            _db.Publishers.Add(publisher);
            _db.SaveChanges();
        }

        public void Update(Publisher newRecord, List<Guid> journalsId, List<Guid> booksId)
        {
            Guid oldPublisherId = newRecord.Id;
            PublisherIdNull(oldPublisherId);

            foreach (var journalId in journalsId)
            {
                var journal = _db.Journals.Where(p => p.Id == journalId).FirstOrDefault();
                journal.PublisherId = newRecord.Id;
                _db.Entry(journal).State = EntityState.Modified;
            }

            foreach (var bookId in booksId)
            {
                var book = _db.Books.Where(p => p.Id == bookId).FirstOrDefault();
                book.PublisherId = newRecord.Id;
                _db.Entry(book).State = EntityState.Modified;
            }

            _db.Entry(newRecord).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Guid PublisherId)
        {
            PublisherIdNull(PublisherId);

            _db.Publishers.Remove(_db.Publishers.Where(p => p.Id == PublisherId).FirstOrDefault());
            _db.SaveChanges();
        }

        public void PublisherIdNull(Guid PublisherId)
        {
            var books = _db.Books.ToList();
            var journals = _db.Journals.ToList();

            foreach (var book in books)
            {
                if (book.PublisherId == PublisherId)
                {
                    book.PublisherId = null;
                    _db.Entry(book).State = EntityState.Modified;
                   
                }
            }

            foreach (var journal in journals)
            {
                if (journal.PublisherId == PublisherId)
                {
                    journal.PublisherId = null;
                    _db.Entry(journal).State = EntityState.Modified;
                    
                }
            }

            _db.SaveChanges();
        }
    }
}
