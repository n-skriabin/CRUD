using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Views.ResponseModels
{
    public class ResponsePublisherViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public List<Guid> BooksListId { get; set; }
        public List<Guid> JournalsListId { get; set; }
    }
}
