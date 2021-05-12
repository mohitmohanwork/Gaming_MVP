using gamemvp.campaign.Services.Api.Journey.Models;
using gamemvp.campaign.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZNxt.Net.Core.Helpers;

namespace gamemvp.campaign.test
{
    [TestClass]
    public class JourneyDetailsControllerUnitTest
    {
        [TestMethod]
        public void AddJourneyDetails()
        {
            var request = new JourneyDetailsDbo()
            {
                name = "JourneyDetails 1",
                journey_id = 1,
                description = "JourneyDetails 1 desc"
            }.ToJObject();

            var JourneyDetailsctrl = ControllerHelper.GetJourneyController(request);
            var response = JourneyDetailsctrl.AddJourneyDetails();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateJourneyDetails()
        {
            var request = new JourneyDetailsDbo()
            {
                journey_detail_id = 1,
               journey_id = 1,
                name = "JourneyDetails 11111",
                description = "JourneyDetails 11111 desc",
            }.ToJObject();

            var JourneyDetailsctrl = ControllerHelper.GetJourneyController(request);
            var response = JourneyDetailsctrl.UpdateJourneyDetails();

            request = new JourneyDetailsDbo()
            {
                journey_detail_id = 1,
                name = "JourneyDetails 1",
                description = "JourneyDetails 11111 desc",
            }.ToJObject();

            response = JourneyDetailsctrl.UpdateJourneyDetails();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void InsertBulk()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new JourneyDetailsDbo()
                {
                    name = $"JourneyDetails {CommonUtility.RandomString(10)}",
                    journey_id = 1,
                    description = $"JourneyDetails {CommonUtility.RandomString(10)}",
                }.ToJObject();

                var JourneyDetailsctrl = ControllerHelper.GetJourneyController(request);
                var response = JourneyDetailsctrl.AddJourneyDetails();
                Assert.AreEqual("1", response["code"].ToString());

            }


        }
        [TestMethod]
        public void GetJourneyDetails()
        {

            var JourneyDetailsctrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["JourneyDetails_id"] = "1" });
            var response = JourneyDetailsctrl.GetJourneyDetailsById();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetJourneyDetailsByFilterAll()
        {

            var JourneyDetailsctrl = ControllerHelper.GetJourneyController();
            var response = JourneyDetailsctrl.GetJourneydetailsByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetGameByFilterName()
        {

            var gamectrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["name"] = "JourneyDetails 1" });
            var response = gamectrl.GetJourneydetailsByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetGameByFilterPagesize()
        {

            var gamectrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["currentpage"] = "2", ["pagesize"] = "20" });
            var response = gamectrl.GetJourneydetailsByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
