namespace SRMWebApiApp.Services {
    public interface IUtilityService {
        Task<int> GetActiveSecuritiesCount();
        Task<int> GetInactiveSecuritiesCount();
    }
}