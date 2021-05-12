using gamemvp.campaign.Services.Api.Campaign.Models;
using gamemvp.campaign.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZNxt.Net.Core.Helpers;

namespace gamemvp.campaign.test
{
    [TestClass]
    public class CampaignControllerUnitTest
    {
        [TestMethod]
        public void AddCampaign()
        {
            var request = new CampaignDbo()
            {
                display_name = "Campaign 3",
                dynamic_name = "Campaign 3",
                short_name = "CAPM3",
                description = "Campaign 3 desc",
                startdate = CommonUtility.GetUnixTimestamp(DateTime.UtcNow),
                enddate = CommonUtility.GetUnixTimestamp(DateTime.UtcNow.AddDays(30)),
                budget_days = 30,
                budget_per_day = 100,
               // customer_segment_id = 1,
                is_tournament_type = false,
                purchase_rule_id = 1,
                tenant_id = 1,
                engagement_status_id =1

            }.ToJObject();

            var Campaignctrl = ControllerHelper.GetCampaignController(request);
            var response = Campaignctrl.AddCampaign();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateCampaign()
        {
            var request = new CampaignDbo()
            {
                engagement_id = 1,
                dynamic_name = "Campaign 1000",
              //  short_name = "CAPM1",
                description = "Campaign 11111 desc",
            }.ToJObject();

            var Campaignctrl = ControllerHelper.GetCampaignController(request);
            var response = Campaignctrl.UpdateCampaign();
            
            Assert.AreEqual("1", response["code"].ToString());
            request = new CampaignDbo()
            {
                engagement_id = 1,
                display_name = "Campaign 1",
                dynamic_name = "Campaign 1",
                short_name = "CAPM1",
                description = "Campaign 11111 desc",
            }.ToJObject();
            
            response = Campaignctrl.UpdateCampaign();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateCampaignStatus()
        {
            var request = new CampaignDbo()
            {
                engagement_id = 1,
                engagement_status_id = 2
            }.ToJObject();

            var Campaignctrl = ControllerHelper.GetCampaignController(request);
            var response = Campaignctrl.UpdateCampaignStatus();

            Assert.AreEqual("1", response["code"].ToString());
        }

        [TestMethod]
        public void InsertBulk()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new CampaignDbo()
                {
                    display_name = $"Campaign {CommonUtility.RandomString(10)}",
                    dynamic_name = $"Campaign {CommonUtility.RandomString(10)}",
                    short_name = $"{CommonUtility.RandomString(5)}",
                    description = $"Campaign {CommonUtility.RandomString(10)}",
                    startdate = CommonUtility.GetUnixTimestamp(DateTime.UtcNow),
                    enddate = CommonUtility.GetUnixTimestamp(DateTime.UtcNow.AddDays(30)),
                    budget_days = 30,
                    budget_per_day = 100,
                  //  customer_segment_id = 1,
                    is_tournament_type = false,
                    purchase_rule_id = 1,
                    tenant_id = 1,
                    engagement_status_id= 1
                }.ToJObject();

                var Campaignctrl = ControllerHelper.GetCampaignController(request);
                var response = Campaignctrl.AddCampaign();
                Assert.AreEqual("1", response["code"].ToString());
            }

        }
        [TestMethod]
        public void GetCampaign()
        {

            var Campaignctrl = ControllerHelper.GetCampaignController(null, new System.Collections.Generic.Dictionary<string, string>() { ["campaign_id"] = "1" });
            var response = Campaignctrl.GetCampaignById();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetCampaignByFilterAll()
        {

            var Campaignctrl = ControllerHelper.GetCampaignController();
            var response = Campaignctrl.GetCampaignByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetCampaignByFilterName()
        {

            var Campaignctrl = ControllerHelper.GetCampaignController(null, new System.Collections.Generic.Dictionary<string, string>() { ["name"] = "Campaign 1" });
            var response = Campaignctrl.GetCampaignByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetCampaignByFilterPagesize()
        {

            var Campaignctrl = ControllerHelper.GetCampaignController(null, new System.Collections.Generic.Dictionary<string, string>() { ["currentpage"] = "2", ["pagesize"] = "20" });
            var response = Campaignctrl.GetCampaignByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
