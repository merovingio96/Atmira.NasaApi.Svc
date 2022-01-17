using Atmira.NasaApi.Svc.Services.V1.Asteroids;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Atmira.NasaApi.Svc.Test.Services
{
    public class AsteroidsServiceTests
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly IAsteroidsService _asteroidsService;

        public AsteroidsServiceTests()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _mapper = Substitute.For<IMapper>();

            _asteroidsService = new AsteroidsService(_httpClientFactory, _mapper);
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public void IsValidDay_ValuesLessThan1_ReturnFalse(int value)
        {
            bool isFalse = _asteroidsService.IsValidDay(value);

            Assert.False(isFalse);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void IsValidDay_ValuesGreaterThan7_ReturnFalse(int value)
        {
            bool isFalse = _asteroidsService.IsValidDay(value);

            Assert.False(isFalse);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void IsValidDay_ValuesBetween1And7_ReturnTrue(int value)
        {
            bool isTrue = _asteroidsService.IsValidDay(value);

            Assert.True(isTrue);
        }
    }
}
