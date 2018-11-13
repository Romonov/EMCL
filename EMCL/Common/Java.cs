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
            RegistryKey registryKey;
            if (Environment.Is64BitOperatingSystem)
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            try
            {
                Main.JavaPaths = registryKey.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment").GetSubKeyNames();
                Main.JavaVersions = registryKey.OpenSubKey(@"SOFTWARE\JavaSoft\").GetSubKeyNames();
                Main.JavaPath = registryKey.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + Main.JavaPaths[0]).GetValue("JavaHome") + "\\bin\\javaw.exe";

                for (int i = 0; i < Main.JavaVersions.Length; i++)
                {
                    if (Main.JavaVersions[i] == "Java Runtime Environment")
                    {
                        Main.JavaVersion = registryKey.OpenSubKey(@"SOFTWARE\JavaSoft\" + Main.JavaVersions[i]).GetValue("CurrentVersion").ToString();
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
