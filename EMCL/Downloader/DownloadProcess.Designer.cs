namespace EMCL
{
    partial class DownloadProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadProcess));
            this.listViewDownloadProcess = new System.Windows.Forms.ListView();
            this.columnHeaderDownloadItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDownloadProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarDownloadAll = new System.Windows.Forms.ProgressBar();
            this.buttonDownloadCancel = new System.Windows.Forms.Button();
            this.textDownloadProcessAll = new System.Windows.Forms.Label();
            this.textDownloadSpeed = new System.Windows.Forms.Label();
            this.textDownloadSpeedActive = new System.Windows.Forms.Label();
            this.textDownloadRemainTime = new System.Windows.Forms.Label();
            this.textDownloadRemainTimeActive = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewDownloadProcess
            // 
            this.listViewDownloadProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDownloadItem,
            this.columnHeaderDownloadProcess});
            this.listViewDownloadProcess.Location = new System.Drawing.Point(2, 2);
            this.listViewDownloadProcess.Name = "listViewDownloadProcess";
            this.listViewDownloadProcess.Size = new System.Drawing.Size(559, 275);
            this.listViewDownloadProcess.TabIndex = 0;
            this.listViewDownloadProcess.UseCompatibleStateImageBehavior = false;
            this.listViewDownloadProcess.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDownloadItem
            // 
            this.columnHeaderDownloadItem.Text = "项目";
            this.columnHeaderDownloadItem.Width = 481;
            // 
            // columnHeaderDownloadProcess
            // 
            this.columnHeaderDownloadProcess.Text = "进度";
            // 
            // progressBarDownloadAll
            // 
            this.progressBarDownloadAll.Location = new System.Drawing.Point(83, 283);
            this.progressBarDownloadAll.Name = "progressBarDownloadAll";
            this.progressBarDownloadAll.Size = new System.Drawing.Size(478, 23);
            this.progressBarDownloadAll.TabIndex = 1;
            // 
            // buttonDownloadCancel
            // 
            this.buttonDownloadCancel.Location = new System.Drawing.Point(486, 312);
            this.buttonDownloadCancel.Name = "buttonDownloadCancel";
            this.buttonDownloadCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonDownloadCancel.TabIndex = 2;
            this.buttonDownloadCancel.Text = "取消";
            this.buttonDownloadCancel.UseVisualStyleBackColor = true;
            this.buttonDownloadCancel.Click += new System.EventHandler(this.buttonDownloadCancel_Click);
            // 
            // textDownloadProcessAll
            // 
            this.textDownloadProcessAll.AutoSize = true;
            this.textDownloadProcessAll.Location = new System.Drawing.Point(0, 288);
            this.textDownloadProcessAll.Name = "textDownloadProcessAll";
            this.textDownloadProcessAll.Size = new System.Drawing.Size(77, 12);
            this.textDownloadProcessAll.TabIndex = 3;
            this.textDownloadProcessAll.Text = "下载总进度：";
            // 
            // textDownloadSpeed
            // 
            this.textDownloadSpeed.AutoSize = true;
            this.textDownloadSpeed.Location = new System.Drawing.Point(0, 317);
            this.textDownloadSpeed.Name = "textDownloadSpeed";
            this.textDownloadSpeed.Size = new System.Drawing.Size(65, 12);
            this.textDownloadSpeed.TabIndex = 4;
            this.textDownloadSpeed.Text = "下载速度：";
            // 
            // textDownloadSpeedActive
            // 
            this.textDownloadSpeedActive.AutoSize = true;
            this.textDownloadSpeedActive.Location = new System.Drawing.Point(71, 317);
            this.textDownloadSpeedActive.Name = "textDownloadSpeedActive";
            this.textDownloadSpeedActive.Size = new System.Drawing.Size(29, 12);
            this.textDownloadSpeedActive.TabIndex = 5;
            this.textDownloadSpeedActive.Text = "0B/s";
            // 
            // textDownloadRemainTime
            // 
            this.textDownloadRemainTime.AutoSize = true;
            this.textDownloadRemainTime.Location = new System.Drawing.Point(207, 317);
            this.textDownloadRemainTime.Name = "textDownloadRemainTime";
            this.textDownloadRemainTime.Size = new System.Drawing.Size(65, 12);
            this.textDownloadRemainTime.TabIndex = 6;
            this.textDownloadRemainTime.Text = "剩余时间：";
            // 
            // textDownloadRemainTimeActive
            // 
            this.textDownloadRemainTimeActive.AutoSize = true;
            this.textDownloadRemainTimeActive.Location = new System.Drawing.Point(278, 317);
            this.textDownloadRemainTimeActive.Name = "textDownloadRemainTimeActive";
            this.textDownloadRemainTimeActive.Size = new System.Drawing.Size(53, 12);
            this.textDownloadRemainTimeActive.TabIndex = 7;
            this.textDownloadRemainTimeActive.Text = "00:00:00";
            // 
            // DownloadProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 341);
            this.Controls.Add(this.textDownloadRemainTimeActive);
            this.Controls.Add(this.textDownloadRemainTime);
            this.Controls.Add(this.textDownloadSpeedActive);
            this.Controls.Add(this.textDownloadSpeed);
            this.Controls.Add(this.textDownloadProcessAll);
            this.Controls.Add(this.buttonDownloadCancel);
            this.Controls.Add(this.progressBarDownloadAll);
            this.Controls.Add(this.listViewDownloadProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadProcess";
            this.Text = "正在下载，请稍后...已下载 0%";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadProcess_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewDownloadProcess;
        private System.Windows.Forms.ProgressBar progressBarDownloadAll;
        private System.Windows.Forms.ColumnHeader columnHeaderDownloadItem;
        private System.Windows.Forms.ColumnHeader columnHeaderDownloadProcess;
        private System.Windows.Forms.Button buttonDownloadCancel;
        private System.Windows.Forms.Label textDownloadProcessAll;
        private System.Windows.Forms.Label textDownloadSpeed;
        private System.Windows.Forms.Label textDownloadSpeedActive;
        private System.Windows.Forms.Label textDownloadRemainTime;
        private System.Windows.Forms.Label textDownloadRemainTimeActive;
    }
}