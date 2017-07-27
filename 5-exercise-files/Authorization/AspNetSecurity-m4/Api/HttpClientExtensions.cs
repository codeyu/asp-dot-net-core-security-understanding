using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace AspNetSecurity_m4.Api
{
    public static class HttpClientExtensions
    {
        public static async void SetBearerToken(this HttpClient client, HttpContext context)
        {
            var accessToken =
                await context.Authentication.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
