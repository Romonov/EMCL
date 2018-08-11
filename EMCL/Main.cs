using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMCL
{
    public partial class Main : Form
    {
        public const string Version = "0.3.2";
        public const string Author = "Romonov";

        public static string GamePath = Application.StartupPath + "\\.minecraft";
        public static string[] JavaPaths = null;
        public static string[] JavaVersions = null;
        public static string JavaPath = ""; 
        public static string JavaVersion = "";

        public static LoginType loginType = LoginType.Offline;


        public Main()
        {
            InitializeComponent();

            // Read config file


            // Get computer info
            if(ComputerInfo.GetOSVersion() != "Error")
            {
                textOSVersionActive.Text = ComputerInfo.GetOSVersion();
            }
            else
            {
                textOSVersionActive.Text = "获取失败";
            }

            if (ComputerInfo.GetCPU() != "Error")
            {
                textCPUModelActive.Text = ComputerInfo.GetCPU();
            }
            else
            {
                textCPUModelActive.Text = "获取失败";
            }

            if (ComputerInfo.GetGPU() != "Error")
            {
                textGPUModelActive.Text = ComputerInfo.GetGPU();
            }
            else
            {
                textGPUModelActive.Text = "获取失败";
            }

            textTotalMemoryActive.Text = ComputerInfo.GetMemoryTotal().ToString() + " MB";
            textRemainMemoryActive.Text = ComputerInfo.GetMemoryAvailable().ToString() + " MB";
            textRecommendMemoryActive.Text = (int.Parse(ComputerInfo.GetMemoryTotal()) / 4).ToString() + " MB";
            textBoxSetMemory.Text = (int.Parse(ComputerInfo.GetMemoryTotal()) / 4).ToString();

            listBoxLoginType.SelectedIndex = 0;
            textBoxYggdrasilServer.Enabled = false;
            textBoxPassword.Enabled = false;
            buttonLogin.Enabled = false;
            listBoxPlayerRole.Enabled = false;
            textMimirNotice.Visible = false;

            // Find Javas
            FindJava();

            textJavaVersionActive.Text = JavaVersion;

            // Find versions
            Directory.CreateDirectory(GamePath);
            Directory.CreateDirectory(GamePath + "\\versions");

            try
            {
                listVersions.Items.Clear();
                String[] Versions = Directory.GetDirectories(GamePath + "\\versions");
                foreach (String itemVerisons in Versions)
                {
                    if (File.Exists($@"{itemVerisons}\\{itemVerisons.Replace(GamePath + "\\versions\\", "")}.json"))
                    {
                        listVersions.Items.Add(itemVerisons.Replace(GamePath + "\\versions\\", ""));
                    }
                }
                if(listVersions.Items.Count == 0)
                {
                    DialogResult = MessageBox.Show(text: "未找到任何版本，是否进入版本下载？", caption: "EMCL 无法找到游戏", buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Warning);
                    if (DialogResult == DialogResult.OK)
                    {
                        tabMain.SelectedIndex = 2;
                    }
                }
            }
            catch
            {
                DialogResult = MessageBox.Show(text: "未找到任何版本，是否进入版本下载？", caption: "EMCL 无法找到游戏", buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Warning);
                if (DialogResult == DialogResult.OK)
                {
                    tabMain.SelectedIndex = 2;
                }
            }

            // Get Notice 

            // Status
            textStatus.Text = "就绪";
        }

        private void buttonExploreJavaPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog JavaPathView = new OpenFileDialog();
            JavaPathView.Title = "EMCL 请选择Java路径";
            JavaPathView.RestoreDirectory = true;
            JavaPathView.Multiselect = false;
            //JavaPathView.Filter = "Java命令行工具|java.exe|Java可执行文件|javaw.exe";
            JavaPathView.Filter = "应用程序（*.exe）|*.exe";
            if (JavaPathView.ShowDialog() == DialogResult.OK)
            {
                textBoxSetJavaPath.Text = JavaPathView.FileName;
            }
        }

        private void buttonAutoJavaPath_Click(object sender, EventArgs e)
        {
            FindJava();
        }

        private void FindJava()
        {
            string str = Java.Find();
            if (str != "Error")
            {
                textBoxSetJavaPath.Text = str;
                comboBoxJavaSelect.Items.Add(JavaVersion);
                comboBoxJavaSelect.SelectedIndex = 0;
            }
            else
            {
                DialogResult = MessageBox.Show(text: "Java获取失败，可能是没有安装Java也可能是启动器的问题，请尝试安装Java或配置Java环境变量。\n（但仍可以尝试启动，不过产生任何异常概不负责，还要继续运行吗？）", caption: "EMCL 无法找到Java", buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Error);
                if (DialogResult == DialogResult.OK)
                {
                    checkBoxSetJavaPath.Checked = true;
                    textBoxSetJavaPath.Enabled = true;
                    textBoxSetJavaPath.Text = "java";
                }
                else
                {
                    Environment.Exit(2);
                }
            }
        }

        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            if(listVersions.SelectedItem == null)
            {
                textStatus.Text = "请选择要启动的版本";
                return;
            }

            if(textBoxUsername.Text == "")
            {
                textStatus.Text = "请输入玩家名";
                return;
            }
            textStatus.Text = "正在准备启动游戏";

            Launcher.Launch(
                LoginType.Offline, listVersions.SelectedItem.ToString(), 
                int.Parse(textBoxSetMemory.Text), 
                textBoxUsername.Text, 
                textBoxJVMAdditionalParameter.Text, 
                textBoxMinecraftAdditionalParameter.Text, 
                textBoxStartDirectConnectionServer.Text, 
                int.Parse(textBoxGameWindowWidth.Text), 
                int.Parse(textBoxGameWindowHeight.Text), 
                textBoxSetJavaPath.Text, 
                !checkBoxDisableDefaultPublicAssets.Checked, 
                checkBoxDisableDefaultJVMParameter.Checked, 
                checkBoxVersionIsolate.Checked, 
                checkBoxFullScreen.Checked
            );

            textStatus.Text = "就绪";
        }

        private void checkBoxSetJavaPath_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSetJavaPath.Enabled = checkBoxSetJavaPath.Checked;
        }

        private void listBoxLoginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxLoginType.SelectedIndex == 0)
            {
                textBoxYggdrasilServer.Enabled = false;
                textBoxPassword.Enabled = false;
                buttonLogin.Enabled = false;
                listBoxPlayerRole.Enabled = false;
                textMimirNotice.Visible = false;
                loginType = LoginType.Offline;
            }
            if (listBoxLoginType.SelectedIndex == 1)
            {
                textBoxYggdrasilServer.Enabled = false;
                textBoxPassword.Enabled = true;
                buttonLogin.Enabled = true;
                listBoxPlayerRole.Enabled = false;
                textMimirNotice.Visible = false;
                loginType = LoginType.Yggdrasil;
            }
            if (listBoxLoginType.SelectedIndex == 2)
            {
                textBoxYggdrasilServer.Enabled = true;
                textBoxPassword.Enabled = true;
                buttonLogin.Enabled = true;
                listBoxPlayerRole.Enabled = true;
                textMimirNotice.Visible = false;
                loginType = LoginType.Authlib;
            }
            if (listBoxLoginType.SelectedIndex == 3)
            {
                textBoxYggdrasilServer.Enabled = true;
                textBoxPassword.Enabled = true;
                buttonLogin.Enabled = true;
                listBoxPlayerRole.Enabled = true;
                textMimirNotice.Visible = true;
                loginType = LoginType.Mimir;
            }
        }

        private void textBoxSetMemory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(int.Parse(textBoxSetMemory.Text) > int.Parse(ComputerInfo.GetMemoryAvailable()))
                {
                    textRemainMemoryActive.Text = (int.Parse(ComputerInfo.GetMemoryAvailable())).ToString() + " MB";
                    textBoxSetMemory.Text = (int.Parse(ComputerInfo.GetMemoryAvailable())).ToString();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                textBoxSetMemory.Text = (int.Parse(ComputerInfo.GetMemoryAvailable())).ToString();
            }
        }
    }
}
