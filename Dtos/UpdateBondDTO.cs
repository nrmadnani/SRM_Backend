namespace SRMWebApiApp.Dtos {
    public class UpdateBondDTO {
        public int SID { get; set; }
        public string? SecurityDescription { get; set; }
        public double? CouponRate {get; set;} 
        public bool? isCallable { get; set; }
        public string? MaturityDate { get; set; }
        public string? PFCreditRating { get; set; }
        public decimal? AskPrice { get; set; }
        public double? BidPrice {get; set; }
    }
}