using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Global
{
    public class MGlobalMenu
    {
        [Key]
        public Guid GlobalMenuId { get; set; }

        public string MenuKey { get; set; } = string.Empty;

        public string DefaultLabel { get; set; } = string.Empty;

        public string DefaultRoute { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;
    }
}
