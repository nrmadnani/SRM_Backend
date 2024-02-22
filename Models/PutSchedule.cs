using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("PutSchedule")]
public partial class PutSchedule
{
    [Key]
    public int SID { get; set; }

    public DateOnly? PutDate { get; set; }

    public double? PutPrice { get; set; }
}
