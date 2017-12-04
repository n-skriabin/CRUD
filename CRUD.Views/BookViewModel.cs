using CRUD.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views
{
    public class BookViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public string Author { get; set; }
        public List<Author> AuthorsList { get; set; }
    }
}
