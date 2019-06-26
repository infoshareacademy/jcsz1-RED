using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFi.Library.Models.RestModels
{
    public class RestReportsModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
    }
}
