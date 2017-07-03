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
        [Fact]
        public async Task ShouldGetLatestMessageFromConversation()
        {
            MessageConversationStrategy facebookService = DependencyResolver.Resolve<MessageConversationStrategy>();
            Message message = await FacebookService.GetLastMessageFromConversationId
                (TestFacebookAccount.Token, "t_mid.$cAAdZrm4k4UZh9X1vd1bxDgkg7Bo9");

            Assert.NotNull(message);
            Assert.NotNull(message.SocialId);
            Assert.True(message.SenderId > 0);
            Assert.NotNull(message.SenderSocialId);
            Assert.NotNull(message.SenderEmail);
            Assert.NotNull(message.FacebookConversationId);
        }
    }
}
