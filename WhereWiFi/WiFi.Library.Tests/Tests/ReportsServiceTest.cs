using FluentAssertions;
using Moq;
using seeWifi.Controllers;
using WiFi.Library.Services;
using WiFi.Library.Services.IServices;
using Xunit;

namespace WiFi.Library.Tests.Tests
{
    public class ReportsServiceShould
    {
        [Fact]
        public void Index_Should__Call_ReportsService()
        {
            //Arrange
            var reportsService = Mock.Of<IReportsService>();
            var reportsMock = Mock.Get(reportsService);
            var reportsController = new ReportsController(reportsService);

            //Act
            var responseResult = reportsController.Index();

            //Assert
            reportsMock.Verify(r => r.GetSuspiciousHotSpotsList(), Times.Once);
        }
        [Fact]
        public void Should_Get_Five_Suspicious_Hotspots_By_Incoming_Transfer()
        {
            //Arrange
            var reports = new ReportsService();
            var expectedResult = 5;

            //Act
            var sut = reports.GetSuspiciousHotSpotByIncomingTransfer();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void Should_Get_Five_Suspicious_Hotspots_By_Outgoing_Transfer()
        {
            //Arrange
            var reports = new ReportsService();
            var expectedResult = 5;

            //Act
            var sut = reports.GetSuspiciousHotSpotByOutGoingTransfer();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void Should_Get_Five_Suspicious_Hotspots_By_Total_Transfer()
        {
            //Arrange
            var reports = new ReportsService();
            var expectedResult = 5;

            //Act
            var sut = reports.GetSuspiciousHotSpotByTotalTransfer();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void ShouldGetTenLowestCurrentHotSpotUser()
        {
            //Arrange
            var reports = new ReportsService();
            var expectedResult = 10;

            //Act
            var sut = reports.GetLowestCurrentHotSpotUsers();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }
        [Fact]
        public void Should_Get_Suspicious_Hotspots_List_With_No_Nulls()
        {
            //Arrange
            var reports = new ReportsService();

            //Act
            var sut = reports.GetSuspiciousHotSpotsList();

            //Assert
            sut.Should().NotContainNulls();
        }
    }
}
