using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings
{
    public class KeycloakSettings
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
    }
}
