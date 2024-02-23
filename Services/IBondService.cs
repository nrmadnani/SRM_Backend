using SRMWebApiApp.Dtos;

namespace SRMWebApiApp.Services {
    public interface IBondService {
        Task<IEnumerable<BondDto>> GetBondData();
    }
}