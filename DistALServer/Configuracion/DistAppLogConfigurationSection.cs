using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DistALServer
{
    public class DistAppLogConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("database")]
        public DataBaseElement DataBase
        {
            get { return (DataBaseElement)this["database"]; }
            set { this["database"] = value; }
        }

        [ConfigurationProperty("communication")]
        public CommunicationElement Communication
        {
            get { return (CommunicationElement)this["communication"]; }
            set { this["communication"] = value; }
        }
    }

    public class DataBaseElement : ConfigurationElement
    {
        [ConfigurationProperty("dbprovider", DefaultValue = "SQLLiteDataAccess", IsRequired = true)]
        public string DatabaseProvider
        {
            get
            {
                return (string)this["dbprovider"];
            }
            set
            {
                this["dbprovider"] = value;
            }
        }

        [ConfigurationProperty("sqlConnectionString")]
        public string SQLConnectionString
        {
            get { return (string)this["sqlConnectionString"]; }
            set { this["sqlConnectionString"] = value; }
        }

        [ConfigurationProperty("redisserver", DefaultValue = "127.0.0.1")]
        public string RedisServer
        {
            get { return (string)this["redisserver"]; }
            set { this["redisserver"] = value; }
        }

        [ConfigurationProperty("redisdb", DefaultValue = "0")]
        public int RedisDbNumber
        {
            get { return (int)this["redisdb"]; }
            set { this["redisdb"] = value; }
        }
    }

    public class CommunicationElement : ConfigurationElement
    {
        [ConfigurationProperty("tcpport",IsRequired=true, DefaultValue=5050)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 35000, MinValue = 100)]
        public int TcpPort
        {
            get { return (int)this["tcpport"]; }
            set { this["tcpport"] = value; }
        }

        [ConfigurationProperty("webserverport", DefaultValue = 8080)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 50000, MinValue = 1)]
        public int WebServerPort
        {
            get { return (int)this["webserverport"]; }
            set { this["webserverport"] = value; }
        }
    }
}
