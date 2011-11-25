using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALServer.DAL.Entities
{
    public class Log
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long AppIdentity { get; set; }
        public string Module { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

    }
}
