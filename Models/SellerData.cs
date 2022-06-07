using System;
using System.ComponentModel.DataAnnotations;

namespace WafferAPIs.Models
{
    public class SellerData
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public String Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public String Description { get; set; }
        [Required(ErrorMessage = "ContactPhoneNumber is required")]
        public String ContactPhoneNumber { get; set; }

        public String CustomerServicePhoneNumber { get; set; }

        [Required(ErrorMessage = "Store info is required")]
        public Boolean HasStore { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }
    }
}


