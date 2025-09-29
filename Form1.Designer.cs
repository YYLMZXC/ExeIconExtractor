using System;
using System.Windows.Forms;

namespace ExeIconExtractor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnExtract = new System.Windows.Forms.Button();
            this.lstIcons = new System.Windows.Forms.ListBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();

            // txtExePath（EXE文件路径输入框）
            this.txtExePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExePath.Location = new System.Drawing.Point(12, 12);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.Size = new System.Drawing.Size(400, 23);
            this.txtExePath.TabIndex = 0;

            // btnBrowse（浏览按钮）
            this.btnBrowse.Location = new System.Drawing.Point(418, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            // btnExtract（提取按钮）
            this.btnExtract.Location = new System.Drawing.Point(499, 12);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnExtract.TabIndex = 2;
            this.btnExtract.Text = "提取图标";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);

            // lstIcons（图标列表）
            this.lstIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lstIcons.FormattingEnabled = true;
            this.lstIcons.ItemHeight = 15;
            this.lstIcons.Location = new System.Drawing.Point(12, 51);
            this.lstIcons.Name = "lstIcons";
            this.lstIcons.Size = new System.Drawing.Size(180, 364);
            this.lstIcons.TabIndex = 3;
            this.lstIcons.SelectedIndexChanged += new System.EventHandler(this.lstIcons_SelectedIndexChanged);

            // picPreview（图标预览）
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(198, 51);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(376, 364);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 4;
            this.picPreview.TabStop = false;

            // openFileDialog（文件选择对话框）
            this.openFileDialog.Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*";
            this.openFileDialog.Title = "选择EXE文件";

            // folderBrowser（文件夹选择对话框）
            this.folderBrowser.Description = "选择保存图标文件夹";

            // Form1（主窗体）
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 427);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.lstIcons);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtExePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EXE图标提取器";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox txtExePath;
        private Button btnBrowse;
        private Button btnExtract;
        private ListBox lstIcons;
        private PictureBox picPreview;
        private FolderBrowserDialog folderBrowser;
        private OpenFileDialog openFileDialog;
    }
}