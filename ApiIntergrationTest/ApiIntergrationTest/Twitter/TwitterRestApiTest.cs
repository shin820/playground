using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Tweetinvi;
using Tweetinvi.Models;
using System.IO;
using Tweetinvi.Parameters;
using System.Linq;

namespace ApiIntergrationTest.Twitter
{
    public class TwitterRestApiTest : IDisposable
    {
        private Stack<ITweet> PendingDestroyTweets = new Stack<ITweet>();

        public TwitterRestApiTest()
        {
            ITwitterCredentials creds =
                new TwitterCredentials("Mj6zNyYU0GGHcdAqAHv5q0oHi",
                "FBPUNsy5HYUdz4cRTFIST0FA0EBxi0bMPwCvae9KtIOxHenbn4",
                "855320911989194753-25EU8AmKqJw8HhPJYdCUcje2mat9UxV",
                "HQYSviXLSEFHZkF2xqj8R9KxWRtIHG3Tp4yBdjpEutUa3");
            Auth.SetCredentials(creds);
        }

        public void Dispose()
        {
            foreach (ITweet tweet in PendingDestroyTweets)
            {
                Tweet.DestroyTweet(tweet);
            }
        }

        [Fact]
        public void ShouldPublishTwit()
        {
            ITweet myTweet = Tweet.PublishTweet("Test publish tweet.");
            PendingDestroyTweets.Push(myTweet);

            Assert.NotNull(myTweet);
        }

        [Fact]
        public void ShouldPublishTwitWithImage()
        {
            // publish tweet with image
            var picturePath = Path.Combine(AppContext.BaseDirectory, "picture.jpg");
            byte[] image = File.ReadAllBytes(picturePath);
            ITweet myTweet = Tweet.PublishTweetWithImage("Test publish tweet with image.", image);
            PendingDestroyTweets.Push(myTweet);

            // assert
            Assert.NotNull(myTweet);
            Assert.Equal(1, myTweet.Media.Count);
        }

        [Fact]
        public void ShouldReplyTweet()
        {
            ITweet origin = Tweet.PublishTweet("This is a original tweet for testing reply.");
            PendingDestroyTweets.Push(origin);
            ITweet replyTweet = Tweet.PublishTweetInReplyTo("Test reply a tweet.", origin);
            PendingDestroyTweets.Push(replyTweet);

            Assert.Equal(origin.Id, replyTweet.InReplyToStatusId);
        }

        [Fact]
        public void ShouldPublishRetweet()
        {
            ITweet origin = Tweet.PublishTweet("This is a original tweet for testing retweet.");
            PendingDestroyTweets.Push(origin);
            ITweet retweet = Tweet.PublishRetweet(origin);
            PendingDestroyTweets.Push(retweet);

            Assert.Equal(origin.Id, retweet.RetweetedTweet.Id);
        }

        [Fact]
        public void ShouldQuoteTweet()
        {
            ITweet origin = Tweet.PublishTweet("This is a original tweet for testing quote.");
            PendingDestroyTweets.Push(origin);
            ITweet quoteTweet = Tweet.PublishTweet("Test quote a tweet.", new PublishTweetOptionalParameters
            {
                QuotedTweet = origin
            });
            PendingDestroyTweets.Push(quoteTweet);

            Assert.Equal(origin.Id, quoteTweet.QuotedStatusId);
        }

        [Fact]
        public void ShouldFavouriteTweet()
        {
            ITweet origin = Tweet.PublishTweet("Test favourite a tweet.");
            PendingDestroyTweets.Push(origin);

            Tweet.FavoriteTweet(origin);

            // assert
            ITweet favouritedTweet = Tweet.GetTweet(origin.Id);
            Assert.Equal(1, favouritedTweet.FavoriteCount);
        }

        [Fact]
        public void ShouldSendPrivateMessage()
        {
            Message.PublishMessage("hello shin (api test)", User.GetUserFromScreenName("shin___liu"));
            var messsage = Message.GetLatestMessagesSent(1).First();

            Assert.Equal("hello shin (api test)", messsage.Text);

            Message.DestroyMessage(messsage);
        }

        [Fact]
        public void ShouldGetUserInfo()
        {
            var user = User.GetUserFromScreenName("shin___liu");
            var b = user;
        }
    }
}
