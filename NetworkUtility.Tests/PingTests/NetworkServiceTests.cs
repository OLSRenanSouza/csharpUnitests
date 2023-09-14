using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _networkService;
        private readonly IDNS _dNS;
        public NetworkServiceTests()
        {
            _dNS = A.Fake<IDNS>();
            _networkService = new NetworkService(_dNS);
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            A.CallTo(() => _dNS.SendDNS()).Returns(false);
            //Act
            var result = _networkService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Ping");
            result.Should().Contain("Pin", Exactly.Once());
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnString()
        {
            //Act
            var result = _networkService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            //Act
            var result = _networkService.GetPingOptions();
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };

            //Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_GetPingOptionsList_ReturnObjectList()
        {
            //Act
            var result = _networkService.GetPingOptionsList();
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };

            //Assert
            //result.Should().BeOfType<PingOptions>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            int result = _networkService.PingTimeout(a, b);

            result.Should().Be(expected);
        }

    }

}
