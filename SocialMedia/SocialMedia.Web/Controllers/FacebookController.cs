using Social.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Social.Facebook.FacebookClient;

namespace SocialMedia.Web.Controllers
{
    public class FacebookController : Controller
    {
        private const string CLIENT_ID = "1263112220424532";
        private const string CLIENT_SECRET = "8ddef446a381fb5cb625395887a1f679";
        private const string AUTH_REDIRECT_URI = "http://localhost:56276/Facebook/AuthCallback";

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AuthRequest()
        {
            var authUrl = $"https://www.facebook.com/v2.9/dialog/oauth?client_id={CLIENT_ID}&redirect_uri={AUTH_REDIRECT_URI}&scope=manage_pages,publish_pages,pages_messaging,pages_messaging_phone_number,read_page_mailboxes,pages_show_list";
            return Redirect(authUrl);
        }

        public async Task<ActionResult> AuthCallback(string code)
        {

            var fb = new FacebookClient();

            FacebookClient client = new FacebookClient();
            //var userToken = await client.GetUserToken(code, AUTH_REDIRECT_URI);
            Token userToken = null;
            var result = new UserInfoResult
            {
                UserToken = userToken,
                ApplicationToken = await client.GetApplicationToken(),
                Pages = await client.GetPages("EAAR8yzs1uVQBACi9PlHz1RaXazY8DKuwLf1OcWGr5xbv1blPNKbpSUZAAbfTq4KQxtdbZAIAQrQW36mA1wZAaeknIB3l3zDrjLX36kD2WObWWqdMf671pOEvHyS87ZChXKsaLZAwBXqTSuwCHFO4syDvZBP06WXZAyPR6M9jQ9s7wZDZD")
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