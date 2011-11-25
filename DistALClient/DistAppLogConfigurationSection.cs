using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace DistALClient
{
    public class DistAppLogConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("server")]
        public CommunicationElement Communication
        {
            get { return (CommunicationElement)this["server"]; }
            set { this["server"] = value; }
        }
    }

    public class CommunicationElement : ConfigurationElement
    {
        [ConfigurationProperty("tcpport", IsRequired = true, DefaultValue = 5050)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 35000, MinValue = 100)]
        public int TcpPort
        {
            get { return (int)this["tcpport"]; }
            set { this["tcpport"] = value; }
        }

        [ConfigurationProperty("host", IsRequired = true, DefaultValue = "127.0.0.1")]
        public string Server
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("identity", IsRequired = true, DefaultValue = "client01")]
        public string Identity
        {
            get { return (string)this["identity"]; }
            set { this["identity"] = value; }
        }
    }
}
