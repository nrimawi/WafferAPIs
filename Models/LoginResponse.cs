namespace WafferAPIs.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public SellerData Seller { get; set; }


    }
}
