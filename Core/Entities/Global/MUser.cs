using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class MUser : BaseAuditableEntity
    {
        [Key]
        public Guid UserId { get; set; }
        
        public string KeyCloakId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime? UpdatedAt { get; set; }
        public ICollection<TrUserTenant> UserTenants { get; set; } = new List<TrUserTenant>();


    }
}
