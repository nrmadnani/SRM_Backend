using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("EquityRisk")]
public partial class EquityRisk
{
    [Key]
    public int SID { get; set; }

    public double? AverageVolume20Day { get; set; }

    public double? Beta { get; set; }

    public double? ShortInterest { get; set; }

    public double? YTDReturn { get; set; }

    public double? PriceVolatility90Day { get; set; }
}
