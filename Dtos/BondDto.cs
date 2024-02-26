namespace SRMWebApiApp.Dtos {
    public class BondDto{
        public int id { get; set; }
        public string? SecurityName { get; set; }
        public string? SecurityDescription { get; set; }
        public bool? IsCallable { get; set; }
        public DateOnly? MaturityDate { get; set; }
        public DateOnly? PenultimateCouponDate { get; set; }
        public double? CouponRate { get; set; }
        public string? PFCreditRating { get; set; }
        public decimal? AskPrice { get; set; }
        public double? BidPrice { get; set; }
    }
}