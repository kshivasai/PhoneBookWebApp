namespace Phone.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(13)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public virtual Address Address { get; set; }
    }
}
