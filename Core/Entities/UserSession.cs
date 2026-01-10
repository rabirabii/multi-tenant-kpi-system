using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class UserSession
    {
        public Guid UserId { get; set; }      
        public string KeycloakId { get; set; } 
        public string Username { get; set; }   
        public string Name { get; set; }       
        public string Email { get; set; }      
        public Guid TenantId { get; set; }     
        public string Role { get; set; }       
        public List<string> Permissions { get; set; }
    }
}
