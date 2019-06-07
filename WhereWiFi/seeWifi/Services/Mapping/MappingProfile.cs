using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using seeWifi.ViewModels;
using WiFi.Library.Models;

namespace seeWifi.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotSpotViewModel, HotSpotModel>();
            CreateMap<HotSpotModel, HotSpotViewModel>();
        }
    }
}
