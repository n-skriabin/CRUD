using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain
{
    public class Journal : BaseEntity
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public Guid? PublisherId { get; set; }
    }
}
