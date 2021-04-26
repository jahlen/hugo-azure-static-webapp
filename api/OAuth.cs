/***********************************************************
GitHub OAuth integration backend for Netlify CMS
Using C# and Azure Functions
Copyright Johan Åhlén 2021
Licensed under The MIT License (MIT)
***********************************************************/

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace How2Code.info
{
    public static class OAuth
    {
        public const string AuthorizationUrl = "https://github.com/login/oauth/authorize";
        public const string TokenUrl = "https://github.com/login/oauth/access_token";
        public const string Scope = "repo,user";
        public const string CookieName = "oauth_state";
        public static string ClientId => Environment.GetEnvironmentVariable("OAuthClientID");
        public static string ClientSecret => Environment.GetEnvironmentVariable("OAuthClientSecret");
        public static string RedirectUri => Environment.GetEnvironmentVariable("OAuthRedirectUri");
        public static string State => Environment.GetEnvironmentVariable("OAuthState");

        [FunctionName("Auth")]
        public static IActionResult Auth(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Just for testing... adding a random state cookie to test if it comes back to the callback
            var state = CreateRandomString();
            req.HttpContext.Response.Cookies.Append(CookieName, state, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Lax, Secure = true});
            // End of testing

            var authorizationUrl = $"{AuthorizationUrl}?response_type=code&client_id={ClientId}&redirect_uri={WebUtility.UrlEncode(RedirectUri)}&scope={WebUtility.UrlEncode(Scope)}&state={WebUtility.UrlEncode(State)}";

            return new RedirectResult(authorizationUrl);
        }

        [FunctionName("Callback2")]
        public static async Task<IActionResult> Callback2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var code = req.Query["_code"].ToString(); // The parameter is renamed from "code" to "_code" by proxies.json to avoid problems with the special meaning of "code" in Azure Functions
            var state = req.Query["state"].ToString();
            if (state != State)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var tokenParams = $"grant_type=authorization_code&code={code}&redirect_uri={RedirectUri}&client_id={ClientId}&client_secret={ClientSecret}";
            var httpContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(tokenParams));
            var response = await client.PostAsync(TokenUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var tokenRegex = new Regex("access_token=([^&$]+)");
            var match = tokenRegex.Match(responseContent);
            var token = match.Groups[1].Value;

            var script = CallbackScript("github", "success", $"{{\"token\":\"{token}\",\"provider\":\"github\"}}");

            return new ContentResult { Content = script, ContentType = "text/html", StatusCode = 200 };
        }

        [FunctionName("Success")]
        public static IActionResult Success(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new ContentResult() { StatusCode = 204, ContentType = "text/plain", Content = "" };
        }

        private static string CallbackScript(string oauthProvider, string message, string content) => $@"
            <script>
            (function() {{
                function receiveMessage(e) {{
                console.log('receiveMessage %o', e);
                console.log('sendMessage %o', 'authorization:{oauthProvider}:{message}:{content}');
                // send message to main window with the app
                window.opener.postMessage(
                    'authorization:{oauthProvider}:{message}:{content}',
                    e.origin
                );
                }}
                window.addEventListener('message', receiveMessage, false);
                // Start handshare with parent
                console.log('Sending message: %o', '{oauthProvider}');
                window.opener.postMessage('authorizing:{oauthProvider}', '*');
            }})()
            </script>
        ";

        private static string CreateRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
