﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Massive;
namespace DistALServer.DAL.SQLite
{
    public class Applications: DynamicModel
    {
        public Applications()
            : base("LogDatabase")
        {
            PrimaryKeyField = "Id";
        }
    }
}
