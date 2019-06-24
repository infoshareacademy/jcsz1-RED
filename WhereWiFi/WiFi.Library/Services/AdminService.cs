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
        public async Task<ApplicationUserDbModel> ChangeUserRole(int id, int role)
        {
            using (var context = _contextFactory.GetDbContext())
            {
                var user = await context.ApplicationUser.FindAsync(id);
                switch (role)
                {
                    case 0:
                        user.UserRole = Role.Admin;
                        break;
                    case 1:
                        user.UserRole = Role.Developer;
                        break;
                    case 2:
                        user.UserRole = Role.PremiumUser;
                        break;
                    case 3:
                        user.UserRole = Role.BasicUser;
                        break;
                    default:
                        user.UserRole = Role.Unknown;
                        break;
                }
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
    }


}
