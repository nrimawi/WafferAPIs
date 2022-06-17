using System;

namespace WafferAPIs.DAL.Entities
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fetures { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int AveragePrice { get; set; }
        public bool Status { get; set; }


    }
}
