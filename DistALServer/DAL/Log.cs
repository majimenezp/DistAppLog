﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Massive;

namespace DistALServer.DAL
{
    public class Log:DynamicModel
    {
        public Log()
            : base("LogDatabase")
        {
            PrimaryKeyField = "Id";
        }
    }
}