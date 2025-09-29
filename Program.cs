using System;
using System.Windows.Forms;

namespace ExeIconExtractor
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 初始化应用程序配置（高DPI、默认字体等）
            ApplicationConfiguration.Initialize();
            // 启动主窗体
            Application.Run(new Form1());
        }
    }
}