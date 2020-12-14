using System.Net;

namespace DockingTest
{
   class MainClass
   {
      private static void ConfigureHttpsClient()
      {
         // Enable TLS 1.2 (which is NOT enabled by default in .NET 4.5 on Windows 10!).
         // The default value there is:
         //    ServicePointManager.SecurityProtocol = Ssl3 | Tls;
         // Without this enabling, you will get this exception from some https sites which require TLS 1.2:
         //    System.Net.WebException: The request was aborted: Could not create SSL/TLS secure channel
         // https://www.itsicherheit-online.com/news/ende-fuer-tls-1.0-und-tls-1.1-naht
         // https://www.zdnet.de/88378482/microsoft-verschiebt-support-ende-fuer-tls-1-0-und-1-1/       
         // Note that there is lots of wrong information in the internet regarding this topic...
         ServicePointManager.SecurityProtocol |= (
              // https://en.wikipedia.org/wiki/Transport_Layer_Security#SSL_1.0,_2.0,_and_3.0
              SecurityProtocolType.Ssl3   // SSL 3   - DEPRECATED, UNSAFE. Was superceded by TLS 1.0. We only enable it here for compatibility with ancient sites which have not upgraded to TLS 1.2 yet.
            | SecurityProtocolType.Tls    // TLS 1.0 - DEPRECATED, UNSAFE. We only enable it here for compatibility with ancient sites which have not upgraded to TLS 1.2 yet.
            | SecurityProtocolType.Tls11  // TLS 1.1 - DEPRECATED, UNSAFE. We only enable it here for compatibility with ancient sites which have not upgraded to TLS 1.2 yet.
            | SecurityProtocolType.Tls12  // TLS 1.2 - The current standard. Soon to be superceded by TLS 1.3.
         // | SecurityProtocolType.Tls13  // TLS 1.3 - The future standard (2018). Not available in .NET 4.5. https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls
         );
      }

      public static void Main(string [] args)
      {
         ConfigureHttpsClient();
         Gtk.Application.Init();
         MainWindow mainWin = new MainWindow();
         mainWin.Show();
         Gtk.Application.Run();
      }
   }
}
