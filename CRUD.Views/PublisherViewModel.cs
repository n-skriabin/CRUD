using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views
{
    public class PublisherViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Journals { get; set; }
        public string Books { get; set; }

        public List<Book> BooksList { get; set; }
        public List<Journal> JournalsList { get; set; }
    }
}
