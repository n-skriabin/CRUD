using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views
{
    public class ResponseJournalViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public List<Guid> ArticlesList { get; set; }
    }
}
