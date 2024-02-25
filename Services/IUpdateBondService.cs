using SRMWebApiApp.Dtos;

namespace SRMWebApiApp.Services {
    public interface IUpdateBondService {
        Task<UpdateBondDTO> UpdateBondData(UpdateBondDTO dto); 
    }
}