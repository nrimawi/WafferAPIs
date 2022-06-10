using System.Collections.Generic;

namespace WafferAPIs.Models
{
    public class LoginResponse
    {

        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string UserAuthId { get; set; }
        public SellerData Seller { get; set; }
        public string AdminName { get; set; }



    }
}
