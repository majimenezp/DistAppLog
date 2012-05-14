using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALServer.DAL.Entities
{
    public class Application
    {
        public virtual long Id { get; set; }
        public virtual string AppName { get; set; }
    }
}
