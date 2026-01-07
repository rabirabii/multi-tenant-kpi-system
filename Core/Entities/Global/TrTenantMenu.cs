using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class TrTenantMenu
    {
        [Key]
        public Guid TenantMenuId { get; set; }
        public Guid TenantId { get; set; }

        public Guid GlobalMenuId { get; set; }

        public string CustomLabel { get; set; } = string.Empty;

        public bool IsVisible { get; set; } = true;

        public int SortOrder { get; set; } = 0;

        public MTenant Tenant { get; set; } = null!;
        public MGlobalMenu GlobalMenu { get; set; } = null!;

        public ICollection<TrRoleAccess> RoleAccesses { get; set; } = new List<TrRoleAccess>();
    }
}
