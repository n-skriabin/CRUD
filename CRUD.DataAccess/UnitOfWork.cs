using CRUD.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        Context db = new Context();

        private AuthorRepository authorRepository;

        private ArticleRepository articleRepository;

        private bool disposed = false;

        public AuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository(db);
                }

                return authorRepository;
            }
        }

        public ArticleRepository Articles
        {
            get
            {
                if (articleRepository == null)
                {
                    articleRepository = new ArticleRepository(db);
                }

                return articleRepository;
            }
        }

        public void Save(Context db)
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
