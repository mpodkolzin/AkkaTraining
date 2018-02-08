using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akka.TestKit.VsTest;

namespace AkkaTrainingUnitTests
{
    [TestClass]
    public class ActorUnitTests : TestKit
    {
        public ActorUnitTests() : base(TestConfig.GetConfigString())
        {

        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
