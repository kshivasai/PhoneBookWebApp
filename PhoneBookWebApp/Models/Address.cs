using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhoneBookWebApp.Models
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Address Line One")]
        public String AddressOne { get; set; }

        [Display(Name = "Address Line Two")]
        public String AddressTwo { get; set; }
        [Required]
        [Display(Name = "Pin Code")]
        public int PinCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "Person")]
        public int PeopleId { get; set; }

        public virtual People People { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }

    }
}