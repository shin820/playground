using Facebook;
using Framework.Core;
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
    public class ConversationService : DomainService<Conversation>
    {
        private IRepository<Message> _messageRepo;
        private IRepository<SocialUserInfo> _socialUserInfoRepo;
        public ConversationService(
            IRepository<Message> messageRepo,
            IRepository<SocialUserInfo> socialUserInfoRepo
            )
        {
            _messageRepo = messageRepo;
            _socialUserInfoRepo = socialUserInfoRepo;
        }

        public void ProcessFbMessage(FbData fbData)
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

            foreach (var change in changes)
            {
                if (change.Field == "conversations" && change.Value.ThreadId != null)
                {
                    FacebookClient client = new FacebookClient();

                    var existingConversation = Repository.FindAll().FirstOrDefault(t => t.FacebookConversationId == change.Value.ThreadId && t.Status != ConversationStatus.Closed);
                    if (existingConversation != null)
                    {
                        existingConversation.IsRead = false;
                        existingConversation.Messages.Add(new Message
                        {
                            Type = MessageType.FacebookMessage
                        });
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
