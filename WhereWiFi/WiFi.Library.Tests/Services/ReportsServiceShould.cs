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
            var reportsListOfReports = reports.ListOfReports;
            var expectedResult = 5;

            //Act
            var sut = reportsListOfReports.OrderByDescending(s => (s.IncomingTransfer / s.CurrentHotSpotUsers))
            .Take(5)
            .ToList();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void ShouldGetFiveSuspiciousHotSpotByOutGoingTransfer()
        {
            //Arrange
            var reports = new ReportsService();
            var reportsListOfReports = reports.ListOfReports;
            var expectedResult = 5;

            //Act
            var sut = reportsListOfReports.OrderByDescending(s => s.OutgoingTransfer / s.CurrentHotSpotUsers)
           .Take(5).ToList();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void ShouldGetFiveSuspiciousHotSpotByTotalTranfer()
        {
            //Arrange
            var reports = new ReportsService();
            var reportsListOfReports = reports.ListOfReports;
            var expectedResult = 5;

            //Act
            var sut = reportsListOfReports
            .OrderByDescending(s => ((s.IncomingTransfer + s.OutgoingTransfer) / s.CurrentHotSpotUsers))
            .Take(5).ToList();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }

        [Fact]
        public void ShouldGetTenLowestCurrentHotSpotUser()
        {
            //Arrange
            var reports = new ReportsService();
            var reportsListOfReports = reports.ListOfReports;
            var expectedResult = 10;

            //Act
            var sut = reportsListOfReports
                .OrderBy(s => s.CurrentHotSpotUsers)
                .Take(10).ToList();

            //Assert
            Assert.Equal(expectedResult, sut.Count);
            sut.Should().NotContainNulls();
        }
        [Fact]
        public void ShouldGetSuspiciousHotSpotsListWithNoNulls()
        {
            //Arrange
            var listByOutGoingTransfer = new ReportsService().GetSuspiciousHotSpotByOutGoingTransfer();
            var listByIncomingTransfer = new ReportsService().GetSuspiciousHotSpotByIncomingTransfer();
            var listByTotalTransfer = new ReportsService().GetSuspiciousHotSpotByTotalTransfer();

            //Act
           var idListByOutGoingTransfer = listByOutGoingTransfer.Select(x => x.fakeID).ToList();
           var idListByIncomingTransfer = listByIncomingTransfer.Select(x => x.fakeID).ToList();
           var idListByTotalTransfer = listByTotalTransfer.Select(x => x.fakeID).ToList();
           var sut =
           listByOutGoingTransfer.Union(listByIncomingTransfer).Union(listByTotalTransfer).ToList();
           foreach (var hotSpot in sut)
           {
               hotSpot.SuspiciousByIncomingTransfer = idListByIncomingTransfer.Contains(hotSpot.fakeID);
               hotSpot.SuspiciousByOutgoingTransfer = idListByOutGoingTransfer.Contains(hotSpot.fakeID);
               hotSpot.SuspiciousByTotal = idListByTotalTransfer.Contains(hotSpot.fakeID);
           }
           //Assert
          sut.Should().NotContainNulls();
        }
    }
}
