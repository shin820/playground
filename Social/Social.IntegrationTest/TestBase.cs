using Framework.Core;
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

        static TestBase()
        {
            DependencyResolver = new DependencyResolver();
            DependencyResolver.Install(new IntegrationTestInstaller());
        }
    }
}
