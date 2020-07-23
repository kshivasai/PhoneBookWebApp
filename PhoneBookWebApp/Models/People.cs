using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PhoneBookWebApp.Models
{
    public class People
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.", MinimumLength = 1)]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.", MinimumLength = 1)]
        public String LastName { get; set; }

        [Display(Name = "Phone")]
        [Required]
        [StringLength(13, ErrorMessage = "Phone Number cannot be longer than 13 characters.", MinimumLength = 10)]
        public String PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email field is Requried")]
        [EmailAddress]
        public String Email { get; set; }


        //Address Part
        [Display(Name = "Address Line One")]
        [Required]
        public String AddressOne { get; set; }

        [Display(Name = "Address Line Two")]
        public String AddressTwo { get; set; }

        [Display(Name = "Pin Code")]
        [Required]
        public int PinCode { get; set; }
        public Boolean IsActive { get; set; } = true;

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [Required]
        [ForeignKey("State")]
        
        public int StateId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
    }
}