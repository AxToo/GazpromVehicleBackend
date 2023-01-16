using System.Security.Claims;
using GazpromVehicleBackEnd.DataAccessLayer;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Shared.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace GazpromVehicleBackend.Repositories
{
    public class ChekListRepository : IChekListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public ChekListRepository(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context=context;
            _UserManager=userManager;
            _RoleManager=roleManager;
        }

        public async Task<List<ChekList>> GetChekListById() => await _context.ChekList.ToListAsync();

        public async Task AddChekListAsync(string Name)
        {

            ChekList cheklist = new ChekList(Name);
            _context.ChekList.Add(cheklist);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveChekList (string Name)
        {
            var ChekList = _context.ChekList.First(c => c.Name == Name);
            _context.ChekList.Remove(ChekList);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<ChekList>> GetChekList ()
        {
            var ChekList = _context.ChekList.ToListAsync();
            return await ChekList;
        }
    }
}