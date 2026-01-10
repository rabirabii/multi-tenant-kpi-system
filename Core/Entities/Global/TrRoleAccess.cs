using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class TrRoleAccess : BaseAuditableEntity
    {
        [Key]
        public Guid RoleAccessId { get; set; }
        public Guid RoleId { get; set; }
        public Guid TenantMenuId { get; set; }

        public bool CanCreate { get; set; } = false;
        public bool CanRead { get; set; } = false;

        public bool CanUpdate { get; set; } = false;

        public bool CanDelete { get; set; } = false;

        public MRole Role { get; set; } = null!;
        public TrTenantMenu TenantMenu { get; set; } = null!;
    }
}
