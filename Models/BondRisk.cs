using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("BondRisk")]
public partial class BondRisk
{
    [Key]
    public int SID { get; set; }

    public double? MacaulayDuration { get; set; }

    public double? Volatility30Day { get; set; }

    public double? Volatility90Day { get; set; }

    public double? Convexity { get; set; }

    public double? AverageVolume30Day { get; set; }

    public double? Spread { get; set; }
}
