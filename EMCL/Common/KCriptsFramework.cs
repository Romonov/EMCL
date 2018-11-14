using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EMCL.Common
{
    class KCriptsFramework
    {
        #region KCript
        // KCript
        [DllImport("KCript.dll", EntryPoint = "Initialize")]
        public static extern int Initialize();

        [DllImport("KCript.dll", EntryPoint = "Enable")]
        public static extern int Enable();

        [DllImport("KCript.dll", EntryPoint = "Trigger")]
        public static extern string Trigger(
            string Name,
            string Msg
        );

        [DllImport("KCript.dll", EntryPoint = "getPluginList")]
        public static extern string getPluginList();

        [DllImport("KCript.dll", EntryPoint = "getVersion")]
        public static extern string getVersion(
            string Name
        );

        [DllImport("KCript.dll", EntryPoint = "getPluginName")]
        public static extern string getPluginName(
            int ID
        );

        [DllImport("KCript.dll", EntryPoint = "getPluginNum")]
        public static extern string getPluginNum(
            string Name
        );

        [DllImport("KCript.dll", EntryPoint = "freePlugin")]
        public static extern int freePlugin(
            string Name
        );

        #endregion
    }
}
