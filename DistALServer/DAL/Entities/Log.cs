using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALServer.DAL.Entities
{
    public class Log
    {
        public virtual long Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual long AppIdentity { get; set; }
        public virtual string App { get; set; }
        public virtual string Module { get; set; }
        public virtual string Level { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
        public Log()
        {
            this.Message = string.Empty;
            this.Level = "INFO";
            this.Module = string.Empty;
            this.App = string.Empty;
            this.Date = DateTime.Now;
        }
    }
}
