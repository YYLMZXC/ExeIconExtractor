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
        // �洢��ȡ��ͼ���б�
        private List<Icon> extractedIcons = new List<Icon>();

        // Windows API ����������������ȡͼ�꣩
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
        /// �����ť����¼���ѡ��EXE�ļ�
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExePath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// ��ȡ��ť����¼�����ȡ������ͼ��
        /// </summary>
        private void btnExtract_Click(object sender, EventArgs e)
        {
            // ��֤�ļ�·����Ч��
            if (string.IsNullOrEmpty(txtExePath.Text) || !File.Exists(txtExePath.Text))
            {
                MessageBox.Show("��ѡ����Ч��EXE�ļ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ���֮ǰ����ȡ���
                extractedIcons.Clear();
                lstIcons.Items.Clear();
                picPreview.Image = null;

                // ��ȡEXE�ļ��е�ͼ������
                uint iconCount = ExtractIconEx(txtExePath.Text, -1, null, null, 0);

                if (iconCount == 0)
                {
                    MessageBox.Show("δ�ҵ�ͼ����Դ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // �����ȡͼ��
                for (int i = 0; i < iconCount; i++)
                {
                    IntPtr[] largeIcons = new IntPtr[1]; // �洢��ͼ��ľ��
                    IntPtr[] smallIcons = new IntPtr[1]; // �洢Сͼ��ľ��

                    // ��ȡ��i��ͼ�ֻ꣨�����ͼ�꣩
                    ExtractIconEx(txtExePath.Text, i, largeIcons, smallIcons, 1);

                    if (largeIcons[0] != IntPtr.Zero)
                    {
                        // �Ӿ������ͼ�꣬����¡�������ͷ�����
                        using (Icon icon = Icon.FromHandle(largeIcons[0]))
                        {
                            extractedIcons.Add((Icon)icon.Clone());
                        }
                        DestroyIcon(largeIcons[0]); // �ͷ�ͼ����
                    }

                    // �ͷ�Сͼ��������ʹ��Сͼ�꣩
                    if (smallIcons[0] != IntPtr.Zero)
                    {
                        DestroyIcon(smallIcons[0]);
                    }

                    // ��ӵ��б��
                    lstIcons.Items.Add($"ͼ�� {i + 1}");
                }

                // �Զ�ѡ���һ��ͼ�겢��ʾ����
                if (lstIcons.Items.Count > 0)
                {
                    lstIcons.SelectedIndex = 0;

                    // ѡ�񱣴�Ŀ¼������ͼ��
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        SaveIcons(folderBrowser.SelectedPath);
                        MessageBox.Show($"�ɹ���ȡ {extractedIcons.Count} ��ͼ�굽:\n{folderBrowser.SelectedPath}",
                            "��ȡ�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡʧ��: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ͼ���б�ѡ�����¼���Ԥ��ѡ�е�ͼ��
        /// </summary>
        private void lstIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstIcons.SelectedIndex >= 0 && lstIcons.SelectedIndex < extractedIcons.Count)
            {
                picPreview.Image = extractedIcons[lstIcons.SelectedIndex].ToBitmap();
            }
        }

        /// <summary>
        /// ����ȡ��ͼ�걣��ΪPNG�ļ�
        /// </summary>
        /// <param name="savePath">����Ŀ¼</param>
        private void SaveIcons(string savePath)
        {
            for (int i = 0; i < extractedIcons.Count; i++)
            {
                string fileName = $"Icon_{i + 1}.png";
                string fullPath = Path.Combine(savePath, fileName);

                // ת��Ϊλͼ������
                using (Bitmap bmp = extractedIcons[i].ToBitmap())
                {
                    bmp.Save(fullPath, ImageFormat.Png);
                }
            }
        }
    }
}