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
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;

namespace EMCL
{
    class Launcher
    {
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
            if (VerIsolate)
            {
                GameDir = $@"{Main.GamePath}\versions\{Version}";
            }

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
            if (Directory.GetFiles(NativesPath).Length == 0)
            {
                try
                {
                    DecompressNatives(JsonMain, NativesPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "EMCL 启动错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            string MainClass = JsonMain.mainClass;

            RunCommand += $"-Xmx{Memory}M -Xmn128M";

            if (!DisDefJVMP)
            {
                RunCommand += $" -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow";
            }

            RunCommand += $" -Djava.library.path={NativesPath}";

            string cp = GetCP(JsonMain);

            if (JsonMain.inheritsFrom != null)
            {
                IsInheritsed = true;

                JsonPathInherits = $@"{Main.GamePath}\versions\{JsonMain.inheritsFrom}\{JsonMain.inheritsFrom}.json";
                JsonInherits = File.ReadAllText(JsonPathInherits);
                MainJson JsonInheritsMain = JsonConvert.DeserializeObject<MainJson>(JsonInherits);

                cp += GetCP(JsonInheritsMain);
            }

            RunCommand += $" -cp {cp}";
            RunCommand += $@"{Main.GamePath}\versions\{JsonMain.id}\{JsonMain.id}.jar";

            string Arguments = "";
            if(JsonMain.minecraftArguments != null)
            {
                Arguments = JsonMain.minecraftArguments
                    .Replace("${auth_player_name}", $"{Username}")
                    .Replace("${version_name}", $"\"EMCL-{Main.Version}\"")
                    .Replace("${version_type}", $"\"EMCL-{Main.Version}\"")
                    .Replace("${game_directory}", $"{GameDir}")
                    .Replace("${assets_root}", $"{AssetsDir}")
                    .Replace("${assets_index_name}", $"{AssetsIndex}")
                    .Replace("${user_type}", $"Legacy")
                    .Replace("${auth_uuid}", $"88888888888888888888888888888888")
                    .Replace("${auth_access_token}", $"88888888888888888888888888888888");
            }
            else
            {
                Arguments = $"--username {Username} --version \"EMCL-{Main.Version}\" --gameDir {Main.GamePath} --assetsDir {AssetsDir} --assetIndex {AssetsIndex} --uuid 88888888888888888888888888888888 --accessToken 88888888888888888888888888888888 --userType Legacy --versionType \"EMCL-{Main.Version}\"";
            }

            RunCommand += $" {MainClass} {Arguments}";

            if (LnhFullSrn)
            {
                RunCommand += $" --height {Height} --width {Width} --fullscreen";
            }
            else
            {
                RunCommand += $" --height {Height} --width {Width}";
            }

            //MessageBox.Show(RunCommand);

            Process StartMinecraft = new Process();
            ProcessStartInfo StartMinecraftInfo = new ProcessStartInfo(JavaPath, RunCommand);
            StartMinecraftInfo.UseShellExecute = false;
            StartMinecraft.StartInfo = StartMinecraftInfo;
            StartMinecraft.Start();

        }

        private static string GetCP(MainJson Json)
        {
            string cp = "";
            for (int i = 0; i < Json.libraries.Length; i++)
            {
                string[] tmp = Json.libraries[i].name.Split(':');
                string[] tmp2 = tmp[0].Split('.');
                string path = $@"{Main.GamePath}\libraries";
                for (int j = 0; j < tmp2.Length; j++)
                {
                    path += $@"\{tmp2[j]}";
                }
                path += $@"\{tmp[1]}";
                path += $@"\{tmp[2]}";
                path += $@"\{tmp[1]}-{tmp[2]}.jar;";
                cp += path;
            }
            return cp;
        }

        private static void DecompressNatives(MainJson Json, string path)
        {
            string name = "";
            for (int i = 0; i < Json.libraries.Length; i++)
            {
                if(Json.libraries[i].extract != null)
                {
                    string[] tmp = Json.libraries[i].name.Split(':');
                    string[] tmp2 = tmp[0].Split('.');
                    name = $@"{Main.GamePath}\libraries";
                    for (int j = 0; j < tmp2.Length; j++)
                    {
                        name += $@"\{tmp2[j]}";
                    }
                    name += $@"\{tmp[1]}";
                    name += $@"\{tmp[2]}";
                    name += $@"\{tmp[1]}-{tmp[2]}-natives-windows.jar";

                    (new FastZip()).ExtractZip(name.Replace("${arch}", "32"), path, "");
                }
            }
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
        public librariesExtract? extract;
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
