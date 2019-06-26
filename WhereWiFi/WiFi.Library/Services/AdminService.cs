using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WiFi.Library.DataBaseAccess.IDataBaseAccess;
using WiFi.Library.Models;
using WiFi.Library.Models.ModelsForDB;
using WiFi.Library.Services.IServices;

namespace WiFi.Library.Services
{
    public class AdminService : IAdminService
    {
        private readonly IWiFiDbContextFactory _contextFactory;

        public AdminService(IWiFiDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<ApplicationUserDbModel> CreateUser(ApplicationUserDbModel applicationUserModel)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                await context.ApplicationUser.AddAsync(applicationUserModel);
                await context.SaveChangesAsync();
            }
            return applicationUserModel;
        }
        public async Task<ApplicationUserDbModel> ChangeUserRole(ApplicationUserDbModel applicationUserModel)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var checkId = applicationUserModel.Id;
                var user = await context.ApplicationUser.FindAsync(checkId);
                user.Login = applicationUserModel.Login;
                user.Password = applicationUserModel.Password;
                user.Email = applicationUserModel.Email;
                user.UserRole = applicationUserModel.UserRole;
                await context.SaveChangesAsync();
                return user;
            }
        }
        public async void DeleteUser(int id)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var user = await context.ApplicationUser.FindAsync(id);
                context.ApplicationUser.Remove(user);
                await context.SaveChangesAsync();
            } 
        }
        public async Task<List<ApplicationUserDbModel>> GetAllUsers()
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var user = await context.ApplicationUser.ToListAsync();
                return user;
            }
        }
        public async Task<ApplicationUserDbModel> GetUserById(int id)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var user = await context.ApplicationUser.FindAsync(id);
                return user;
            }
        }
    }


}
