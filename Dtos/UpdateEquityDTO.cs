namespace SRMWebApiApp.Dtos {
    public class UpdateEquityDTO {
        public int SID { get; set; }
        public string? SecurityDescription { get; set; }
        public string? PricingCurrency {get; set;} 
        public double? SharesOutStanding { get; set; }
        public double? OpenPrice { get; set; }
        public double? ClosePrice { get; set; }
        public string? DividendDeclaredDate { get; set; }
        public string? PFCreditRating {get; set; }
    }
}