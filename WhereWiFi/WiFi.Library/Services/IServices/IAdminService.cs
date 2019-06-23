using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiFi.Library.Models;
using WiFi.Library.Models.ModelsForDB;

namespace WiFi.Library.Services.IServices
{
    public interface IAdminService
    {
        Task<ApplicationUserDbModel> CreateUser(ApplicationUserDbModel applicationUserModel);
        Task<ApplicationUserDbModel> ChangeUserRole(int id, int role);
        void DeleteUser(int id);
    }
}
