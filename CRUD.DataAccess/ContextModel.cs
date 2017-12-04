namespace CRUD.DataAccess
{
    using CRUD.Domain;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ContextModel : DbContext
    {
        public ContextModel(string ConnectionString)
            : base(ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BooksAuthors> BooksAuthors { get; set; }
    }
}