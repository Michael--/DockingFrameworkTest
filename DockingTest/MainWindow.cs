using System.IO;
using Docking.Components;

public partial class MainWindow : ComponentManager
{
   public MainWindow()
   {
      LicenseGroup.DefaultState = Docking.Components.LicenseGroup.State.ENABLED;

      MessageBox.Init(this);

      this.Title = ApplicationName;

      // running with mac os, Graphics could fail
      GLib.ExceptionManager.UnhandledException += (a) => 
      {
         MessageWriteLine ("{0}: {1}", a, a.ExceptionObject);
      };

      var assemblyDirectory = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetEntryAssembly ().Location);

      // configuration should stored in shared folder and not in application folder, note: this is an example
      var configFile = System.IO.Path.Combine (assemblyDirectory, "config.xml");
      var defaultConfigFile = System.IO.Path.Combine (assemblyDirectory, "default_config.xml");

      // if no config exists, take default config as initial setup
      if(!File.Exists (configFile))
         if(File.Exists (defaultConfigFile))
            File.Copy (defaultConfigFile, configFile);
      
      // load old configuration or init new one if not existing
      LoadConfigurationFile (configFile);

      // Create designer elements
      Build ();

      // tell the component manager about all widgets to manage
      SetDockFrame (theDockFrame);
      theStatusBar.Visible = false; // SetStatusBar(theStatusBar);
      theToolBar.Visible = false;   // SetToolBar(theToolBar);
      SetMenuBar (menubar3);

      SearchLoadAndInitializeComponentsFromDLLs();

      InstallLanguageMenu ("Options");

      LoadLayout ();

      SetLanguage ("default", true, false);

      // update with own persistence
      LoadPersistency (true);

      // after layout has been set, call component initialization
      // any component could load its persistence data now
      ComponentsLoaded ();

      UpdateLanguage (false);
   }
}
