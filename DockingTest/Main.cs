namespace DockingTest
{
   class MainClass
   {
      public static void Main(string [] args)
      {
         Gtk.Application.Init();
         MainWindow mainWin = new MainWindow();
         mainWin.Show();
         Gtk.Application.Run();
      }
   }
}
