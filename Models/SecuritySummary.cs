using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("SecuritySummary")]
public partial class SecuritySummary
{
    [Key]
    public int SID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? SecurityType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SecurityName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string SecurityDescription { get; set; } = null!;

    public bool? HasPosition { get; set; }

    public bool? IsActive { get; set; }
}
