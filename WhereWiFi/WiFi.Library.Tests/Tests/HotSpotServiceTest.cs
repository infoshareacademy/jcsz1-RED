using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using seeWifi.Controllers;
using WiFi.Library.Models;
using WiFi.Library.Services;
using WiFi.Library.Services.IServices;
using Xunit;

namespace WiFi.Library.Tests.Tests
{
    public class HotSpotServiceTest
    {
    //    [Fact]
    //    public void Index_Should_Call_HotSpotService()
    //    {
    //        //Arrange
    //        var hotSpotService = Mock.Of<IHotSpotService>();
    //        var serviceMock = Mock.Get(hotSpotService);
    //        var hotSpotController = new HotSpotController(hotSpotService);

    //        //Act
    //        hotSpotController.Index();

    //        //Assert
    //        serviceMock.Verify(s => s.GetAll(), Times.Once);
    //    }
    //    [Fact]
    //    public void Index_Should_Return_Hotspots()
    //    {
    //        //Arrange
    //        var hotSpotService = Mock.Of<IHotSpotService>();
    //        var serviceMock = Mock.Get(hotSpotService);
    //        var hotSpotController = new HotSpotController(hotSpotService);
    //        var hotspots = new List<HotSpotModel>()
    //        {
    //            new HotSpotModel()
    //            {
    //                Number = 1,
    //                fakeID = "Fikcyjny",
    //                LocationName = "Grunwaldzka",
    //                LatitudeX = "54.4",
    //                LongitudeY = "18.5"
    //            }

    //        };
    //        serviceMock.Setup(s => s.GetAll()).Returns(hotspots);

    //        //Act
    //        var responseResult = (ViewResult)hotSpotController.Index();

    //        //Assert
    //        Assert.Equal(hotspots, responseResult.ViewData.Model);
    //    }
        [Fact]
        public void Should_Add_Hotspot()
        {
            //Arrange
            HotSpotService hs = new HotSpotService();
            HotSpotModel hotspot = new HotSpotModel()
            {
                LocationName = "Burzyńskiego 12",
                LatitudeX = "54",
                LongitudeY = "18",
            };
            //Act
            var sut = hs.AddHotSpot(hotspot);
            var lastHotSpot = hs.GetAll().TakeLast(1);
            //Assert
            Assert.Collection(lastHotSpot, item => Assert.Contains("Burzyńskiego", item.LocationName));
            Assert.Collection(lastHotSpot, item => Assert.Contains("54", item.LatitudeX));
            Assert.Collection(lastHotSpot, item => Assert.Contains("18", item.LongitudeY));
        }
        [Fact]
        public void Should_Get_Hotspot_By_Id()
        {
            //Arrange
            var hotspot = new HotSpotService();
            //Act
            var sut = hotspot.GetById(100);
            //Assert
            sut.Number.Should().Be(100);
        }
        [Fact]
        public void Should_Update_Old_Data_In_Hotspot()
        {
            //Arrange
            var hotspotService = new HotSpotService();
            var allHotSpots = hotspotService.GetAll();
            var updatedHotSpot = new HotSpotModel()
            {LocationName = "testing",
                LatitudeX = "111",
                LongitudeY = "222",
                //Number pop and FakeId prop are not taking care into consideration for the update purpose
                //Therefore it's values will be the same as used to be
                Number = 1,
                fakeID = "LOK001"
            };
            var currentHotSpot = hotspotService.GetById(1);
            //Act
            hotspotService.Update(1,updatedHotSpot);
            //Assert
            currentHotSpot.Should().BeEquivalentTo(updatedHotSpot);
        }
        [Fact]
        public void Should_Get_All_Favorites()
        {
            //Arrange
            var hotspot = new HotSpotService();
            var testingHotSpot = new HotSpotModel()
            {
                LocationName = "Test123",
            };
            hotspot.AddHotSpot(testingHotSpot);
            hotspot.MarkAsFavorite(testingHotSpot.Number);
            //Act
            var sut = hotspot.GetAllFavorites();
            //Assert
            Assert.Collection(sut, item => Assert.True(item.FavoriteHotSpot));
        }
        [Fact]
        public void Should_Get_All_Hotspots()
        {
            //Arrange
            var hotspot = new HotSpotService();
            //Act
            var sut = hotspot.GetAll();
            //Assert
            sut.Count.Should().BeGreaterOrEqualTo(1);
        }
    }
}
