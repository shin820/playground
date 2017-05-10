using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveChatApi.Controllers
{
    public class FacebookWebHookController : ApiController
    {
        // GET api/values/5
        public int Get()
        {
            var queryStrings = Request.GetQueryNameValuePairs();
            var challenge = queryStrings.FirstOrDefault(t => t.Key == "hub.challenge").Value;
            return int.Parse(challenge);
        }

        // POST api/values
        public IHttpActionResult Post()
        {
            var request = Request;
            var content = request.Content.ReadAsStringAsync();

            return Ok();
        }
    }
}
