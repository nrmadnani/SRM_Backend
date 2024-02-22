using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models;

[Table("CallSchedule")]
public partial class CallSchedule
{
    [Key]
    public int SID { get; set; }

    public DateOnly? CallDate { get; set; }

    public double? CallPrice { get; set; }
}
