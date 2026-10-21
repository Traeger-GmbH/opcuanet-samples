// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client.NodeExplorer
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// This sample demonstrates how to browse the address space of an OPC UA server and show it as
    /// a tree in a Windows Forms application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
#if NET
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
