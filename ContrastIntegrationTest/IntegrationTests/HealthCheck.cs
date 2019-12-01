using System;
using Xunit;
using ContrastIntegrationTest.Utils;
using Newtonsoft.Json.Linq;

namespace ContrastIntegrationTest
{
    public class HealthCheck
    {
        [Fact]
        public void ServerIsOnline()
        {
            var endpoint = $"/{Config.OrganizationId}/applications";
            var request = Request.ContrastRequest(endpoint, "GET");
            dynamic responseContent = JObject.Parse(request.Content);

            foreach(var item in responseContent.applications)
            {
                if(item.app_id == Config.AppId)
                {

                    Assert.True(item.status == "online", "Server is not currently running");
                    Assert.True(item.active_attacks == 0, $"Server is actively being attacked with status: ${item.attack_label}");


                }
            }
        }
    }
}
