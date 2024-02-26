using SRMWebApiApp.Dtos;

namespace SRMWebApiApp.Services {
    public interface IEquityService {
        Task<IEnumerable<EquityDto>> GetEquityData();

        Task<bool> DeleteEquityById(int id);
        Task<EquityDto?> DeleteEquity(int id);
        Task<EquityDto?> GetEquity(int id);
    }
}