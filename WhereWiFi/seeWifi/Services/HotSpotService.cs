using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
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
            //Bellow code responsible for signing in to file with skipping of adding new line at the end of file
            WriteAllLinesBetter(path, PreparingListToSaveInFile());   
            return hotspot;
        }
        private string[] PreparingListToSaveInFile()
        {
            List<string> list = new List<string>();
            foreach (var instance in _hotSpotList)
            {
                list.Add($"{instance.Id},{instance.LocationName},{instance.LatitudeX},{instance.LongitudeY}");
            }

            return list.ToArray();
        }
        private void WriteAllLinesBetter(string filePath, params string[] lines)
        {
            if (filePath==null)
            {
                throw new ArgumentException("The path to file is missing");
            }
            using (var stream = File.OpenWrite(path))
            using (StreamWriter writer= new StreamWriter(stream))
            {
                if (lines.Length>0)
                {
                    for (int i = 0; i < lines.Length-1; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                    writer.Write(lines[lines.Length-1]);
                }
            }
        }
        public List<HotSpotModel> GetAll()
        {
            return _hotSpotList;
        }
    }
}
