using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TwitterTest
{
    public class FacebookClient
    {
        private const string CLIENT_ID = "1263112220424532";
        private const string CLIENT_SECRET = "8ddef446a381fb5cb625395887a1f679";

        public async Task<Token> GetUserToken(string code, string redirectUri)
        {
            string tokenUrl = $"https://graph.facebook.com/v2.9/oauth/access_token?client_id={CLIENT_ID}&redirect_uri={redirectUri}&client_secret={CLIENT_SECRET}&code={code}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(tokenUrl);
            response.EnsureSuccessStatusCode();
            var jsonRes = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(jsonRes);        
        }

        public async Task<Token> GetApplicationToken()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://graph.facebook.com/v2.9/oauth/access_token?client_id={CLIENT_ID}&client_secret={CLIENT_SECRET}&grant_type=client_credentials");
            response.EnsureSuccessStatusCode();
            var jsonRes = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(jsonRes);
        }

        public async Task<IList<FBPage>> GetPages(string userToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            HttpResponseMessage response = await client.GetAsync($"https://graph.facebook.com//me/accounts");
            response.EnsureSuccessStatusCode();

            var jsonRes = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FBResult<List<FBPage>>>(jsonRes);
            return result.Data;
        }


        public class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("token_type")]
            public string Type { get; set; }
            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }
        }

        public class FBPage
        {
            public string Id { get; set; }
            public string Name { get; set; }
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            public string Category { get; set; }
        }

        public class FBResult<T>
        {
            public T Data { get; set; }
        }
    }
}
