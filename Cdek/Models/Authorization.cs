namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    internal class Authorization
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
        public string JTI { get; set; }
    }
}
