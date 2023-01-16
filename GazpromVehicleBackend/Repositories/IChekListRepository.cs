using System.Security.Claims;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Shared.Models.Responses;

namespace GazpromVehicleBackend.Repositories;

    
   
public interface IChekListRepository
{
    Task<List<ChekList>> GetChekListById();
    Task AddChekListAsync(string Name);
    Task RemoveChekList(string Name);
    Task<List<ChekList>> GetChekList();

}