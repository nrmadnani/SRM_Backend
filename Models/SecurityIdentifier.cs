using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("SecurityIdentifier")]
public partial class SecurityIdentifier
{
    [Key]
    public int SIdentifier { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string? CUSIP { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? ISIN { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? SEDOL { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? BloombergTicker { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? BloombergUniqueId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BloombergUniqueName { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? BloombergGlobalId { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? BloombergTickerAndExchange { get; set; }
}
