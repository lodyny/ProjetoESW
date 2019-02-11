using System;
using Xunit;
using AdotAqui.Controllers;
using AdotAqui.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;

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
            var ctxAccessor = buildFakeHttpContextAccesor(new RequestCulture("pt-PT", "pt-PT"));
            HomeController controller = new HomeController(_context, ctxAccessor);
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        public HttpContextAccessor buildFakeHttpContextAccesor(RequestCulture requestCulture)
        {
            var ctxAccessor = new HttpContextAccessor();
            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, new AcceptLanguageHeaderRequestCultureProvider()));
            ctxAccessor.HttpContext = httpContext;
            return ctxAccessor;
        }


    }
}
