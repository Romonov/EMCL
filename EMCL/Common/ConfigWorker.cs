using RUL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCL.Common
{
    class ConfigWorker
    {
        public static void Load(string Path)
        {
            try
            {
                INI.Read(Path, "", "");
            }
            catch(Exception e)
            {

            }
        }

        public static void Save(string Path)
        {

        }
    }
}
