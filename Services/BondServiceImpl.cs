using SRMWebApiApp.Data;
using SRMWebApiApp.Dtos;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Services {
    public class BondServiceImpl : IBondService
    {
        private readonly IVP_3308_v3Context _context;

        public BondServiceImpl(IVP_3308_v3Context context) {
            _context = context;
        }
        async Task<IEnumerable<BondDto>> IBondService.GetBondData()
        {
            var SecuritySummariesData =  await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true) && s.SecurityType != null && s.SecurityType.Equals("Bond"))
                            .Select(s => new SecuritySummaryDto(){
                                SID = s.SID,
                                SecurityName = s.SecurityName,
                                SecurityDescription = s.SecurityDescription
                            })
                            .ToListAsync();

            var BondDetailsData =  await _context.SecurityDetailsBonds
                            .Select(s => new SecurityDetailsBondDto(){
                                SID = s.SID,
                                CouponRate = s.CouponRate,
                                IsCallable = s.IsCallable,
                                MaturityDate = s.MaturityDate,
                                PenultimateCouponDate = s.PenultimateCouponDate
                            })
                            .ToListAsync();
            
            var RegulatoryData =  await _context.RegulatoryDetails
                            .Select(r => new RegulatoryDetailDto(){
                                PFId = r.PFId,
                                PFCreditRating = r.PFCreditRating
                            })
                            .ToListAsync();

            var PricingDetailsData =  await _context.PricingDetails
                            .Select(p => new PricingDetailsDto(){
                                SID = p.SID,
                                AskPrice = p.AskPrice,
                                BidPrice = p.BidPrice
                            })
                            .ToListAsync();

            var query = from d1 in SecuritySummariesData
                                             join d2 in BondDetailsData
                                             on d1.SID equals d2.SID
                                             join d3 in RegulatoryData
                                             on d1.SID equals d3.PFId
                                             join d4 in PricingDetailsData
                                             on d1.SID equals d4.SID
                                            select new BondDto {
                                                id = d1.SID,
                                                SecurityName = d1.SecurityName,
                                                SecurityDescription = d1.SecurityDescription,
                                                CouponRate = d2.CouponRate,
                                                IsCallable = d2.IsCallable,
                                                MaturityDate = d2.MaturityDate,
                                                PenultimateCouponDate = d2.PenultimateCouponDate,
                                                PFCreditRating = d3.PFCreditRating,
                                                AskPrice = d4.AskPrice,
                                                BidPrice = d4.BidPrice
                                            };

            
            
            return query;
                
        }


    }
}