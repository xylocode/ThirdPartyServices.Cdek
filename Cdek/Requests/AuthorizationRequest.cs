namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    internal class AuthorizationRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; } = "client_credentials";

        public AuthorizationRequest() { }
        public AuthorizationRequest(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
