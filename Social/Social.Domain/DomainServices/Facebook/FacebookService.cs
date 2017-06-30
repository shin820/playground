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
    public interface IFacebookService
    {
        Task Process(FbData fbData);
    }

    public class FacebookService : IFacebookService
    {
        private IRepository<IntegrationAccount> _socialAccountRepo;
        private IDependencyResolver _dependencyResolver;

        public FacebookService(
            IRepository<IntegrationAccount> socialAccountRepo,
            IDependencyResolver dependencyResolver
            )
        {
            _socialAccountRepo = socialAccountRepo;
            _dependencyResolver = dependencyResolver;
        }

        public async Task Process(FbData fbData)
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


            var strategies = _dependencyResolver.ResolveAll<IConversationSrategy>();

            foreach (var change in changes)
            {
                var socialAccount = _socialAccountRepo.FindAll().FirstOrDefault(t => t.SocialId == change.Value.PageId);
                if (socialAccount != null)
                {
                    var strategory = strategies.FirstOrDefault(t => t.IsMatch(change));
                    if (strategory != null)
                    {
                        await strategory.Process(socialAccount, change);
                    }
                }
            }
        }
    }
}
