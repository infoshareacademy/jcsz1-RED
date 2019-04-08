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
        private List<HotSpotModel> _hotSpotList;
        private PathToFile _path;
        public HotSpotService()
        {
            _path=new PathToFile();
            _hotSpotList = new List<HotSpotModel>();
            ReadDataFromFile();
        }
        void ReadDataFromFile()
        {
            using (var reader = new StreamReader(_path.FullFilePath))
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
        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
    }
}
