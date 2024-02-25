
using Microsoft.EntityFrameworkCore;

namespace SRMWebApiApp.Models {
    
    [Keyless]
    public class ActiveFlag {
        public bool? IsActive { get; set; }
    }
}