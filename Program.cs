using System;
using System.Windows.Forms;

namespace ExeIconExtractor
{
    internal static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ�
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ��ʼ��Ӧ�ó������ã���DPI��Ĭ������ȣ�
            ApplicationConfiguration.Initialize();
            // ����������
            Application.Run(new Form1());
        }
    }
}