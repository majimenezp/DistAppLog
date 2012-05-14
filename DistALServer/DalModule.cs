using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using DistALServer.DAL;
namespace DistALServer
{
    public class DalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccess>().To<DatabaseDataAccess>().InSingletonScope();
        }
    }
}
