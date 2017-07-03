using Framework.Core;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DomainServices
{
    public interface ISocialUserInfoService
    {
        Task<SocialUserInfo> GetOrCreateSocialUser(SocialAccount socialAccount, Message message);
    }

    public class SocialUserInfoService : DomainService<SocialUserInfo>
    {
        private async Task<SocialUserInfo> GetOrCreateSocialUser(SocialAccount socialAccount, Message message)
        {
            var socialUser = Repository.FindAll().Where(t => t.SiteId == socialAccount.SiteId && t.SocialId == message.SenderSocialId).FirstOrDefault();
            var facebookUser = await FacebookService.GetUserInfo(socialAccount, message);

            if (socialUser == null)
            {
                facebookUser.SiteId = socialAccount.SiteId;
                await Repository.InsertAsync(facebookUser);
                return socialUser;
            }

            bool ifUserInfoUpdated =
                socialUser.Email != facebookUser.Email
                || socialUser.Avatar != facebookUser.Avatar;
            if (ifUserInfoUpdated)
            {
                socialUser.Email = facebookUser.Email;
                socialUser.Avatar = facebookUser.Avatar;
                await Repository.UpdateAsync(socialUser);
                return socialUser;
            }

            return socialUser;
        }
    }
}
