using gamemvp.tenantmgmt.Services.Api.Tenant.Modules;
using gamemvp.tenantmgmt.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Helpers;

namespace gamemvp.tenantmgmt.test
{
    [TestClass]
    public class TenantControllerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var request = new TenantDbo() {
             name = "Tenant3", 
             description = "Tenant3",
             email = "Tenant1@gmail.com",
             phone_number = "999999999",
             status = TenantStatus.active,
            }.ToJObject();

            var tenantctrl = ControllerHelper.GetTenantController(request);
            var response = tenantctrl.AddTenant();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void AddClientToTenant()
        {
            var request = new TenantClientDbo()
            {
                tenant_id = 1,
                client_id = "002",
                is_active = true
              
            }.ToJObject();

            var tenantctrl = ControllerHelper.GetTenantController(request);
            var response = tenantctrl.AddTenantClient();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetClientTenant()
        {
            var tenantctrl = ControllerHelper.GetTenantController(null, new System.Collections.Generic.Dictionary<string, string>() { ["client_id"] = "001" });
            var response = tenantctrl.GetTenantClient();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
