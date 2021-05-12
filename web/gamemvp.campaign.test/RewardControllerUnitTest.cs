using gamemvp.campaign.Services.Api.Reward.Models;
using gamemvp.campaign.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZNxt.Net.Core.Helpers;

namespace gamemvp.campaign.test
{
    [TestClass]
    public class RewardControllerUnitTest
    {
        [TestMethod]
        public void AddReward()
        {
            var request = new RewardDbo()
            {
                name = "Reward 1",
                description = "Reward 1 desc"
            }.ToJObject();

            var Rewardctrl = ControllerHelper.GetRewardController(request);
            var response = Rewardctrl.AddReward();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateReward()
        {
            var request = new RewardDbo()
            {
                reward_id = 1,
                name = "Reward 11111",
                description = "Reward 11111 desc",
            }.ToJObject();

            var Rewardctrl = ControllerHelper.GetRewardController(request);
            var response = Rewardctrl.UpdateReward();

            request = new RewardDbo()
            {
                reward_id = 1,
                name = "Reward 1",
                description = "Reward 11111 desc",
            }.ToJObject();

            response = Rewardctrl.UpdateReward();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void InsertBulk()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new RewardDbo()
                {
                    name = $"Reward {CommonUtility.RandomString(10)}",
                    description = $"Reward {CommonUtility.RandomString(10)}",
                }.ToJObject();

                var Rewardctrl = ControllerHelper.GetRewardController(request);
                var response = Rewardctrl.AddReward();
                Assert.AreEqual("1", response["code"].ToString());
            }

        }
        [TestMethod]
        public void GetReward()
        {

            var Rewardctrl = ControllerHelper.GetRewardController(null, new System.Collections.Generic.Dictionary<string, string>() { ["reward_id"] = "1" });
            var response = Rewardctrl.GetRewardById();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetRewardByFilterAll()
        {

            var Rewardctrl = ControllerHelper.GetRewardController();
            var response = Rewardctrl.GetRewardByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetRewardByFilterName()
        {

            var Rewardctrl = ControllerHelper.GetRewardController(null, new System.Collections.Generic.Dictionary<string, string>() { ["name"] = "Reward 1" });
            var response = Rewardctrl.GetRewardByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetRewardByFilterPagesize()
        {

            var Rewardctrl = ControllerHelper.GetRewardController(null, new System.Collections.Generic.Dictionary<string, string>() { ["currentpage"] = "2", ["pagesize"] = "20" });
            var response = Rewardctrl.GetRewardByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
