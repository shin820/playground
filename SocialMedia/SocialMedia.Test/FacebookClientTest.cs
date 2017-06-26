using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook;
using System.Dynamic;

namespace SocialMedia.Test
{
    [TestClass]
    public class FacebookClientTest
    {
        private const string PAGE_TOKEN = "EAAR8yzs1uVQBAEBWQbsXb8HBP7cEbkTZB7CuqvuQlU1lx0ZCmlZCoy25HsxahMcCGfi8PirSyv5ZA62rvnm21EdZC3PZBK4FXfSti6cc8zIPKMb06fdR15sJqteOW2cIzTV64ZBZBZAnDLBwkNvYszc497CafdqAZCNRaip8w5SjmZCBwZDZD";

        [TestMethod]
        public void ShouldAddPost()
        {
            var fb = new FacebookClient(PAGE_TOKEN);

            var feeds = fb.Get("me/feed");

            //// 获取用户信息
            //dynamic userInfo = fb.Get("/121361878431739?fields=id,name,first_name,last_name,picture,gender,email,location");

            dynamic tagged = fb.Get("/1974003879498745/tagged?fields=created_time,message,from{id,name,picture},to");

            //// 添加Post
            //dynamic parameters = new ExpandoObject();
            //parameters.message = "Hello World!";

            //dynamic result = fb.Post("me/feed", parameters);
            //var postId = result.id;

            //// 添加评论
            //parameters = new ExpandoObject();
            //parameters.message = "Hello Comments!";
            //result = fb.Post(string.Format("/{0}/comments", postId), parameters);
            //var commentId = result.id;

            //// 删除评论
            //fb.Delete(commentId);

            //// 删除Post
            //fb.Delete(postId);
        }
    }
}
