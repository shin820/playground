using Facebook;
using Framework.Core;
using Social.Domain.DomainServices.Facebook;
using Social.Domain.Entities;
using Social.Infrastructure.Enum;
using Social.Infrastructure.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DomainServices
{
    public interface IFacebookService : ITransient
    {
        Task ProcessWebHookData(FbData fbData);
    }

    public class FacebookService : IFacebookService
    {
        private IRepository<FacebookAccount> _socialAccountRepo;
        private IConversationStrategyFactory _strategyFactory;

        public FacebookService(
            IRepository<FacebookAccount> socialAccountRepo,
            IConversationStrategyFactory strategyFactory
            )
        {
            _socialAccountRepo = socialAccountRepo;
            _strategyFactory = strategyFactory;
        }

        public async Task ProcessWebHookData(FbData fbData)
        {
            if (fbData == null || !fbData.Entry.Any())
            {
                return;
            }

            var changes = fbData.Entry.First().Changes;
            if (changes == null || !changes.Any())
            {
                return;
            }

            if (fbData.Object != "page")
            {
                return;
            }

            foreach (var change in changes)
            {
                var socialAccount = _socialAccountRepo.FindAll().FirstOrDefault(t => t.SocialId == change.Value.PageId);

                if (socialAccount != null)
                {
                    var strategory = _strategyFactory.Create(change);
                    if (strategory != null)
                    {
                        await strategory.Process(socialAccount, change);
                    }
                }
            }
        }

        public static async Task<SocialUserInfo> GetUserInfo(SocialAccount socialAccount, Message message)
        {
            FacebookClient client = new FacebookClient(socialAccount.Token);
            string url = "/" + message.SenderSocialId + "?fields=id,name,first_name,last_name,picture,gender,email,location";
            dynamic userInfo = await client.GetTaskAsync(url);

            var user = new SocialUserInfo
            {
                Name = userInfo.name,
                SocialId = message.SenderSocialId,
                Email = message.SenderEmail
            };
            if (userInfo.picture != null && userInfo.picture.data.url != null)
            {
                user.Avatar = userInfo.picture.data.url;
            }

            return user;
        }

        public async static Task<Message> GetLastMessageFromConversationId(string token, string fbConversationId)
        {
            Checker.NotNullOrWhiteSpace(fbConversationId, nameof(fbConversationId));

            FacebookClient client = new FacebookClient(token);
            string url = "/" + fbConversationId + "?fields=messages.limit(1){from,to,message,id,created_time,attachments,shares{link,name,id}}";
            dynamic conversation = await client.GetTaskAsync(url);
            dynamic fbMessage = conversation.messages.data[0];

            var message = new Message
            {
                SocialId = fbMessage.id,
                SendTime = Convert.ToDateTime(fbMessage.created_time).ToUniversalTime(),
                SenderSocialId = fbMessage.from.id,
                SenderEmail = fbMessage.from.email,
                Type = MessageType.FacebookMessage,
                Content = fbMessage.message,
                FacebookConversationId = conversation.id,
            };

            if (fbMessage.attachments != null)
            {
                foreach (dynamic attachmnent in fbMessage.attachments.data)
                {
                    var messageAttachment = new MessageAttachment
                    {
                        SocialId = attachmnent.id,
                        MimeType = attachmnent.mime_type,
                        Name = attachmnent.name,
                    };
                    if (attachmnent.image_data != null)
                    {
                        messageAttachment.ImageWidth = attachmnent.image_data.width;
                        messageAttachment.ImageHeight = attachmnent.image_data.height;
                        messageAttachment.ImageMaxWidth = attachmnent.image_data.max_width;
                        messageAttachment.ImageMaxHeight = attachmnent.image_data.max_height;
                        messageAttachment.ImageUrl = attachmnent.image_data.url;
                        messageAttachment.ImagePreviewUrl = attachmnent.image_data.preview_url;
                    }

                    message.Attachments.Add(messageAttachment);
                }
            }

            if (fbMessage.shares != null)
            {
                foreach (dynamic share in fbMessage.shares.data)
                {
                    var messageShare = new MessageShare
                    {
                        Link = share.link,
                    };

                    message.Shares.Add(messageShare);
                }
            }

            return message;
        }
    }
}
