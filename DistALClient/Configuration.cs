﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DistALClient
{
    public class Configuration
    {
        public System.Net.IPAddress ServerIP { get; set; }
        public int Port { get; set; }
        public string Identity { get; set; }
        public System.Net.IPAddress StringToIP(string ipString)
        {
            System.Net.IPAddress dir;
            if (!System.Net.IPAddress.TryParse(ipString, out dir))
            {
                dir = System.Net.IPAddress.Loopback;
            }
            return dir;
        }
    }
}
