using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using static FacebookSdk.FacebookClient;
using FacebookSdk;

namespace TwitterTest.Controllers
{
    public class FacebookController : Controller
    {
        private const string CLIENT_ID= "1263112220424532";
        private const string CLIENT_SECRET = "8ddef446a381fb5cb625395887a1f679";
        private const string AUTH_REDIRECT_URI = "http://localhost:56349/Facebook/UserTokenRequestCallback";

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UserTokenRequest()
        {
            var authUrl = $"https://www.facebook.com/v2.9/dialog/oauth?client_id={CLIENT_ID}&redirect_uri={AUTH_REDIRECT_URI}&scope=manage_pages,publish_pages,pages_messaging,pages_messaging_phone_number,read_page_mailboxes,pages_show_list";
            return Redirect(authUrl);
        }

        public async Task<IActionResult> UserTokenRequestCallback(string code)
        {
            FacebookClient client = new FacebookClient();
            var userToken = await client.GetUserToken(code, AUTH_REDIRECT_URI);
            var result = new UserInfoResult
            {
                UserToken = userToken,
                ApplicationToken = await client.GetApplicationToken(),
                Pages = await client.GetPages(userToken.AccessToken)
            };
            return View(result);
        }

        public class UserInfoResult
        {
            public Token UserToken { get; set; }
            public Token ApplicationToken { get; set; }
            public IList<FBPage> Pages { get; set; }
        }

    }
}