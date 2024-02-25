using Microsoft.AspNetCore.Mvc;
using SRMWebApiApp.Dtos;

namespace SRMWebApiApp.Services {
    public interface IBondService {
        Task<IEnumerable<BondDto>> GetBondData();
        Task<bool> DeleteBondById(int id);
        Task<BondDto?> DeleteBond(int id);
        Task<BondDto?> GetBond(int id);
    }
}