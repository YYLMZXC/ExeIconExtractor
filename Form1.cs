using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExeIconExtractor
{
    public partial class Form1 : Form
    {
        // 存储提取的图标列表
        private List<Icon> extractedIcons = new List<Icon>();

        // Windows API 函数声明（用于提取图标）
        [DllImport("shell32.dll")]
        private static extern uint ExtractIconEx(string lpszFile, int nIconIndex,
            IntPtr[] phIconLarge, IntPtr[] phIconSmall, uint nIcons);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 浏览按钮点击事件：选择EXE文件
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExePath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// 提取按钮点击事件：提取并保存图标
        /// </summary>
        private void btnExtract_Click(object sender, EventArgs e)
        {
            // 验证文件路径有效性
            if (string.IsNullOrEmpty(txtExePath.Text) || !File.Exists(txtExePath.Text))
            {
                MessageBox.Show("请选择有效的EXE文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 清空之前的提取结果
                extractedIcons.Clear();
                lstIcons.Items.Clear();
                picPreview.Image = null;

                // 获取EXE文件中的图标总数
                uint iconCount = ExtractIconEx(txtExePath.Text, -1, null, null, 0);

                if (iconCount == 0)
                {
                    MessageBox.Show("未找到图标资源", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 逐个提取图标
                for (int i = 0; i < iconCount; i++)
                {
                    IntPtr[] largeIcons = new IntPtr[1]; // 存储大图标的句柄
                    IntPtr[] smallIcons = new IntPtr[1]; // 存储小图标的句柄

                    // 提取第i个图标（只处理大图标）
                    ExtractIconEx(txtExePath.Text, i, largeIcons, smallIcons, 1);

                    if (largeIcons[0] != IntPtr.Zero)
                    {
                        // 从句柄创建图标，并克隆避免句柄释放问题
                        using (Icon icon = Icon.FromHandle(largeIcons[0]))
                        {
                            extractedIcons.Add((Icon)icon.Clone());
                        }
                        DestroyIcon(largeIcons[0]); // 释放图标句柄
                    }

                    // 释放小图标句柄（不使用小图标）
                    if (smallIcons[0] != IntPtr.Zero)
                    {
                        DestroyIcon(smallIcons[0]);
                    }

                    // 添加到列表框
                    lstIcons.Items.Add($"图标 {i + 1}");
                }

                // 自动选择第一个图标并提示保存
                if (lstIcons.Items.Count > 0)
                {
                    lstIcons.SelectedIndex = 0;

                    // 选择保存目录并保存图标
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        SaveIcons(folderBrowser.SelectedPath);
                        MessageBox.Show($"成功提取 {extractedIcons.Count} 个图标到:\n{folderBrowser.SelectedPath}",
                            "提取成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"提取失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 图标列表选择变更事件：预览选中的图标
        /// </summary>
        private void lstIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstIcons.SelectedIndex >= 0 && lstIcons.SelectedIndex < extractedIcons.Count)
            {
                picPreview.Image = extractedIcons[lstIcons.SelectedIndex].ToBitmap();
            }
        }

        /// <summary>
        /// 将提取的图标保存为PNG文件
        /// </summary>
        /// <param name="savePath">保存目录</param>
        private void SaveIcons(string savePath)
        {
            for (int i = 0; i < extractedIcons.Count; i++)
            {
                string fileName = $"Icon_{i + 1}.png";
                string fullPath = Path.Combine(savePath, fileName);

                // 转换为位图并保存
                using (Bitmap bmp = extractedIcons[i].ToBitmap())
                {
                    bmp.Save(fullPath, ImageFormat.Png);
                }
            }
        }
    }
}