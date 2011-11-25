using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;

namespace DistALServer
{
    public class NinjectFactory
    {
        public static IKernel kernel = null;
        public static IKernel GetNinjectKernel()
        {
            if (kernel == null)
            {
                INinjectModule module = new DalModule();
                kernel = new StandardKernel(module);
            }
            return kernel;
        }
    }
}
