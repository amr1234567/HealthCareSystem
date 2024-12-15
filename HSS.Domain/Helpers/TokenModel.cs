namespace HSS.Domain.Helpers
{
    public class TokenModel
    {
        public string Token { get;  set; }
        public string RefreshToken { get;  set; }
        public DateTime ExpireDate { get;  set; }
    }
}
