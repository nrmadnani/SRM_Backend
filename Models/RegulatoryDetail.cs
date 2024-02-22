using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

public partial class RegulatoryDetail
{
    [Key]
    public int PFId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFAssetClass { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFCountry { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PFCreditRating { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PFCurrency { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PFInstrument { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PFLiquidityProfile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PFMaturity { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFNAICSCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFRegion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFSector { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PFSubAssetClass { get; set; }
}
