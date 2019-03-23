using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using seeWifi.Models;

namespace seeWifi.Interfaces
{
    public interface IHotSpotService
    {
        List<HotSpotModel> GetAll();
    }
}
