using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookWebApp.Models
{
    public class Country
    {
        public int ID { get; set; }
        [Required]
        [DisplayName ( "Country")]
        public string CountryName { get; set; }

        public bool IsActive { get; set; } = true;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<State> States { get; set; }
      
    }

}