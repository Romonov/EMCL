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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // DownloadProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 341);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDownloadSpeed);
            this.Controls.Add(this.textDownloadProcessAll);
            this.Controls.Add(this.buttonDownloadCancel);
            this.Controls.Add(this.progressBarDownloadAll);
            this.Controls.Add(this.listViewDownloadProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadProcess";
            this.Text = "正在下载，请稍后...已下载 0%";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}