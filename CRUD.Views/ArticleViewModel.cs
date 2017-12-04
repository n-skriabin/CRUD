using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views
{
    public class ArticleViewModel : BaseEntityViewModel
    {       
        public string Name { get; set; }
        
        public string Year { get; set; }
        
        public Guid AuthorId { get; set; }

        public string Abbreviated { get; set; }
    }
}
