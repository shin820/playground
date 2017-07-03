using Framework.Core;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.IntegrationTest
{
    public class TestBase
    {
        protected static DependencyResolver DependencyResolver;
        protected FacebookAccount TestFacebookAccount;

        static TestBase()
        {
            DependencyResolver = new DependencyResolver();
            DependencyResolver.Install(new IntegrationTestInstaller());
        }

        public TestBase()
        {
            IRepository<FacebookAccount> socailAccountRepo = DependencyResolver.Resolve<IRepository<FacebookAccount>>();
            TestFacebookAccount = socailAccountRepo.FindAll().FirstOrDefault(t => t.SiteId == 10000 && t.SocialId == "1974003879498745");

            if (TestFacebookAccount == null)
            {
                TestFacebookAccount = new FacebookAccount
                {
                    Name = "Shin's Test",
                    SocialId = "1974003879498745",
                    Token = "EAAR8yzs1uVQBAEBWQbsXb8HBP7cEbkTZB7CuqvuQlU1lx0ZCmlZCoy25HsxahMcCGfi8PirSyv5ZA62rvnm21EdZC3PZBK4FXfSti6cc8zIPKMb06fdR15sJqteOW2cIzTV64ZBZBZAnDLBwkNvYszc497CafdqAZCNRaip8w5SjmZCBwZDZD",
                    SiteId = 10000,
                    IfConvertMessageToConversation = true,
                    IfConvertVisitorPostToConversation = true,
                    IfConvertWallPostToConversation = true,
                    IfEnable = true,
                };

                socailAccountRepo.Insert(TestFacebookAccount);
            }
        }
    }
}
