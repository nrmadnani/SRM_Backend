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

        async Task<IEnumerable<EquityDto>> IEquityService.GetEquityData()
        {
             var data1 =   await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true))
                            .Select(s => new SecuritySummaryDto(){
                                SID = s.SID,
                                SecurityName = s.SecurityName,
                                SecurityDescription = s.SecurityDescription
                            })
                            .ToListAsync();

            var data2 = await _context.SecurityDetailsEquities
                            .Select(s => new SecurityDetailsEquityDto(){
                                SID = s.SID,
                                PriceCurrency = s.PriceCurrency,
                                SharesOutstanding = s.SharesOutstanding
                            }).ToListAsync();
            
            var data3 = await _context.PricingDetails
                            .Select(p => new PricingDetailsDto(){
                                SID = p.SID,
                                OpenPrice = p.OpenPrice,
                                ClosePrice = p.ClosePrice
                            }).ToListAsync();


          var query = 
                            from d1 in data1
                            join d2 in data2 
                            on d1.SID equals d2.SID
                            join d3 in data3
                            on d1.SID equals d3.SID
                            select new EquityDto{
                                SID = d1.SID,
                                SecurityName = d1.SecurityName,
                                SecurityDescription = d1.SecurityDescription,
                                PriceCurrency = d2.PriceCurrency,
                                SharesOutstanding = d2.SharesOutstanding,
                                OpenPrice = d3.OpenPrice,
                                ClosePrice = d3.ClosePrice
                            };

            return query;   
        
        }
    }
}