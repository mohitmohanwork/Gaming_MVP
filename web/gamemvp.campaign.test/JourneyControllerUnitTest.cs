using gamemvp.campaign.Services.Api.Journey.Models;
using gamemvp.campaign.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZNxt.Net.Core.Helpers;

namespace gamemvp.campaign.test
{
    [TestClass]
    public class JourneyControllerUnitTest
    {
        [TestMethod]
        public void AddJourney()
        {
            var request = new JourneyDbo()
            {
                name = "Journey 2",
                tenant_id = 1,
                event_Id = 1,
                event_display_name = "product view",
                
            }.ToJObject();

            var Journeyctrl = ControllerHelper.GetJourneyController(request);
            var response = Journeyctrl.AddJourney();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateJourney()
        {
            var request = new JourneyDbo()
            {
                name = "Journey 111111111",
                tenant_id = 1,
                event_Id = 1,
                event_display_name = "product view 1111111",
            }.ToJObject();

            var Journeyctrl = ControllerHelper.GetJourneyController(request);
            var response = Journeyctrl.UpdateJourney();

            request = new JourneyDbo()
            {
                name = "Journey 1",
                tenant_id = 1,
                event_Id = 1,
            }.ToJObject();

            response = Journeyctrl.UpdateJourney();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void InsertBulk()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new JourneyDbo()
                {
                    name = $"Journey {CommonUtility.RandomString(10)}",
                    tenant_id = 1,
                    event_Id = 1,
                    event_display_name = $"product view  {CommonUtility.RandomString(10)}",
                }.ToJObject();

                var Journeyctrl = ControllerHelper.GetJourneyController(request);
                var response = Journeyctrl.AddJourney();
                Assert.AreEqual("1", response["code"].ToString());
            }

        }
        [TestMethod]
        public void GetJourney()
        {

            var Journeyctrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["journey_id"] = "1" });
            var response = Journeyctrl.GetJourneyById();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetJourneyByFilterAll()
        {

            var Journeyctrl = ControllerHelper.GetJourneyController();
            var response = Journeyctrl.GetJourneyByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetJourneyByFilterName()
        {

            var Journeyctrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["name"] = "Journey 1" });
            var response = Journeyctrl.GetJourneyByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetJourneyByFilterPagesize()
        {

            var Journeyctrl = ControllerHelper.GetJourneyController(null, new System.Collections.Generic.Dictionary<string, string>() { ["currentpage"] = "2", ["pagesize"] = "20" });
            var response = Journeyctrl.GetJourneyByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void AddJourneyEngagement()
        {
            var request = new JourneyEngagementDbo()
            {
                name = "Journey 3",
               journey_id = 1,
                engagement_id = 1
                

            }.ToJObject();

            var Journeyctrl = ControllerHelper.GetJourneyController(request);
            var response = Journeyctrl.AddEngagementJourney();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void GetJourneyEngagement()
        {
            
            var Journeyctrl = ControllerHelper.GetJourneyController(querystring: new System.Collections.Generic.Dictionary<string, string>() { ["engagement_journey_id"] = "2"});
            var response = Journeyctrl.GetEngagementJourneyById();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void GetJourneyEngagementAll()
        {

            var Journeyctrl = ControllerHelper.GetJourneyController();
            var response = Journeyctrl.GetEngagementJourneyByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
