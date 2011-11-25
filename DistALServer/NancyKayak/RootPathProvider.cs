using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using System.IO;

namespace DistALServer.NancyKayak
{
    public class RootPathProvider
    {
        private static readonly string RootPath;
        static RootPathProvider()
        {
            RootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
}
