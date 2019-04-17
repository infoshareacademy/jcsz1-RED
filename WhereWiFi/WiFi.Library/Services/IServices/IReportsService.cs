using System.Collections.Generic;
using WiFi.Library.Models;

namespace WiFi.Library.Services.IServices
{
    public interface IReportsService
    {
        List<HotSpotReportModel> ListOfReports { get; }
        List<HotSpotReportModel> GetSuspiciousHotSpotsList();
        List<HotSpotReportModel> GetLowestCurrentHotSpotUsers();
    }
}
