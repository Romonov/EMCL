using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
