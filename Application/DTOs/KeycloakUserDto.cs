using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record class KeycloakUserDto
    {
        public string KeycloakId { get; set; } 
        public string Username { get; set; } = string.Empty;  
        public string Email { get; set; } = string.Empty;      
        public string Name { get; set; } = string.Empty;
    }
}
