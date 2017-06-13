using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Models;

namespace SocialMedia.Web.Controllers
{
    public class TwitterController : Controller
    {
        // just for test, should store auth token info in database.
        private static IAuthenticationToken token;

        private IAuthenticationContext _authenticationContext;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthRequest()
        {
            var appCreds = new ConsumerCredentials("Mj6zNyYU0GGHcdAqAHv5q0oHi", "FBPUNsy5HYUdz4cRTFIST0FA0EBxi0bMPwCvae9KtIOxHenbn4");

            // Specify the url you want the user to be redirected to
            _authenticationContext = AuthFlow.InitAuthentication(appCreds, "http://localhost:56349/Twitter/ValidateAuth");

            token = _authenticationContext.Token;
            return new RedirectResult(_authenticationContext.AuthorizationURL);
        }

        public ActionResult ValidateAuth(string oauth_verifier)
        {
            // Create the user credentials
            var userCreds = AuthFlow.CreateCredentialsFromVerifierCode(oauth_verifier, token);

            // Do whatever you want with the user now!
            var user = Tweetinvi.User.GetAuthenticatedUser(userCreds);
            ViewBag.AccessToken = user.Credentials.AccessToken;
            ViewBag.AccessTokenSecret = user.Credentials.AccessTokenSecret;
            return View();
        }
    }
}