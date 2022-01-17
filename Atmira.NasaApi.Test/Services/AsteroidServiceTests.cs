using Atmira.NasaApi.Svc.Services.V1.Asteroids;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Atmira.NasaApi.Test.Services
{
    [TestClass]
    public class AsteroidServiceTests
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly IAsteroidsService _asteroidService;

        public AsteroidServiceTests()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _mapper = Substitute.For<IMapper>();

            _asteroidService = new AsteroidsService(_httpClientFactory, _mapper);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-2)]
        public void Is()
        {
            
        }
    }
}
