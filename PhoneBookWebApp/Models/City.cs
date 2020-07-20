using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBookWebApp.Models
{
    public class City
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("City")]
        public string CityName { get; set; }

        public bool IsActive { get; set; } = true;
        public int StateId { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

    }
}