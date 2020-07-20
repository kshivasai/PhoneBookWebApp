﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBookWebApp.Models
{
    public class State
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("State")]
        public string StateName { get; set; }
        public bool IsActive { get; set; } = true;
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

    }
}