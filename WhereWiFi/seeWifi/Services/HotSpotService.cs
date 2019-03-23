using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        public HotSpotService()
        {
            _hotSpotList = new List<HotSpotModel>();
            using (var reader = new StreamReader("./data/wifigdansk.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = ",";
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new List<HotSpotModel>()
                    {
                        new HotSpotModel()
                        {
                            Id = csv.GetField<string>(0),
                            LocationName = csv.GetField<string>(1),
                            LatitudeX = double.Parse(csv.GetField<string>(2),CultureInfo.InvariantCulture),
                            LongitudeY=double.Parse(csv.GetField<string>(3),CultureInfo.InvariantCulture)

                        }
                    };
                    _hotSpotList.Add(record[0]);
                }
            }  
        }
        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
        public string JsonResult()
        {
            return JsonConvert.SerializeObject(_hotSpotList);
        }

   
    }
}
