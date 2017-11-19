using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStackLayer.UnitTest.Services
{
    [TestClass]
    public class ContainerServiceTest
    {
        class TestOb
        {
            public string Name { get; set; }
        }

        class TestContainer : IContainer
        {
            public TestOb TestOb { get; set; }

            public TestContainer()
            {
            }

            public void Setup()
            {
                TestOb = new TestOb { Name = "_TestTest" };
            }
        }

        [TestMethod]
        public void Open()
        {
            var testOb = new ContainerService().Open<TestContainer, TestOb>();
            testOb.Name.Is( "_TestTest" );
        }
    }
}
