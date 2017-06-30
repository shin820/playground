using Social.Domain.DomainServices;
using Social.Domain.DomainServices.Facebook;
using Social.Domain.Entities;
using Social.Infrastructure.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Social.IntegrationTest.DomainServices.Facebook
{
    public class MessageConversationStrategyTest : TestBase
    {
        private string _pageToken = "EAAR8yzs1uVQBAEBWQbsXb8HBP7cEbkTZB7CuqvuQlU1lx0ZCmlZCoy25HsxahMcCGfi8PirSyv5ZA62rvnm21EdZC3PZBK4FXfSti6cc8zIPKMb06fdR15sJqteOW2cIzTV64ZBZBZAnDLBwkNvYszc497CafdqAZCNRaip8w5SjmZCBwZDZD";

        [Fact]
        public async Task ShouldGetLatestMessageFromConversation()
        {
            MessageConversationStrategy facebookService = DependencyResolver.Resolve<MessageConversationStrategy>();
            Message message = await facebookService.GetLastMessageFromConversationId(_pageToken, "t_mid.$cAAdZrm4k4UZh9X1vd1bxDgkg7Bo9");

            Assert.NotNull(message);
            Assert.NotNull(message.SocialId);
            Assert.True(message.SenderId > 0);
            Assert.NotNull(message.SenderSocialId);
            Assert.NotNull(message.SenderEmail);
            Assert.NotNull(message.FacebookConversationId);
        }
    }
}
