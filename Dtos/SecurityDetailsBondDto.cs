namespace SRMWebApiApp.Dtos {
    public class SecurityDetailsBondDto{

        public int SID { get; set; }
        public bool? IsCallable { get; set; }
        public DateOnly? MaturityDate { get; set; }
        public DateOnly? PenultimateCouponDate { get; set; }
        public double? CouponRate { get; set; }

    }
}