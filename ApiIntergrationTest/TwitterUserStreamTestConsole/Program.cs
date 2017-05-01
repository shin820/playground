using System;
using Tweetinvi;
using Tweetinvi.Models;

namespace TwitterUserStreamTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUserStream();
        }

        private static void StartUserStream()
        {
            ITwitterCredentials creds =
                new TwitterCredentials("Mj6zNyYU0GGHcdAqAHv5q0oHi",
                "FBPUNsy5HYUdz4cRTFIST0FA0EBxi0bMPwCvae9KtIOxHenbn4",
                "855320911989194753-25EU8AmKqJw8HhPJYdCUcje2mat9UxV",
                "HQYSviXLSEFHZkF2xqj8R9KxWRtIHG3Tp4yBdjpEutUa3");
            var stream = Stream.CreateUserStream(creds);

            stream.StreamIsReady += (sender, args) =>
            {
                Console.WriteLine($"Stream is ready...");
            };

            stream.MessageReceived += (sender, args) =>
            {
                Console.WriteLine($"[{args.Message.CreatedAt}] {args.Message.SenderScreenName} : {args.Message.Text}");
            };

            stream.MessageSent += (sender, args) =>
            {
                Console.WriteLine($"[{args.Message.CreatedAt}] {args.Message.SenderScreenName} : {args.Message.Text}");
            };

            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                Console.WriteLine($"[{args.Tweet.CreatedAt}] {args.Tweet.CreatedBy.ScreenName} : {args.Tweet.Text}");
            };

            stream.StreamStopped += (sender, args) =>
            {
                Console.WriteLine($"Stream is stopped...");
            };

            stream.StartStream();
            Console.Read();
        }
    }
}