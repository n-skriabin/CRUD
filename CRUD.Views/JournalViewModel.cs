using CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views
{
    public class JournalViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Articles { get; set; }
        public List<Article> ArticlesList { get; set; }
        public List<string> List { get; set; }
    }
}
