using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EMCL
{
    class Launcher
    {
        /// <summary>
        /// 启动主方法
        /// </summary>
        /// <param name="MaxMem">最大内存</param>
        /// <param name="JavaPath">Javaw.exe路径</param>
        /// <param name="UserName">用户名</param>
        /// <param name="VerName">版本名</param>
        /// <param name="returnvalue">返回值的指定。0为不返回，直接执行；1为返回库的完整字符串；2为返回完整的启动命令。默认为0。</param>
        public static string Launch(string MaxMem, string JavaPath, string UserName, string VerName, int returnvalue = 0)
        {
            string rtxt = "";//声明
            int tmp = 0;//声明
            rtxt = File.ReadAllText(Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + ".json").Replace(" ", "").Replace("\n", "");//读取内容
            tmp = rtxt.IndexOf("mainClass") + "mainClass".Length + 3;//取名称起始位置并赋值到tmp
            String mainClass = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//截取值
            tmp = rtxt.IndexOf("minecraftArguments") + "minecraftArguments".Length + 3;//json读取部分，同上
            String minecraftArguments = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp).Replace("${", " ${").Replace("}", "} ").Replace("cpw", " cpw");//json读取部分，同上
            tmp = rtxt.IndexOf("libraries") + "libraries".Length + 3;//json读取部分，同上
            String libraries = rtxt.Substring(tmp + 1, rtxt.LastIndexOf("]") - tmp);//json读取部分，同上
            String natives = Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + "-natives";//设置natives路径
            if (!Directory.Exists(natives))//目录不存在
            {
                Directory.CreateDirectory(natives);//创建
            }
            String[] libs = libraries.Replace("${arch}", "32").Replace("},{", "^").Split("^".ToCharArray());//将},{替换为^然后以^进行分割，顺便决定32位系统
            String libp = "";//声明libp
            for (int i = 0; i < libs.Length; i++)//为数组内容循环，也可以考虑foreach
            {
                if (libs[i].IndexOf("name") == -1)//如果没有name值
                {
                    continue;//跳过
                }
                String tml = libs[i];//声明临时字符串变量tml
                if (tml.IndexOf("{") != -1)//如果含有子json对象，则去掉子项
                {
                    do//循环
                    {
                        try//捕获错误
                        {
                            tml = tml.Replace(tml.Substring(tml.LastIndexOf("{") - 1, tml.IndexOf("}", tml.LastIndexOf("{")) - tml.LastIndexOf("{") + 1), "");//去掉子项
                        }
                        catch (ArgumentOutOfRangeException ex)//如果是干扰的
                        {
                            tml = tml + "," + libs[i + 1];//追加
                            tml = tml.Replace(tml.Substring(tml.LastIndexOf("{") - 1, tml.IndexOf("}", tml.LastIndexOf("{")) - tml.LastIndexOf("{") + 1), "");//再去掉子项
                        }
                    } while (tml.IndexOf("{") != -1);//有子项则仍然循环
                    tml = tml.Replace("{", "").Replace("}", "");//去掉干扰的{}
                }
                tmp = tml.IndexOf("name") + "name".Length + 3;//json读取部分，同上
                String libn = tml.Substring(tmp, tml.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                if (libn.IndexOf(":") == -1)//如果name的值不合法
                {
                    continue;//跳过
                }
                String[] tlib = new String[] { libn.Substring(0, libn.IndexOf(":")).Replace(":", ""), libn.Substring(libn.IndexOf(":") + 1, libn.IndexOf(":", libn.IndexOf(":") + 1) - libn.IndexOf(":")).Replace(":", ""), libn.Substring(libn.IndexOf(":", libn.IndexOf(":") + 1)).Replace(":", "") };//将读取的name值转成路径
                String tpath = Application.StartupPath + "\\.minecraft\\libraries\\" + tlib[0].Replace(".", "\\") + "\\" + tlib[1] + "\\" + tlib[2] + "\\" + tlib[1] + "-" + tlib[2] + ".jar";//同上
                if (libs[i].IndexOf("natives") != -1 && libs[i].IndexOf("windows") != -1)//如果有natives指定
                {
                    tmp = libs[i].IndexOf("windows") + "windows".Length + 3;//json读取部分，同上
                    tpath = tpath.Replace(".jar", "") + "-" + libs[i].Substring(tmp, libs[i].IndexOf("\"", tmp) - tmp) + ".jar";//json读取部分，同上
                }
                if (File.Exists(tpath))//检查文件是否存在
                {
                    libp = libp + tpath + ";";//存在就加录
                }
                if (libs[i].IndexOf("extract") != -1)//如果要提取natives
                {
                    //decompress(tpath, natives);//提取
                }
            }
            if (returnvalue == 1)//如果要返回库路径
            {
                return libp;//返回并退出方法
            }
            if (rtxt.IndexOf("inheritsFrom\":") != -1)//如果是继承的json数据
            {
                tmp = rtxt.IndexOf("inheritsFrom\":") + "inheritsFrom".Length + 3;//json读取部分，同上
                String newVerName = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                libp += Launch("", "", "", newVerName, 1);//读入父json数据生成的库，解压父json数据要求的natives
                tmp = rtxt.IndexOf("jar\":") + "jar".Length + 3;//json读取部分，同上
                VerName = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                natives = Application.StartupPath + "\\.minecraft\\versions\\" + newVerName + "\\" + newVerName + "-natives";//重新指定natives文件夹
            }
            libp = libp + Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + ".jar";//整合字符串
            String assets = Application.StartupPath + "\\.minecraft\\assets";//设置资源文件夹路径
            tmp = rtxt.IndexOf("assets\":") + "assets".Length + 3;//json读取部分，同上
            String assetIndex = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
            if (!File.Exists(assets + "\\indexes\\" + assetIndex + ".json"))//如果不存在
            {
                if (File.Exists(assets + "\\virtual\\legacy\\indexes\\" + assetIndex + ".json"))//如果virtual目录有效
                {
                    assets = assets + "\\virtual\\legacy";//修改资源文件夹
                }
            }
            String gameDir = Application.StartupPath + "\\.minecraft";//设置游戏路径
            if (Application.StartupPath.IndexOf(" ") != -1)//如果路径有空格
            {
                natives = "\"" + natives + "\"";//加上引号
                libp = "\"" + libp + "\"";//加上引号
                assets = "\"" + assets + "\"";//加上引号
                gameDir = "\"" + gameDir + "\"";//加上引号
            }
            minecraftArguments = minecraftArguments.Replace("${auth_player_name}", UserName).Replace("${version_name}", "JuicyLauncher_1.0").Replace("${game_directory}", gameDir).Replace("${game_assets}", assets).Replace("${assets_root}", assets).Replace("${assets_index_name}", assetIndex).Replace("${user_properties}", "{}").Replace("${user_type}", "Legacy");//读取额外参数
                                                                                                                                                                                                                                                                                                                                                                         //启动参数拼接
            String RunComm = "";//声明RunComm
            RunComm = "-Xmx" + MaxMem + "m -Djava.library.path=" + natives + " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -cp " + libp + " " + mainClass + " " + minecraftArguments;//拼接参数
            if (returnvalue == 2)//如果要返回完整的启动命令
            {
                return RunComm;//返回并退出
            }
            Process mjp = new Process();//运行部分
            MessageBox.Show(RunComm);
            ProcessStartInfo psi = new ProcessStartInfo(JavaPath, RunComm);//运行部分
            psi.UseShellExecute = false;//运行部分
            mjp.StartInfo = psi;//运行部分
            mjp.Start();//运行部分
            Application.Exit();//退出
            return null;//最终返回空值
        }

        /// <summary>
        /// 启动主方法
        /// </summary>
        /// <param name="loginType">登录类型</param>
        /// <param name="Version">版本</param>
        /// <param name="Memory">内存大小</param>
        /// <param name="Username">用户名</param>
        /// <param name="JVMAddP">JVM附加参数</param>
        /// <param name="MCAddP">MC附加参数</param>
        /// <param name="DirServer">直入服务器</param>
        /// <param name="Width">窗口高度</param>
        /// <param name="Height">窗口宽度</param>
        /// <param name="JavaPath">Java路径</param>
        /// <param name="UsePubPath">使用公共路径</param>
        /// <param name="DisDefJVMP">禁用默认JVM参数</param>
        /// <param name="VerIsolate">强制版本隔离</param>
        /// <param name="LnhFullSrn">全屏启动</param>
        public static void Launch(LoginType loginType, string Version, int Memory, string Username, string JVMAddP, string MCAddP, string DirServer, int Width, int Height, string JavaPath, bool UsePubPath, bool DisDefJVMP, bool VerIsolate, bool LnhFullSrn)
        {
            bool IsInheritsed = false;

            string RunCommand = "";

            string JsonPathRaw = $@"{Main.GamePath}\versions\{Version}\{Version}.json";
            string JsonRaw = File.ReadAllText(JsonPathRaw);
            string JsonPathInherits = "";
            string JsonInherits = "";

            string GameDir = $@"{Main.GamePath}";

            string NativesPath = $@"{Main.GamePath}\versions\{Version}\{Version}-natives";

            ArrayList librariesPath = new ArrayList();

            MainJson JsonMain = JsonConvert.DeserializeObject<MainJson>(JsonRaw);

            string AssetsDir = $@"{Main.GamePath}\assets";
            string AssetsIndex = JsonMain.assetIndex.id;
            
            ArrayList LibraryPaths = new ArrayList();
            
            if (!Directory.Exists(NativesPath))
            {
                Directory.CreateDirectory(NativesPath);
            }

            string MainClass = JsonMain.mainClass;

            RunCommand += $"-Xmx{Memory}M -Xmn128M";

            if (!DisDefJVMP)
            {
                RunCommand += $" -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow";
            }

            RunCommand += $" -Djava.library.path={NativesPath}";

            string cp = "";
            for(int i = 0; i < JsonMain.libraries.Length; i++)
            {
                string[] tmp = JsonMain.libraries[i].name.Split(':');
                string[] tmp2 = tmp[0].Split('.');
                string path = $@"{Main.GamePath}\libraries";
                for(int j = 0; j < tmp2.Length; j++)
                {
                    path += $@"\{tmp2[j]}";
;               }
                path += $@"\{tmp[1]}";
                path += $@"\{tmp[2]}";
                path += $@"\{tmp[1]}-{tmp[2]}.jar;";
                cp += path;
            }

            if (JsonMain.inheritsFrom != null)
            {
                IsInheritsed = true;

                JsonPathInherits = $@"{Main.GamePath}\versions\{JsonMain.inheritsFrom}\{JsonMain.inheritsFrom}.json";
                JsonInherits = File.ReadAllText(JsonPathInherits);
                MainJson JsonInheritsMain = JsonConvert.DeserializeObject<MainJson>(JsonInherits);

                for (int i = 0; i < JsonInheritsMain.libraries.Length; i++)
                {
                    string[] tmp = JsonInheritsMain.libraries[i].name.Split(':');
                    string[] tmp2 = tmp[0].Split('.');
                    string path = $@"{Main.GamePath}\libraries";
                    for (int j = 0; j < tmp2.Length; j++)
                    {
                        path += $@"\{tmp2[j]}";
                        ;
                    }
                    path += $@"\{tmp[1]}";
                    path += $@"\{tmp[2]}";
                    path += $@"\{tmp[1]}-{tmp[2]}.jar;";
                    cp += path;
                }

            }

            RunCommand += $" -cp {cp}";
            RunCommand += $@"{Main.GamePath}\versions\{JsonMain.id}\{JsonMain.id}.jar";

            string Arguments = JsonMain.minecraftArguments
                .Replace("${auth_player_name}", $"{Username}")
                .Replace("${version_name}", $"\"EMCL-{Main.Version}\"")
                .Replace("${version_type}", $"\"EMCL-{Main.Version}\"")
                .Replace("${game_directory}", $"{Main.GamePath}")
                .Replace("${assets_root}", $"{AssetsDir}")
                .Replace("${assets_index_name}", $"{AssetsIndex}")
                .Replace("${user_type}", $"Legacy")
                .Replace("${auth_uuid}", $"88888888888888888888888888888888")
                .Replace("${auth_access_token}", $"88888888888888888888888888888888");

            RunCommand += $" {MainClass} {Arguments}";

            MessageBox.Show(RunCommand);

            Process mjp = new Process();//运行部分
            ProcessStartInfo psi = new ProcessStartInfo(JavaPath, RunCommand);//运行部分
            psi.UseShellExecute = false;//运行部分
            mjp.StartInfo = psi;//运行部分
            mjp.Start();//运行部分

            #region tmp
            /*
            string rtxt = "";//声明
            int tmp = 0;//声明
            rtxt = File.ReadAllText(Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + ".json").Replace(" ", "").Replace("\n", "");//读取内容
            tmp = rtxt.IndexOf("mainClass") + "mainClass".Length + 3;//取名称起始位置并赋值到tmp
            String mainClass = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//截取值
            tmp = rtxt.IndexOf("minecraftArguments") + "minecraftArguments".Length + 3;//json读取部分，同上
            String minecraftArguments = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp).Replace("${", " ${").Replace("}", "} ").Replace("cpw", " cpw");//json读取部分，同上
            tmp = rtxt.IndexOf("libraries") + "libraries".Length + 3;//json读取部分，同上
            String libraries = rtxt.Substring(tmp + 1, rtxt.LastIndexOf("]") - tmp);//json读取部分，同上
            String natives = Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + "-natives";//设置natives路径
            
            String[] libs = libraries.Replace("${arch}", "32").Replace("},{", "^").Split("^".ToCharArray());//将},{替换为^然后以^进行分割，顺便决定32位系统
            String libp = "";//声明libp
            for (int i = 0; i < libs.Length; i++)//为数组内容循环，也可以考虑foreach
            {
                if (libs[i].IndexOf("name") == -1)//如果没有name值
                {
                    continue;//跳过
                }
                String tml = libs[i];//声明临时字符串变量tml
                if (tml.IndexOf("{") != -1)//如果含有子json对象，则去掉子项
                {
                    do//循环
                    {
                        try//捕获错误
                        {
                            tml = tml.Replace(tml.Substring(tml.LastIndexOf("{") - 1, tml.IndexOf("}", tml.LastIndexOf("{")) - tml.LastIndexOf("{") + 1), "");//去掉子项
                        }
                        catch (ArgumentOutOfRangeException ex)//如果是干扰的
                        {
                            tml = tml + "," + libs[i + 1];//追加
                            tml = tml.Replace(tml.Substring(tml.LastIndexOf("{") - 1, tml.IndexOf("}", tml.LastIndexOf("{")) - tml.LastIndexOf("{") + 1), "");//再去掉子项
                        }
                    } while (tml.IndexOf("{") != -1);//有子项则仍然循环
                    tml = tml.Replace("{", "").Replace("}", "");//去掉干扰的{}
                }
                tmp = tml.IndexOf("name") + "name".Length + 3;//json读取部分，同上
                String libn = tml.Substring(tmp, tml.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                if (libn.IndexOf(":") == -1)//如果name的值不合法
                {
                    continue;//跳过
                }
                String[] tlib = new String[] { libn.Substring(0, libn.IndexOf(":")).Replace(":", ""), libn.Substring(libn.IndexOf(":") + 1, libn.IndexOf(":", libn.IndexOf(":") + 1) - libn.IndexOf(":")).Replace(":", ""), libn.Substring(libn.IndexOf(":", libn.IndexOf(":") + 1)).Replace(":", "") };//将读取的name值转成路径
                String tpath = Application.StartupPath + "\\.minecraft\\libraries\\" + tlib[0].Replace(".", "\\") + "\\" + tlib[1] + "\\" + tlib[2] + "\\" + tlib[1] + "-" + tlib[2] + ".jar";//同上
                if (libs[i].IndexOf("natives") != -1 && libs[i].IndexOf("windows") != -1)//如果有natives指定
                {
                    tmp = libs[i].IndexOf("windows") + "windows".Length + 3;//json读取部分，同上
                    tpath = tpath.Replace(".jar", "") + "-" + libs[i].Substring(tmp, libs[i].IndexOf("\"", tmp) - tmp) + ".jar";//json读取部分，同上
                }
                if (File.Exists(tpath))//检查文件是否存在
                {
                    libp = libp + tpath + ";";//存在就加录
                }
                if (libs[i].IndexOf("extract") != -1)//如果要提取natives
                {
                    decompress(tpath, natives);//提取
                }
            }
            if (returnvalue == 1)//如果要返回库路径
            {
                return libp;//返回并退出方法
            }
            if (rtxt.IndexOf("inheritsFrom\":") != -1)//如果是继承的json数据
            {
                tmp = rtxt.IndexOf("inheritsFrom\":") + "inheritsFrom".Length + 3;//json读取部分，同上
                String newVerName = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                libp += Launch("", "", "", newVerName, 1);//读入父json数据生成的库，解压父json数据要求的natives
                tmp = rtxt.IndexOf("jar\":") + "jar".Length + 3;//json读取部分，同上
                VerName = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
                natives = Application.StartupPath + "\\.minecraft\\versions\\" + newVerName + "\\" + newVerName + "-natives";//重新指定natives文件夹
            }
            libp = libp + Application.StartupPath + "\\.minecraft\\versions\\" + VerName + "\\" + VerName + ".jar";//整合字符串
            String assets = Application.StartupPath + "\\.minecraft\\assets";//设置资源文件夹路径
            tmp = rtxt.IndexOf("assets\":") + "assets".Length + 3;//json读取部分，同上
            String assetIndex = rtxt.Substring(tmp, rtxt.IndexOf("\"", tmp) - tmp);//json读取部分，同上
            if (!File.Exists(assets + "\\indexes\\" + assetIndex + ".json"))//如果不存在
            {
                if (File.Exists(assets + "\\virtual\\legacy\\indexes\\" + assetIndex + ".json"))//如果virtual目录有效
                {
                    assets = assets + "\\virtual\\legacy";//修改资源文件夹
                }
            }
            String gameDir = Application.StartupPath + "\\.minecraft";//设置游戏路径
            if (Application.StartupPath.IndexOf(" ") != -1)//如果路径有空格
            {
                natives = "\"" + natives + "\"";//加上引号
                libp = "\"" + libp + "\"";//加上引号
                assets = "\"" + assets + "\"";//加上引号
                gameDir = "\"" + gameDir + "\"";//加上引号
            }
            minecraftArguments = minecraftArguments.Replace("${auth_player_name}", UserName).Replace("${version_name}", "JuicyLauncher_1.0").Replace("${game_directory}", gameDir).Replace("${game_assets}", assets).Replace("${assets_root}", assets).Replace("${assets_index_name}", assetIndex).Replace("${user_properties}", "{}").Replace("${user_type}", "Legacy");//读取额外参数
                                                                                                                                                                                                                                                                                                                                                                         //启动参数拼接
            String RunComm = "";//声明RunComm
            RunComm = "-Xmx" + MaxMem + "m -Djava.library.path=" + natives + " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -cp " + libp + " " + mainClass + " " + minecraftArguments;//拼接参数
            if (returnvalue == 2)//如果要返回完整的启动命令
            {
                return RunComm;//返回并退出
            }
            Process mjp = new Process();//运行部分
            ProcessStartInfo psi = new ProcessStartInfo(JavaPath, RunComm);//运行部分
            psi.UseShellExecute = false;//运行部分
            mjp.StartInfo = psi;//运行部分
            mjp.Start();//运行部分
            Application.Exit();//退出
            return null;//最终返回空值
            */
            #endregion
        }

        public static void decompress(String inputFileName, String outputDirName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();//释放7z.exe和7z.dll部分
            Stream stream = assembly.GetManifestResourceStream("EMCL.7z.exe");//释放7z.exe和7z.dll部分
            byte[] bytes = new byte[stream.Length];//释放7z.exe和7z.dll部分
            stream.Read(bytes, 0, int.Parse(stream.Length.ToString()));//释放7z.exe和7z.dll部分
            File.WriteAllBytes(Application.StartupPath + "\\7z.exe", bytes);//释放7z.exe和7z.dll部分
            assembly = Assembly.GetExecutingAssembly();//释放7z.exe和7z.dll部分
            stream = assembly.GetManifestResourceStream("EMCL.7z.dll");//释放7z.exe和7z.dll部分
            bytes = new byte[stream.Length];//释放7z.exe和7z.dll部分
            stream.Read(bytes, 0, int.Parse(stream.Length.ToString()));//释放7z.exe和7z.dll部分
            File.WriteAllBytes(Application.StartupPath + "\\7z.dll", bytes); //释放7z.exe和7z.dll部分
            Process sz = new Process();//运行7z.exe解压部分
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\7z.exe", "x \"" + inputFileName + "\" -o\"" + outputDirName + "\" -y");//运行7z.exe解压部分
            psi.UseShellExecute = false;//运行7z.exe解压部分
            sz.StartInfo = psi;//运行7z.exe解压部分
            sz.Start();//运行7z.exe解压部分
            sz.WaitForExit();//等待退出
            File.Delete(Application.StartupPath + "\\7z.exe");//删除7z.exe
            File.Delete(Application.StartupPath + "\\7z.dll");//删除7z.dll
        }

        private static string GetLaunchType(string Version)
        {
            // Todo
            return "";
        }
    }

    /// <summary>
    /// 登录类型
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 离线登录
        /// </summary>
        Offline,
        /// <summary>
        /// 正版登录
        /// </summary>
        Yggdrasil,
        /// <summary>
        /// Authlib-Injector
        /// </summary>
        Authlib,
        /// <summary>
        /// Mimir
        /// </summary>
        Mimir
    }

    struct MainJson
    {
        public assetIndex assetIndex;
        public string assets;
        public downloads downloads;
        public string id;
        public libraries[] libraries;
        public logging logging;
        public string mainClass;
        public string minecraftArguments;
        public int minimumLauncherVersion;
        public string releaseTime;
        public string time;
        public string type;
        public string jar;
        public string inheritsFrom;
        public bool hidden;
    }
    struct assetIndex
    {
        public string id;
        public string sha1;
        public int size;
        public string url;
        public int totalSize;
    }
    struct downloads
    {
        public downloadsDownload client;
        public downloadsDownload server;
    }
    struct downloadsDownload
    {
        public string sha1;
        public int size;
        public string url;
    }
    struct libraries
    {
        public librariesExtract extract;
        public string name;
        public string url;
        public librariesNatives natives;
        public librariesRules[] rules;
        public librariesDownloads downloads;
    }
    struct librariesDownloads
    {
        public librariesDownloadsClassifiers classifiers;
        public librariesDownloadsArtifact artifact;
    }
    struct librariesDownloadsArtifact
    {
        public int size;
        public string sha1;
        public string path;
        public string url;
    }
    struct librariesDownloadsClassifiers
    {
        public librariesDownloadsClassifiersTests tests;
    }
    struct librariesDownloadsClassifiersTests
    {
        public int size;
        public string sha1;
        public string path;
        public string url;
    }
    struct librariesRules
    {
        public string action;
        public librariesRulesOs os;
    }
    struct librariesRulesOs
    {
        public string name;
    }
    struct librariesExtract
    {
        public string[] exclude;
    }
    struct librariesNatives
    {
        public string linux;
        public string osx;
        public string windows;
    }
    struct logging
    {
        public loggingClient client;
        public string argument;
        public string type;
    }
    struct loggingClient
    {
        public loggingClientFile file;
    }
    struct loggingClientFile
    {
        public string id;
        public string sha1;
        public int size;
        public string url;
    }
}
