using System;
using System.Collections.Generic;

namespace WafferAPIs.DAL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}
