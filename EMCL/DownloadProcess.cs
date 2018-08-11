using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMCL
{
    public partial class DownloadProcess : Form
    {
        public static int Process = 0;
        public static bool IsFinish = false;

        public DownloadProcess()
        {
            InitializeComponent();
        }

        private void buttonDownloadCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DownloadProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsFinish)
            {
                DialogResult dr = MessageBox.Show(text: "真的要退出吗？", caption: "EMCL", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
