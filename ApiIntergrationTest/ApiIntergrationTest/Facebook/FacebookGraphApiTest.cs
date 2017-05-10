using FacebookSdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTest;
using Xunit;
using static FacebookSdk.FacebookClient;

namespace ApiIntergrationTest.Facebook
{
    public class FacebookGraphApiTest
    {
        private const string TEST_PAGE_ID = "1974003879498745";
        private const string TEST_PAGE_TOKEN = "EAAR8yzs1uVQBAKGGunoQjgZAQgm7KuEkXDL1QGLE7BCUwK6VLsjakRqcK8pdhPvS6EzKQdRKU6i9BNK3LBYDFYB4J5C3hx2yFTlElpOZAins0rWHN8rBZBGO6K7kahUdbdwf2drYSpQEA4YHeF4CwcnqsJW3BqZAZAbNV9WbLbQZDZD";

        [Fact]
        public async Task ShouldSendMessage()
        {
            FacebookClient client = new FacebookClient();
            string result = await client.SendMessage(TEST_PAGE_TOKEN, "Hi, " + Guid.NewGuid().ToString(), "1415178501853862");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetLastMessageOfConversation()
        {
            FacebookClient client = new FacebookClient();
            var message = await client.GetLastMessageOfConversation("t_mid.$cAAdZrm4k4UZh9X1vd1bxDgkg7Bo9", TEST_PAGE_TOKEN);

            Assert.NotNull(message);
            Assert.NotNull(message.From);
            Assert.NotNull(message.To);
        }

        [Fact]
        public async Task ShouldAddMessageToConversation()
        {
            FacebookClient client = new FacebookClient();
            var messageId = await client.AddMessageToConversation("t_mid.$cAAdZrm4k4UZh9X1vd1bxDgkg7Bo9", "Hi," + Guid.NewGuid().ToString(), TEST_PAGE_TOKEN);

            Assert.NotNull(messageId);
        }

        [Fact]
        public async Task ShouldPublishPost()
        {
            FacebookClient client = new FacebookClient();
            string postId = await client.PublishPagePost(TEST_PAGE_ID, TEST_PAGE_TOKEN, $"This is a test post.{Guid.NewGuid().ToString()}");

            Assert.NotNull(postId);

            await client.Delete(postId, TEST_PAGE_TOKEN);
        }

        [Fact]
        public async Task ShouldPublishPostWithImage()
        {
            FacebookClient client = new FacebookClient();
            string postId = await client.PublishPagePost(
                TEST_PAGE_ID, 
                TEST_PAGE_TOKEN, 
                $"This is a test post.{Guid.NewGuid().ToString()}",
                "https://fb-s-c-a.akamaihd.net/h-ak-fbx/v/t39.1997-6/p128x128/851575_126362190881911_254357215_n.png?oh=ba066b4fe7f73e238ba52b7e34ff74c5&oe=5978ADA1&__gda__=1501374282_89dd8b85ac13a6ee6a64fbfdb1fecf41"
                );

            Assert.NotNull(postId);

            //await client.Delete(postId, TEST_PAGE_TOKEN);
        }

        [Fact]
        public async Task ShouldPublishComment()
        {
            FacebookClient client = new FacebookClient();
            string postId = await client.PublishPagePost(TEST_PAGE_ID, TEST_PAGE_TOKEN, $"This is for test post.{Guid.NewGuid().ToString()}");
            string commentId = await client.PublishComment(postId, TEST_PAGE_TOKEN, $"This is for test comment.{Guid.NewGuid().ToString()}");

            Assert.NotNull(commentId);

            await client.Delete(commentId, TEST_PAGE_TOKEN);
            await client.Delete(postId, TEST_PAGE_TOKEN);       
        }

        [Fact]
        public async Task ShouldSubscribeApp()
        {
            FacebookClient client = new FacebookClient();
            await client.SubscribeApp(TEST_PAGE_ID, TEST_PAGE_TOKEN);
        }

        [Fact]
        public void ShouldDeserializeData()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "message.json");
            string json = File.ReadAllText(path);

            FBWebHookData data = Newtonsoft.Json.JsonConvert.DeserializeObject<FBWebHookData>(json);

            Assert.NotNull(data);
        }

        [Fact]
        public async Task ShouldGetUserProfile()
        {
            FacebookClient client = new FacebookClient();
            UserProfile user = await client.GetUserProfile("1415178501853862", TEST_PAGE_TOKEN);

            Assert.NotNull(user);
        }

    }
}
