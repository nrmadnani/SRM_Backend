using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("SecurityDetailsEquity")]
public partial class SecurityDetailsEquity
{
    [Key]
    public int SID { get; set; }

    public bool? isADR { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ADRUnderlyingTicker { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ADRUnderlyingCurrency { get; set; }

    public double? SharesPerADR { get; set; }

    public DateOnly? IPODate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PriceCurrency { get; set; }

    public int? SettleDays { get; set; }

    public double? SharesOutstanding { get; set; }

    public double? VotingRightsPerShare { get; set; }
}
