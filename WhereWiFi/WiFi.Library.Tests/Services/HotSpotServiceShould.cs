using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using WiFi.Library.Models;
using WiFi.Library.Services;
using Xunit;


namespace WiFi.Library.Tests.Services
{
    public class HotSpotServiceShould
    {

        [Fact]
        public void ShouldAddHotSpot()
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
        public void ShouldGetHotspotById()
        {
            //Arrange
            var hotspot = new HotSpotService();
            //Act
            var sut = hotspot.GetById(100);
            //Assert
            sut.Number.Should().Be(100);
        }
        [Fact]
        public void ShouldUpdateOldDataInHotspot()
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
        public void ShouldGetAllFavorites()
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
        public void ShouldGetAllHotspots()
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
