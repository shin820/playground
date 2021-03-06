﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FacebookSdk
{
    public class FacebookClient
    {
        private const string CLIENT_ID = "1263112220424532";
        private const string CLIENT_SECRET = "8ddef446a381fb5cb625395887a1f679";
        private const string BASE_GRAPH_API_URL = "https://graph.facebook.com/v2.9";

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

        public async Task<string> PublishPagePost(string pageId, string pageToken, string message, string link = null)
        {
            HttpClient client = new HttpClient();
            var param = JsonConvert.SerializeObject(new { Message = message });
            var parameters = new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("message",message)
            };
            if (!string.IsNullOrWhiteSpace(link))
            {
                parameters.Add(new KeyValuePair<string, string>("link", link));
            }
            FormUrlEncodedContent contentPost = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = await client.PostAsync($"{BASE_GRAPH_API_URL}/{pageId}/feed?access_token={pageToken}", contentPost);
            response.EnsureSuccessStatusCode();

            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            return result.GetValue("id").ToString();
        }

        public async Task Delete(string id, string token)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"{BASE_GRAPH_API_URL}/{id}?access_token={token}");
            var res = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> PublishComment(string id, string token, string message)
        {
            HttpClient client = new HttpClient();
            var param = JsonConvert.SerializeObject(new { Message = message });
            FormUrlEncodedContent contentPost = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("message",message)
            });
            HttpResponseMessage response = await client.PostAsync($"{BASE_GRAPH_API_URL}/{id}/comments?access_token={token}", contentPost);
            response.EnsureSuccessStatusCode();

            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            return result.GetValue("id").ToString();
        }

        public async Task<string> SendMessage(string token, string message, string recipient)
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(new
            {
                recipient = new { id = recipient },
                message = new { text = message }
            });
            StringContent theContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BASE_GRAPH_API_URL}/me/messages?access_token={token}", theContent);
            string result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return result;
        }

        public async Task SubscribeApp(string pageId, string pageToken)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync($"{BASE_GRAPH_API_URL}/{pageId}/subscribed_apps?access_token={pageToken}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserProfile> GetUserProfile(string userId, string token)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{BASE_GRAPH_API_URL}/{userId}?access_token={token}");
            response.EnsureSuccessStatusCode();

            var jsonRes = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserProfile>(jsonRes);
            return result;
        }

        public async Task<ConversationMessage> GetLastMessageOfConversation(string conversationId,string token)
        {
            string lastMessageId = await this.GetLastMessageId("t_mid.$cAAdZrm4k4UZh9X1vd1bxDgkg7Bo9", token);
            return await this.GetMessage(lastMessageId, token);
        }

        private async Task<string> GetLastMessageId(string conversationId, string token)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{BASE_GRAPH_API_URL}/{conversationId}/messages?fields=id&limit=1&access_token={token}");
            response.EnsureSuccessStatusCode();

            var jsonRes = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FBPageResult<FBId>>(jsonRes);
            return result.Data.FirstOrDefault()?.Id;
        }

        public class FBId
        {
            public string Id { get; set; }
        }

        private async Task<ConversationMessage> GetMessage(string messageId,string token)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{BASE_GRAPH_API_URL}/{messageId}?fields=created_time,from,to,tags,id,message&access_token={token}");
            response.EnsureSuccessStatusCode();

            var jsonRes = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ConversationMessage>(jsonRes);
            return result;
        }

        public async Task<string> AddMessageToConversation(string conversationId, string message, string pageToken)
        {
            HttpClient client = new HttpClient();
            var param = JsonConvert.SerializeObject(new { Message = message });
            FormUrlEncodedContent contentPost = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("message",message)
            });
            HttpResponseMessage response = await client.PostAsync($"{BASE_GRAPH_API_URL}/{conversationId}/messages?access_token={pageToken}", contentPost);
            response.EnsureSuccessStatusCode();

            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            return result.GetValue("id").ToString();
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

        public class FBWebHookData
        {
            public string Object { get; set; }
            public List<FBHookDataEntry> Entry { get; set; }
        }

        public class FBHookDataEntry
        {
            public string Id { get; set; }
            public long Time { get; set; }
            public List<FBHookMessage> Messaging { get; set; }
            public List<FBHookChange> Changes { get; set; }
        }

        public class FBHookChange
        {
            public string Field { get; set; }
            public FBHookChangeValue Value { get; set; }
        }

        public class FBHookChangeValue
        {
            public string Item { get; set; }
            [JsonProperty("comment_id")]
            public string CommentId { get; set; }
            [JsonProperty("parent_id")]
            public string ParentId { get; set; }
            [JsonProperty("sender_name")]
            public string SenderName { get; set; }
            [JsonProperty("sender_id")]
            public string SenderId { get; set; }
            [JsonProperty("post_id")]
            public string PostId { get; set; }
            [JsonProperty("verb")]
            public string Verb { get; set; }
            [JsonProperty("created_time")]
            public long CreateTime { get; set; }
            [JsonProperty("is_hidden")]
            public bool IsHidden { get; set; }
            public string Message { get; set; }
        }

        public class FBHookMessage
        {
            public Profile Sender { get; set; }
            public Profile Recipient { get; set; }
            public long Timestamp { get; set; }
            public FBHookMessageContent Message { get; set; }
            public FBHookMessageDelivery Delivery { get; set; }
        }

        public class FBHookMessageDelivery
        {
            public string Watermark { get; set; }
            public long Seq { get; set; }
            public List<string> Mids { get; set; }
        }

        public class FBHookMessageContent
        {
            public string Mid { get; set; }
            public long Seq { get; set; }
            public string Text { get; set; }
        }

        public class Profile
        {
            public string Id { get; set; }
        }

        public class UserProfile
        {
            [JsonProperty("first_name")]
            public string FirstName { get; set; }
            [JsonProperty("last_name")]
            public string LastName { get; set; }
            [JsonProperty("profile_pic")]
            public string Picture { get; set; }
            public string Locale { get; set; }
            public string Timezone { get; set; }
            public string Gender { get; set; }
        }

        public class ConversationMessage
        {
            public MessageParticipant From { get; set; }
            public FBData<MessageParticipant> To { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            [JsonProperty("")]
            public DateTime CreatedTime { get; set; }
        }

        public class FBData<T>
        {
            public List<T> Data { get; set; }
        }

        public class MessageParticipant
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class FBPageResult<T>
        {
            public List<T> Data { get; set; }
            public FBPaging Paging { get; set; }
        }

        public class FBPaging
        {
            public FBCursor Cursors { get; set; }
            public string Next { get; set; }
        }
        public class FBCursor
        {
            public string Before { get; set; }
            public string After { get; set; }
        }
    }
}
