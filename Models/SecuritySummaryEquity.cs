using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Keyless]
[Table("SecuritySummaryEquity")]
public partial class SecuritySummaryEquity
{
    public int? SecId { get; set; }

    public int? RoundLotSize { get; set; }

    [ForeignKey("SecId")]
    public virtual SecuritySummary? Sec { get; set; }
}
