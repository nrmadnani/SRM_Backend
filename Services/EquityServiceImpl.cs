using SRMWebApiApp.Dtos;
using SRMWebApiApp.Data;
using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Models;
using System.Xml.Serialization;
namespace SRMWebApiApp.Services {
    public class EquityServiceImpl : IEquityService
    {
        private readonly IVP_3308_v3Context _context;

        public EquityServiceImpl(IVP_3308_v3Context context){
            _context = context;
        }

        async Task<IEnumerable<EquityDto>> IEquityService.GetEquityData()
        {
             var SecuritySummariesData =   await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true))
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
                                SID = d1.SID,
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