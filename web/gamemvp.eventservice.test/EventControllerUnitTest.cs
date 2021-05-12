using Blaash.Gaming.Service.Common;
using Blaash.Gaming.Service.Common.Models;
using gamemvp.eventservice.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;


namespace gamemvp.eventservice.test
{
    [TestClass]
    public class EventControllerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            EventTrackModel eventTrackModel = new EventTrackModel()
            {
                client_id = 8475,
                event_name = GameMvpCommonConsts.EventTypes.PLAYER_LOGIN,
                properties = new System.Collections.Generic.List<EventProperty>()
                {
                    new EventProperty()
                    {
                        key = GameMvpCommonConsts.CommonKeys.USER_ID , value = "user001"
                    },
                    new EventProperty()
                    {
                        key = GameMvpCommonConsts.CommonKeys.FIRST_NAME, value = "mohit"
                    },
                    new EventProperty()
                    {
                        key = GameMvpCommonConsts.CommonKeys.LAST_NAME, value = "mohan"
                    },
                    new EventProperty()
                    {
                        key = CommonConst.CommonField.EMAIL, value = "mohan@gamil.com"
                    },
                     new EventProperty()
                    {
                        key = CommonConst.CommonField.PHONE, value = "9999999999"
                    }
                }
            };
            var data = eventTrackModel.ToJObject();

            var eventctrl = ControllerHelper.GetEventController(data);
            var response = eventctrl.PushEvent();
            Assert.AreEqual("1", response["code"].ToString());
        }
    }
}
