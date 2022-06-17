using System;
using WafferAPIs.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using WafferAPIs.Models.Others;

namespace WafferAPIs.Models.Dtos
{
    public class SubCategoryData
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Fetures is required")]
        public List<SubCategoryFeature> Fetures { get; set; }


        [Required(ErrorMessage = "AveragePrice is required")]
        public int AveragePrice { get; set; }
        [Required(ErrorMessage = "Category id is required")]

        public CategoryData Category { get; set; }

    }
}
