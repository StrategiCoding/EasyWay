using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Samples.Tests
{
    public sealed class SampleTest : TestBase<SampleModule, SampleModuleConfigurator>
    {
        public SampleTest() : base() { }

        [Fact]
        public void Test()
        {
            /*
            Given(x => x.UserId(Guid.NewGuid()))

            .And()
            .And()
            .And()
            .And()

            .When()

            .Then()

            .And()
            .And();
            */
            Assert.True(true);
        }
    }
}
