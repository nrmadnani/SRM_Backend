using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("DividendHistory")]
public partial class DividendHistory
{
    [Key]
    public int SID { get; set; }

    public DateOnly? DeclaredDate { get; set; }

    public DateOnly? ExDate { get; set; }

    public DateOnly? RecordDate { get; set; }

    public DateOnly? PayDate { get; set; }

    public double? Amount { get; set; }

    public int? Frequency { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DividendType { get; set; }
}
