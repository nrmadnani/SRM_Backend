using SRMWebApiApp.Dtos;

namespace SRMWebApiApp.Services {
    public interface IEquityService {
        Task<IEnumerable<EquityDto>> GetEquityData();
        public Task<UpdateEquityDTO> UpdateEquityData(UpdateEquityDTO dto);
    }
}