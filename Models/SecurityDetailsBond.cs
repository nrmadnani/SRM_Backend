using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

public partial class SecurityDetailsBond
{
    [Key]
    public int SID { get; set; }

    public DateOnly? FirstCouponDate { get; set; }

    public double? CouponCap { get; set; }

    public double? CouponFloor { get; set; }

    public int? CouponFrequency { get; set; }

    public double? CouponRate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CouponType { get; set; }

    public bool? IsCallable { get; set; }

    public bool? IsFixToFloat { get; set; }

    public bool? IsPutable { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? LastResetDate { get; set; }

    public DateOnly? MaturityDate { get; set; }

    public DateOnly? PenultimateCouponDate { get; set; }

    public int? MaximumCallNotificationDays { get; set; }

    public int? MaximumPutNotificationDays { get; set; }

    public int? ResetFrequency { get; set; }
}
