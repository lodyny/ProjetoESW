using System;
using Xunit;
using AdotAqui.Controllers;
using AdotAqui.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Localization;

namespace XUnitTesting
{
    public class AnimalsControllerTestXUnit
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<AdotAquiDbContext> _options;
        private AdotAquiDbContext _context;

        public AnimalsControllerTestXUnit()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            var teste = _configuration.GetConnectionString("AzureConnection");
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
            var ctxAccessor = WebHelper.BuildFakeHttpContextAccesor(new RequestCulture("pt-PT", "pt-PT"));
            HomeController controller = new HomeController(_context, ctxAccessor);
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
