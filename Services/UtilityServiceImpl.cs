

using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Data;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Services {
    public class UtilityServiceImpl : IUtilityService
    {
        private readonly IVP_3308_v3Context _context;
        public UtilityServiceImpl(IVP_3308_v3Context context) {
            _context = context;
        }
        async Task<int> IUtilityService.GetActiveSecuritiesCount()
        {
            var result = await _context.SecuritySummaries
                .Where(s => s.IsActive == true)
                .CountAsync();
            return result;
        }

        async Task<int> IUtilityService.GetInactiveSecuritiesCount()
        {
            var result = await _context.SecuritySummaries
                .Where(s => s.IsActive == false)
                .CountAsync();
            return result;
        }
    }
}