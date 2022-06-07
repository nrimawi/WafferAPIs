using Models.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace WafferAPIs.DAL.Entites
{
    public class Seller
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; }


        [Required]
        public String Email { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public String ContactPhoneNumber { get; set; }
        public String CustomerServicePhoneNumber { get; set; }

        [Required]
        public Boolean HasStore { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        public Boolean IsVerified { get; set; }

        [Required]
        public Boolean Status { get; set; }

        public string ApplicationUserId { get; set; }


    }

}
