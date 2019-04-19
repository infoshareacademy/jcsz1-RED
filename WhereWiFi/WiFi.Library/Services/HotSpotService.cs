using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using WiFi.Library.Filepath;
using WiFi.Library.Models;
using WiFi.Library.Services.IServices;

namespace WiFi.Library.Services
{
    public class HotSpotService:IHotSpotService
    {
        private readonly List<HotSpotModel> _hotSpotList;
        private readonly List<HotSpotModel> _favoritesHotSpots;
        private readonly FilePath _filePath;
        public HotSpotService()
        {
            _filePath =new FilePath();
            _favoritesHotSpots = new List<HotSpotModel>();
            _hotSpotList = new List<HotSpotModel>();
            ReadDataFromFile();
        }
        void ReadDataFromFile()
        {
            using (var reader = new StreamReader(_filePath.FullFilePath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = ",";
                var counter = 0;
                while (csv.Read())
                {
                    var record = new List<HotSpotModel>()
                    {
                        new HotSpotModel()
                        {
                            Number = ++counter,
                            fakeID = csv.GetField<string>(0),
                            LocationName = csv.GetField<string>(1),
                            LatitudeX = csv.GetField<string>(2),
                            LongitudeY=csv.GetField<string>(3),
                            FavoriteHotSpot = false
                        }
                    };
                    _hotSpotList.Add(record[0]);
                }
            }
        }
        public HotSpotModel AddHotSpot(HotSpotModel hotspot)
        {
            hotspot.Number = _hotSpotList.Max(x=>x.Number)+1;
            _hotSpotList.Add(hotspot);
            return hotspot;
        }
        public HotSpotModel GetById(int id)
        {
            return _hotSpotList.First(x => x.Number == id);
        }
        public bool Update(int id, HotSpotModel hotSpotById)
        {
            var currentHotSpot = GetById(id);
            currentHotSpot.LocationName = hotSpotById.LocationName;
            currentHotSpot.LatitudeX = hotSpotById.LatitudeX;
            currentHotSpot.LongitudeY = hotSpotById.LongitudeY;
            return true;
        }

        public List<HotSpotModel> GetAllFavorites()
        {
            return _favoritesHotSpots;
        }

        public void MarkAsFavorite(int id)
        {
            var currentHotSpot = GetById(id);
            if (currentHotSpot.FavoriteHotSpot)
            {
                currentHotSpot.FavoriteHotSpot = false;
                _favoritesHotSpots.Remove(GetById(id));
            }
            else if (currentHotSpot.FavoriteHotSpot==false)
            {
                currentHotSpot.FavoriteHotSpot = true;
                _favoritesHotSpots.Add(GetById(id));
            }
        }

        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
    }
}
