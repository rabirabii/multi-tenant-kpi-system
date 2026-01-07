using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class MUser
    {
        [Key]
        public Guid UserId { get; set; }
        
        public Guid KeyCloakId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime? UpdatedAt { get; set; }
        public ICollection<TrUserTenant> UserTenants { get; set; } = new List<TrUserTenant>();


    }
}
