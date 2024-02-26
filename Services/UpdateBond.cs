using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using SRMWebApiApp.Data;
using SRMWebApiApp.Dtos;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Services {
    public class UpdateBond : IUpdateBondService{
        private readonly IVP_3308_v3Context _context;
        public UpdateBond(IVP_3308_v3Context context) {
            _context = context;
        }

        public async Task<UpdateBondDTO> UpdateBondData(UpdateBondDTO dto) {
            var SecuritySummariesData = _context.SecuritySummaries.FirstOrDefault(x => x.SID == dto.SID);
            var SecurityDetailsBondData = _context.SecurityDetailsBonds.FirstOrDefault(x => x.SID == dto.SID);
            Console.WriteLine(SecurityDetailsBondData.ToString());
            var RegulatoryDetailData = _context.RegulatoryDetails.FirstOrDefault(x => x.PFId == dto.SID);
            var PricingDetailsData = _context.PricingDetails.FirstOrDefault(x => x.SID == dto.SID);

            SecuritySummariesData.SecurityDescription = dto.SecurityDescription;
            Console.WriteLine("HELLO");
            SecurityDetailsBondData.CouponRate = dto.CouponRate;
            Console.Write("DONEEEEE life");
            SecurityDetailsBondData.IsCallable = Boolean.Parse(dto.isCallable);
            SecurityDetailsBondData.PenultimateCouponDate = DateOnly.Parse(dto.PenultimateCouponDate);
            // SecurityDetailsBondData.MaturityDate = dto.MaturityDate;
            SecurityDetailsBondData.MaturityDate = DateOnly.FromDateTime(DateTime.Now);
            Console.Write("DONEEEEE adasd");
            RegulatoryDetailData.PFCreditRating = dto.PFCreditRating;
            Console.Write("DONEEEEE asd");
            PricingDetailsData.AskPrice = dto.AskPrice;
            Console.Write("DONEEEEE 2 ");
            PricingDetailsData.BidPrice = dto.BidPrice;
            await _context.SaveChangesAsync();
            return dto;
        }
    }
}