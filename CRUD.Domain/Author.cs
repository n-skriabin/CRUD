namespace CRUD.DataAccess
{
    using CRUD.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Author : BaseEntity
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Patronymic { get; set; }
        [StringLength(50)]
        public string Abbreviated { get; set; }
    }
}
