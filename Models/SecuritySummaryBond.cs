using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Keyless]
public partial class SecuritySummaryBond
{
    public int? SecId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? InvestmentType { get; set; }

    public double? TradingFactor { get; set; }

    public double? PricingFactor { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? AssetType { get; set; }

    [ForeignKey("SecId")]
    public virtual SecuritySummary? Sec { get; set; }
}
