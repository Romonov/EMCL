using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCL
{
    class Java
    {
        public static string Find()
        {
            try
            {
                Main.JavaPaths = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment").GetSubKeyNames();
                Main.JavaVersions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\").GetSubKeyNames();
                Main.JavaPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + Main.JavaPaths[0]).GetValue("JavaHome") + "\\bin\\javaw.exe";
                for(int i = 0;i < Main.JavaVersions.Length; i++)
                {
                    if(Main.JavaVersions[i] == "Java Runtime Environment")
                    {
                        Main.JavaVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\" + Main.JavaVersions[i]).GetValue("CurrentVersion").ToString();
                    }
                }
            }
            catch
            {
                return "Error";
            }
            return Main.JavaPath;
        }
    }
}
