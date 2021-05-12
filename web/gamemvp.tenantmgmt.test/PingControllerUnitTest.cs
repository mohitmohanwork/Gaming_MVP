using gamemvp.tenantmgmt.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace gamemvp.tenantmgmt.test
{
    [TestClass]
    public class PingControllerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pingctrl = ControllerHelper.GetPingController();
            var response = pingctrl.Ping();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
