using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using WiFi.Library.DataBaseAccess;
using WiFi.Library.DataBaseAccess.IDataBaseAccess;
using WiFi.Library.Filepath;
using WiFi.Library.Models;
using WiFi.Library.Models.ModelsForDB;
using WiFi.Library.Services.IServices;

namespace WiFi.Library.Services
{
    public class HotSpotService : IHotSpotService
    {
        private readonly List<HotSpotModel> _hotSpotList;
        private readonly FilePath _filePath;
        private readonly IWiFiDbContextFactory _contextFactory;

        public HotSpotService() : this(new WiFiDbContextFactory())
        {
        }

        public HotSpotService(IWiFiDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _filePath = new FilePath();
            //_favoritesHotSpots = new List<HotSpotModel>();
            _hotSpotList = new List<HotSpotModel>();
            ReadDataFromFile();
            SetFavorites();
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

        private void SetFavorites()
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var favoriteHotSpotIds = context.HotSpotUsersFavorites.Select(x => x.HotSpotNumber).ToList();

                //var favoriteHotSpots = _hotSpotList.Where(x => favoriteHotSpotIds.Contains(x.Number));
                var favoriteHotSpots = _hotSpotList.Join(favoriteHotSpotIds, x => x.Number, y => y, (a, b) => a);

                foreach (var hotSpot in favoriteHotSpots)
                {
                    hotSpot.FavoriteHotSpot = true;
                }
            }
        }

        public HotSpotModel AddHotSpot(HotSpotModel hotspot)
        {
            hotspot.Number = _hotSpotList.Max(x => x.Number) + 1;
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
            using (var context = _contextFactory.GetDbContext())
            {
                var favoriteHotSpotIds = context.HotSpotUsersFavorites.Select(x => x.HotSpotNumber).ToList();

                //var favoriteHotSpots = _hotSpotList.Where(x => favoriteHotSpotIds.Contains(x.Number));
                var favoriteHotSpots = _hotSpotList.Join(favoriteHotSpotIds, x => x.Number, y => y, (a, b) => a);

                return favoriteHotSpots.ToList();
            }
        }

        public void MarkAsFavorite(int id)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var favoriteHotSpot = context.HotSpotUsersFavorites.SingleOrDefault(x => x.HotSpotNumber == id);
                if (favoriteHotSpot == null)
                {
                    context.Add(new HotSpotUserFavoriteDbModel
                        {
                            HotSpotNumber = id
                        }
                    );
                    context.SaveChanges();
                    _hotSpotList.Single(x => x.Number == id).FavoriteHotSpot = true;
                }
                else
                {
                    context.Remove(favoriteHotSpot);
                    context.SaveChanges();
                    _hotSpotList.Single(x => x.Number == id).FavoriteHotSpot = false;
                }
            }
        }

        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
    }
}
