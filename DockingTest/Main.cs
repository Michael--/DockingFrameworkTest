using System;
using Gtk;

namespace DockingTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow(args, "Docking Test");
            win.Show();
            Application.Run();
        }
    }
}
