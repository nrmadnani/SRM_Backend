using SRMWebApiApp.Data;
using SRMWebApiApp.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Web.Http.Results;

namespace SRMWebApiApp.Services {
    public class BondServiceImpl : IBondService
    {
        private readonly IVP_3308_v3Context _context;

        public BondServiceImpl(IVP_3308_v3Context context) {
            _context = context;
        }

        async Task<BondDto?> IBondService.DeleteBond(int id)
        {
            var deletedEntity = await _context.SecuritySummaries.FindAsync(id);
            if (deletedEntity is not null){
                var bondResult = await this.GetBond(id);
                deletedEntity.IsActive = false;
                await _context.SaveChangesAsync();
                return bondResult;
            }
            
            return null;
            

        }

        async Task<BondDto?> GetBond(int id){
            var securitySummariesData =  await _context.SecuritySummaries
                            .Where(s => s.IsActive.Equals(true) && s.SecurityType != null && s.SecurityType.Equals("Bond") && s.SID == id)
                            .Select(s => new SecuritySummaryDto(){
                                SID = s.SID,
                                SecurityName = s.SecurityName,
                                SecurityDescription = s.SecurityDescription
                            })
                            .FirstOrDefaultAsync();

            if(securitySummariesData is not null){
                var bondDetailsData =  await _context.SecurityDetailsBonds
                        .Where(s=> s.SID == id)
                            .Select(s => new SecurityDetailsBondDto(){
                                SID = id,
                                CouponRate = s.CouponRate,
                                IsCallable = s.IsCallable,
                                MaturityDate = s.MaturityDate,
                                PenultimateCouponDate = s.PenultimateCouponDate
                            }).FirstOrDefaultAsync();
                
                var regulatoryData =  await _context.RegulatoryDetails
                            .Where(r => r.PFId == id)
                            .Select(r => new RegulatoryDetailDto(){
                                PFId = id,
                                PFCreditRating = r.PFCreditRating
                            })
                            .FirstOrDefaultAsync();

                var pricingDetailsData =  await _context.PricingDetails
                            .Where(p=> p.SID == id)
                            .Select(p => new PricingDetailsDto(){
                                SID = p.SID,
                                AskPrice = p.AskPrice,
                                BidPrice = p.BidPrice
                            })
                            .FirstOrDefaultAsync();

                if (bondDetailsData != null && regulatoryData != null && pricingDetailsData != null){
                    return new BondDto{
                        SecurityName = securitySummariesData.SecurityName,
                        SecurityDescription = securitySummariesData.SecurityDescription,
                        CouponRate = bondDetailsData.CouponRate,
                        IsCallable = bondDetailsData.IsCallable,
                        MaturityDate = bondDetailsData.MaturityDate,
                        PenultimateCouponDate = bondDetailsData.PenultimateCouponDate,
                        PFCreditRating = regulatoryData.PFCreditRating,
                        AskPrice = pricingDetailsData.AskPrice,
                        BidPrice = pricingDetailsData.BidPrice
                    };
                }
            }
            return null;
        }

        async Task<bool> IBondService.DeleteBondById(int id)
        {
            var deletedSecurity = await _context.SecuritySummaries.FindAsync(id);
            if (deletedSecurity is null){
                return false;
            }
            if (deletedSecurity.SecurityType == "Bond"){
                deletedSecurity.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
           
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

        Task<BondDto?> IBondService.GetBond(int id)
        {
            return this.GetBond(id);
        }
    }
}