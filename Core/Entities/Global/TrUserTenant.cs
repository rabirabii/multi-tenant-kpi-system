using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class TrUserTenant
    {
        [Key]
        public Guid UserTenantId { get; set; }
        
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }

        public Guid RoleId { get; set; }

        public MUser User { get; set; } = null!;
        public MTenant Tenant { get; set; } = null!;
        public MRole Role { get; set; } = null!;

    }
}
