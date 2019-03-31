using System.Collections.Generic;
using seeWifi.Models;

namespace seeWifi.Interfaces
{
    public interface IReportsService
    {
        List<HotSpotReportModel> ListOfReports { get; }
    }
}
