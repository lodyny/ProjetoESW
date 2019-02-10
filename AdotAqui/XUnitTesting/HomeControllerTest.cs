using System;
using Xunit;
using AdotAqui.Controllers;
using AdotAqui.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace XUnitTesting
{

    public class HomeControllerTestXUnit
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<AdotAquiDbContext> _options;
        private AdotAquiDbContext _context;

        public HomeControllerTestXUnit()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<AdotAquiDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("AzureConnection"))
                .Options;
            InitializeDatabaseWithDataTest();
        }

        internal void InitializeDatabaseWithDataTest()
        {
            _context = new AdotAquiDbContext(_options);
        }


        [Fact]
        public void Index_ReturnsViewResult()
        {
            HomeController controller = new HomeController(_context);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
