using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Keyless]
public partial class ReferenceDatum
{
    public int? SID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? IssueCountry { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Exchange { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Issuer { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? IssueCurrency { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? TradingCurrency { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BloombergIndustrySubGroup { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BloombergIndustryGroup { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BloombergSector { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CountryOfIncorporation { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? RiskCurrency { get; set; }

    [ForeignKey("SID")]
    public virtual SecuritySummary? SIDNavigation { get; set; }
}
