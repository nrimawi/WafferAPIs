using System;
using System.ComponentModel.DataAnnotations;

namespace WafferAPIs.Models.Dtos
{
    public class CategoryData
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }


    }
}
