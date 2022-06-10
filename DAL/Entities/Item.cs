using System;
using System.ComponentModel.DataAnnotations;
using WafferAPIs.DAL.Entites;
using WafferAPIs.Models.Others;

namespace WafferAPIs.DAL.Entities
{
    public class Item
    {

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Dimensions Dimensions { get; set; }

        public double Weight { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]

        public string ModelNumber { get; set; }

        public int Waranty { get; set; }
        public Double SaleRatio { get; set; }

        public string OtherFeatures { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
