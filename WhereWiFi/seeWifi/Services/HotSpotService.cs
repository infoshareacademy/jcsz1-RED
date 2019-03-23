using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json;
using seeWifi.Interfaces;
using seeWifi.Models;

namespace seeWifi.Services
{
    public class HotSpotService:IHotSpotService
    {
        private List<HotSpotModel> _hotSpotList;
        private string path = string.Empty;
        public HotSpotService()
        {
            path = "./data/wifigdansk.csv";
            _hotSpotList = new List<HotSpotModel>();
            using (var reader = new StreamReader(path))
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
                            Number = counter++,
                            Id = csv.GetField<string>(0),
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
            hotspot.Number = _hotSpotList.Max(x=>x.Number);
            _hotSpotList.Add(hotspot);

            List<string> output = new List<string>();
            output.Add($"{hotspot.Id},{hotspot.LocationName},{hotspot.LatitudeX},{hotspot.LongitudeY}");
            File.AppendAllLines(path, output);
            return hotspot;
        }

        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
    }
}
