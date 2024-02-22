using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

public partial class PricingDetail
{
    [Key]
    public int SID { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? AskPrice { get; set; }

    public int? Volume { get; set; }

    public double? HighPrice { get; set; }

    public double? LowPrice { get; set; }

    public double? OpenPrice { get; set; }

    public double? ClosePrice { get; set; }

    public double? BidPrice { get; set; }

    public double? LastPrice { get; set; }

    public double? PERatio { get; set; }
}
