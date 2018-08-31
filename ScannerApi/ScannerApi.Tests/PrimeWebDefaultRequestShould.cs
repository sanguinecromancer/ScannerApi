using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using ScannerApi.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Scanner.Core.Services;
using Xunit;

namespace ScannerApi.Tests
{
    public class PrimeWebDefaultRequestShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly ScannerService _scannerService = new ScannerService();
        private readonly URLContext _context;


        //public PrimeWebDefaultRequestShould()
        //{
        //    // Arrange
        //    _server = new TestServer(WebHost.CreateDefaultBuilder()
        //        .UseStartup<ScannerApi.Startup>());
        //    _client = _server.CreateClient();
        //}

        [Fact]
        public async Task TestVisitRoot()
        {
            // Arrange.
            var client = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()).CreateClient();

            // Act.
            var response = await client.GetAsync("/api/scanner");

            // Assert.
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }

        [Fact]
        public void CalculationOfFileSizeShouldReturnASize()
        {
            if (_scannerService.GetResponse("https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Grave_eend_maasmuur.jpg/1200px-Grave_eend_maasmuur.jpg"))
            {
                var length = _scannerService.GetFileSize();

                Assert.Equal(356296, length);
            }

        }

        [Fact]
        public void GetCheckSumShouldReturnAValue()
        {
            _scannerService.GetResponse("http://ipv4.download.thinkbroadband.com/5MB.zip");
            
                var sha1 = _scannerService.GetCheckSum();

                Assert.NotNull(sha1);
        }


    }

}