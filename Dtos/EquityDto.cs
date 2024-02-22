namespace SRMWebApiApp.Dtos {
    public class EquityDto{
            public int SID { get; set; }
            public string? SecurityName { get; set; }
            public string? SecurityDescription { get; set; }
            public string? PriceCurrency { get; set; }
            public double? SharesOutstanding { get; set; }
            public double? OpenPrice { get; set; }
            public double? ClosePrice { get; set; }
        }
}