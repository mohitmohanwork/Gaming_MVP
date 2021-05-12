using em.ui.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace em.ui.test
{
    [TestClass]
    public class TrackUnitTest
    {
        [TestMethod]
        public void GetEventdata()
        {
            JObject request = new JObject();
            var trackCtrl = ControllerHelper.GetTrackController(request,new System.Collections.Generic.Dictionary<string, string>() { ["store"] = "1"});
            var eventdata = trackCtrl.GetEvent();
            Assert.IsNotNull(eventdata);
        }

        [TestMethod]
        public void PushTrackdata()
        {
            
            var request = new JObject();
            request["client_id"] = "ut-znxt-analytics001";
            request["event_name"] = "product_view";
            request["properties"] = new JArray() {
                                    new JObject() {
                                        ["key"] = "product_name",
                                        ["value"] = $"Hi-Tea Grey Embroidery Party Wear Gown for Kids Girls-{ZNxt.Net.Core.Helpers.CommonUtility.GetNewID()}",
                                    },new JObject() {
                                        ["key"] ="customer_name",
                                        ["value"] = "ut-Mohit Mohan",
                                    }
                                };
            
            var trackCtrl = ControllerHelper.GetTrackController(request);
            var eventdata = trackCtrl.trackevent();
            
            Assert.AreEqual(eventdata["code"].ToString(), "1");
        }
        [TestMethod]
        public void Getmockgames()
        {


            var trackCtrl = ControllerHelper.GetTrackController(new JObject());
            var eventdata = trackCtrl.Getmock();

            Assert.AreEqual(eventdata["code"].ToString(), "1");
        }

    }
}
