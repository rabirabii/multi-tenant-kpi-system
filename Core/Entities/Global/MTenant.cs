using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class MTenant
    {
        [Key]
        public Guid TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string SchemaName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
