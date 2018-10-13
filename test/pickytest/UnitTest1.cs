using System;
using Xunit;
using pickypicky;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace pickytest
{
    public class RequestContentValidationPriorityTests
      : IClassFixture<WebApplicationFactory<pickypicky.Startup>>,
        IClassFixture<WebApplicationFactory<pickybuthelpful.Startup>>
    {
        private readonly WebApplicationFactory<pickypicky.Startup> pickypickyFactory;
        private readonly WebApplicationFactory<pickybuthelpful.Startup> pickybuthelpfulFactory;

        public RequestContentValidationPriorityTests(
              WebApplicationFactory<pickypicky.Startup> pickypickyFactory,
              WebApplicationFactory<pickybuthelpful.Startup> pickybuthelpfulFactory
            ) {
            this.pickypickyFactory = pickypickyFactory;
            this.pickybuthelpfulFactory = pickybuthelpfulFactory;
        }

        [Theory]
        [MemberData(nameof(ClientFactories))]
        public async Task I_can_put_and_get_things_if_they_are_json() {
          using (var client = clientFactory.CreateClient()) {
            var putResponse = await client.PutAsync("/api/values/3", new StringContent("\"value3\"", Encoding.UTF8, "application/json"));
            putResponse.EnsureSuccessStatusCode();
            var getResponse = await client.GetAsync("/api/values/3");
            getResponse.EnsureSuccessStatusCode();
            var value3 = await getResponse.Content.ReadAsStringAsync();
            Assert.Equal("value3", value3);
          }
        }

        [Theory]
        [MemberData(nameof(ClientFactories))]
        public async Task Putting_an_unsupported_media_type_elicits_an_HTTP_415_code() {
          using (var client = pickypickyFactory.CreateClient()) {
            var putResponse = await client.PutAsync("/api/values/4", new StringContent("\"value3\"", Encoding.UTF8, "application/gunk"));
            Assert.Equal(HttpStatusCode.UnsupportedMediaType,putResponse.StatusCode);
          }
        }
    }
}
