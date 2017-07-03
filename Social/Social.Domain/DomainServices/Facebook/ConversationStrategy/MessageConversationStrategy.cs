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
        private ISocialUserInfoService _socialUserInfoService;

        public MessageConversationStrategy(
            IRepository<Conversation> conversationRepo,
            IRepository<Message> messageRepo,
            ISocialUserInfoService socialUserInfoService
            )
        {
            _conversationRepo = conversationRepo;
            _messageRepo = messageRepo;
            _socialUserInfoService = socialUserInfoService;
        }

        public bool IsMatch(FbChange change)
        {
            return change.Field == "conversations" && change.Value.ThreadId != null;
        }

        public async Task Process(SocialAccount socialAccount, FbChange change)
        {
            Message message = await FacebookService.GetLastMessageFromConversationId(socialAccount.Token, change.Value.ThreadId);

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
                var userInfo = await _socialUserInfoService.GetOrCreateSocialUser(socialAccount, message);
                message.SenderId = userInfo.Id;
            }

            var existingConversation = _conversationRepo.FindAll().FirstOrDefault(t => t.SiteId == socialAccount.SiteId && t.SocialId == change.Value.ThreadId && t.Status != ConversationStatus.Closed);
            message.SiteId = socialAccount.SiteId;
            message.Attachments.Foreach(t => t.SiteId = message.SiteId);
            message.Shares.Foreach(t => t.SiteId = message.SiteId);
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
                    SocialAccountId = socialAccount.Id,
                    LastMessageSendBy = message.SenderId,
                    LastMessageSentTime = message.SendTime
                };
                conversation.Messages.Add(message);
                await _conversationRepo.InsertAsync(conversation);
            }
        }

        private string GetSubject(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "No Subject";
            }

            return message.Length <= 200 ? message : message.Substring(200);
        }
    }
}
