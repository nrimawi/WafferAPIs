using Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WafferAPIs.DAL.Entities;

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



        public string ApplicationUserId { get; set; }

        public string Logo { get; set; }
        public string WebsiteLink { get; set; }
        public string SocailMedaLink { get; set; }

        public List<Item> Items { get; set; }


        [Required]
        public Boolean Status { get; set; }
    }

}
