using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using WiFi.Library.Models;
using WiFi.Library.Services;
using Xunit;


namespace WiFi.Library.Tests.Services
{
    public class ReportsServiceShould
    {
        [Fact]
        public void ShouldGetFiveSuspcicousHotSpotByIncomingTransfer()
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
        public void ShouldGetFiveSuspiciousHotSpotByOutGoingTransfer()
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
        public void ShouldGetFiveSuspiciousHotSpotByTotalTransfer()
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
        public void ShouldGetSuspiciousHotSpotsListWithNoNulls()
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
