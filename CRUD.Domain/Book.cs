using CRUD.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Domain
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Year { get; set; }

        public Guid? PublisherId { get; set; }
    }
}
