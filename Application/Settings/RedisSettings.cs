using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings
{
    public class RedisSettings
    {
        public bool Enabled { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int Database { get; set; }
    }
}
