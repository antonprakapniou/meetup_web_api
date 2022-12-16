namespace MeetupWebApi.WEB.IdentityServer
{
    //use for connect to IdentityServer4
    //specify paths according to appsettings.json
    //use RequireHttpsMetadata=false for development
    public static class IdentityServerConnection
    {
        public const string Authority = "IdentityServer:Authority";
        public const string ApiName = "IdentityServer:ApiName";
        public const string RequireHttpsMetadata = "IdentityServer:RequireHttpsMetadata";
    }
}