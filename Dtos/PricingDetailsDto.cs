namespace SRMWebApiApp.Dtos{
    public class PricingDetailsDto{
            public int SID { get; set; }
            public double? OpenPrice { get; set; }
            public double? ClosePrice { get; set; }
            public decimal? AskPrice { get; set; }
            public double? BidPrice {get; set; }
        }
}