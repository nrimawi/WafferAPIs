using System;
using System.Collections.Generic;

namespace WafferAPIs.Models.Others
{
    public class CustomizePackageRequest
    {
        public int Budget { get; set; }
        public int HouseSpace { get; set; }
        public int FamilyMembers { get; set; }
        public string Brand { get; set; }
        public List<Guid> RequiredItems { get; set; }
    }
}
