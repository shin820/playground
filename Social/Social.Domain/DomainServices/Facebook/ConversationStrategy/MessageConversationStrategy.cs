using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Infrastructure.Facebook;
using Framework.Core;
using Social.Domain.Entities;
using Social.Infrastructure.Enum;
using Facebook;

namespace Social.Domain.DomainServices.Facebook
{
    public class MessageConversationStrategy : IConversationSrategy
    {
        private IRepository<Conversation> _conversationRepo;
        private IRepository<Message> _messageRepo;
        private IRepository<FacebookAccount> _socialAccountRepo;
        private IRepository<SocialUserInfo> _socialUserRepo;

        public MessageConversationStrategy(
            IRepository<Conversation> conversationRepo,
            IRepository<Message> messageRepo,
            IRepository<FacebookAccount> socialAccountRepo,
            IRepository<SocialUserInfo> socialUserRepo
            )
        {
            _conversationRepo = conversationRepo;
            _messageRepo = messageRepo;
            _socialAccountRepo = socialAccountRepo;
            _socialUserRepo = socialUserRepo;
        }

        public bool IsMatch(FbChange change)
        {
            return change.Field == "conversations" && change.Value.ThreadId != null;
        }

        public async Task Process(SocialAccount socialAccount, FbChange change)
        {
            Message message = await this.GetLastMessageFromConversationId(socialAccount.Token, change.Value.ThreadId);

            // ignore if message is sent by social account itself.
            bool isSendByIntegrationAccont = message.SenderSocialId == socialAccount.SocialId;
            if (isSendByIntegrationAccont)
            {
                return;
            }

            // ignore if existed same message in conversation.
            bool isDuplicatedMessage = _messageRepo.FindAll().Any(t => t.SiteId == socialAccount.SiteId && t.SocialId == message.SocialId);
            if (isDuplicatedMessage)
            {
                return;
            }

            if (message.IsSendByAgent)
            {
                message.SenderId = socialAccount.Id;
            }
            else
            {
                var userInfo = await GetOrCreateSocialUser(socialAccount, message.SenderSocialId, message);
                message.SenderId = userInfo.Id;
            }

            var existingConversation = _conversationRepo.FindAll().FirstOrDefault(t => t.SiteId == socialAccount.SiteId && t.SocialId == change.Value.ThreadId && t.Status != ConversationStatus.Closed);
            message.SiteId = socialAccount.SiteId;
            if (existingConversation != null)
            {
                existingConversation.IfRead = false;
                existingConversation.Messages.Add(message);
                existingConversation.Status = ConversationStatus.PendingInternal;
                await _conversationRepo.UpdateAsync(existingConversation);
            }
            else
            {
                var conversation = new Conversation
                {
                    SocialId = change.Value.ThreadId,
                    Source = ConversationSource.FacebookMessage,
                    SiteId = socialAccount.SiteId,
                    Subject = GetSubject(message.Content),
                    SocialAccountId = socialAccount.Id
                };
                conversation.Messages.Add(message);
                await _conversationRepo.InsertAsync(conversation);
            }
        }

        private async Task<SocialUserInfo> GetOrCreateSocialUser(SocialAccount socialAccount, string socialUserId, Message message)
        {
            var socialUser = _socialUserRepo.FindAll().Where(t => t.SiteId == socialAccount.SiteId && t.SocialId == socialUserId).FirstOrDefault();
            var facebookUser = await GetUserInfo(socialAccount.Token, socialUserId, message);

            if (socialUser == null)
            {
                facebookUser.SiteId = socialAccount.SiteId;
                await _socialUserRepo.InsertAsync(facebookUser);
                return socialUser;
            }

            //if (socialUser != null)
            //{
            //    socialUser.Email = facebookUser.Email;
            //    socialUser.Avatar = facebookUser.Avatar;
            //    socialUser.UpdateTime = facebookUser.UpdateTime;
            //    await _socialUserRepo.UpdateAsync(socialUser);
            //    return socialUser;
            //}

            return socialUser;
        }

        private string GetSubject(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "No Subject";
            }

            return message.Length <= 200 ? message : message.Substring(200);
        }


        public async Task<SocialUserInfo> GetUserInfo(string token, string fbUserId, Message message)
        {
            FacebookClient client = new FacebookClient(token);
            string url = "/" + fbUserId + "?fields=id,name,first_name,last_name,picture,gender,email,location";
            dynamic userInfo = await client.GetTaskAsync(url);

            var user = new SocialUserInfo
            {
                Name = userInfo.name,
                SocialId = fbUserId,
                Email = message.SenderEmail
            };
            if (userInfo.picture != null && userInfo.picture.data.url != null)
            {
                user.Avatar = userInfo.picture.data.url;
            }

            return user;
        }


        public async Task<Message> GetLastMessageFromConversationId(string token, string fbConversationId)
        {
            Checker.NotNullOrWhiteSpace(fbConversationId, nameof(fbConversationId));

            FacebookClient client = new FacebookClient(token);
            string url = "/" + fbConversationId + "?fields=messages.limit(1){from,to,message,id,created_time,attachments},updated_time";
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

            return message;
        }
    }
}
