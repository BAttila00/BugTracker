using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BugTrackerTests {
    #region snippet1
    public class IntegrationTests_01
        : IClassFixture<WebApplicationFactory<BugTracker.Web.Startup>> {
        private readonly WebApplicationFactory<BugTracker.Web.Startup> _factory;

        public IntegrationTests_01(WebApplicationFactory<BugTracker.Web.Startup> factory) {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Projects/Index")]
        [InlineData("/Projects/Delete")]
        [InlineData("/Issues/Details")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url) {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
    #endregion
}
