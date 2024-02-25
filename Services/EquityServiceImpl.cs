using SRMWebApiApp.Dtos;
using SRMWebApiApp.Data;
using Microsoft.EntityFrameworkCore;
namespace SRMWebApiApp.Services {
    public class EquityServiceImpl : IEquityService
    {
        private readonly IVP_3308_v3Context _context;

        public EquityServiceImpl(IVP_3308_v3Context context){
            _context = context;
        }

        async Task<EquityDto?> IEquityService.DeleteEquity(int id)
        {
            var deletedEntity = await _context.SecuritySummaries.FindAsync(id);
            if (deletedEntity is not null){
                var equityResult = await this.GetEquity(id);
                deletedEntity.IsActive = false;
                await _context.SaveChangesAsync();
                return equityResult;
            }
            
            return null;
        }

        async Task<EquityDto?> GetEquity(int id){
            var securitySummariesData =  await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true) && s.SecurityType != null && s.SecurityType.Equals("Equity") && s.SID == id)
                            .Select(s => new SecuritySummaryDto(){
                                SID = s.SID,
                                SecurityName = s.SecurityName,
                                SecurityDescription = s.SecurityDescription
                            })
                            .FirstOrDefaultAsync();
            
            if(securitySummariesData is not null){
                var securityDetailsEquitiesData = await _context.SecurityDetailsEquities
                            .Where(s=> s.SID == id)
                            .Select(s => new SecurityDetailsEquityDto(){
                                SID = s.SID,
                                PriceCurrency = s.PriceCurrency,
                                SharesOutstanding = s.SharesOutstanding
                            }).FirstOrDefaultAsync();
            
                var pricingDetailsData = await _context.PricingDetails
                            .Where(s=> s.SID == id)
                            .Select(p => new PricingDetailsDto(){
                                SID = p.SID,
                                OpenPrice = p.OpenPrice,
                                ClosePrice = p.ClosePrice
                            }).FirstOrDefaultAsync();

                var dividendHistoriesData = await _context.DividendHistories
                            .Where(s=> s.SID == id)                    
                            .Select(d => new DividendHistoryDto(){
                                SID = d.SID,
                                DeclaredDate = d.DeclaredDate
                            }).FirstOrDefaultAsync();
            
                var regulatoryDetailsData = await _context.RegulatoryDetails
                            .Where(r=> r.PFId == id)
                            .Select(r => new RegulatoryDetailDto(){
                                PFId = r.PFId,
                                PFCreditRating = r.PFCreditRating
                            }).FirstOrDefaultAsync();

                if (securityDetailsEquitiesData!= null && pricingDetailsData!=null && dividendHistoriesData != null && regulatoryDetailsData !=null){
                    return new EquityDto{
                            SecurityName = securitySummariesData.SecurityName,
                            SecurityDescription = securitySummariesData.SecurityDescription,
                            PriceCurrency = securityDetailsEquitiesData.PriceCurrency,
                            SharesOutstanding = securityDetailsEquitiesData.SharesOutstanding,
                            OpenPrice = pricingDetailsData.OpenPrice,
                            ClosePrice = pricingDetailsData.ClosePrice,
                            DeclaredDate = dividendHistoriesData.DeclaredDate,
                            PFCreditRating = regulatoryDetailsData.PFCreditRating
                    };
                }
                else{
                    return null;
                }
            }

            return null;
        }

        async Task<bool> IEquityService.DeleteEquityById(int id)
        {
            var deletedSecurity = await _context.SecuritySummaries.FindAsync(id);
            if (deletedSecurity is null){
                return false;
            }
            if (deletedSecurity.SecurityType == "Equity"){
                deletedSecurity.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        Task<EquityDto?> IEquityService.GetEquity(int id)
        {
            return this.GetEquity(id);
        }

        async Task<IEnumerable<EquityDto>> IEquityService.GetEquityData()
        {
             var SecuritySummariesData =   await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true) && s.SecurityType != null && s.SecurityType.Equals("Equity"))
                            .Select(s => new SecuritySummaryDto(){
                                SID = s.SID,
                                SecurityName = s.SecurityName,
                                SecurityDescription = s.SecurityDescription
                            })
                            .ToListAsync();

            var SecurityDetailsEquitiesData = await _context.SecurityDetailsEquities
                            .Select(s => new SecurityDetailsEquityDto(){
                                SID = s.SID,
                                PriceCurrency = s.PriceCurrency,
                                SharesOutstanding = s.SharesOutstanding
                            }).ToListAsync();
            
            var PricingDetailsData = await _context.PricingDetails
                            .Select(p => new PricingDetailsDto(){
                                SID = p.SID,
                                OpenPrice = p.OpenPrice,
                                ClosePrice = p.ClosePrice
                            }).ToListAsync();

            var DividendHistoriesData = await _context.DividendHistories
                            .Select(d => new DividendHistoryDto(){
                                SID = d.SID,
                                DeclaredDate = d.DeclaredDate
                            }).ToListAsync();
            
            var RegulatoryDetailsData = await _context.RegulatoryDetails
                            .Select(r => new RegulatoryDetailDto(){
                                PFId = r.PFId,
                                PFCreditRating = r.PFCreditRating
                            }).ToListAsync();

          var query = 
                            from d1 in SecuritySummariesData
                            join d2 in SecurityDetailsEquitiesData
                            on d1.SID equals d2.SID
                            join d3 in PricingDetailsData
                            on d1.SID equals d3.SID
                            join d4 in DividendHistoriesData
                            on d1.SID equals d4.SID
                            join d5 in RegulatoryDetailsData
                            on d1.SID equals d5.PFId
                            select new EquityDto{
                                SecurityName = d1.SecurityName,
                                SecurityDescription = d1.SecurityDescription,
                                PriceCurrency = d2.PriceCurrency,
                                SharesOutstanding = d2.SharesOutstanding,
                                OpenPrice = d3.OpenPrice,
                                ClosePrice = d3.ClosePrice,
                                DeclaredDate = d4.DeclaredDate,
                                PFCreditRating = d5.PFCreditRating
                            };

            return query;   
        
        }
    }
}