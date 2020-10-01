using System;
using System.IO;
using Docking.Components;

public partial class MainWindow : ComponentManager
{
   public MainWindow (string [] args, string application_name)
   : base (args, "en-US", application_name)
   {
      // running with mac os, Graphics could fail
      GLib.ExceptionManager.UnhandledException += (a) => 
      {
         MessageWriteLine ("{0}: {1}", a, a.ExceptionObject);
      };

      var assemblyDirectory = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetEntryAssembly ().Location);

      // configuration should stored in shared folder and not in application folder, note: this is an example
      var configFile = System.IO.Path.Combine (assemblyDirectory, "config.xml");
      var defaultConfigFile = System.IO.Path.Combine (assemblyDirectory, "default_config.xml");

      // copy default config if exist as config if not already exist
      // nice for 1st start of test app to avoid empty framework
      // the default config is a part of this project
      if (!File.Exists (configFile))
      {
         if (File.Exists (defaultConfigFile))
            File.Copy (defaultConfigFile, configFile);
      }

      // load old configuration or init new one if not existing
      LicenseGroup.DefaultState = Docking.Components.LicenseGroup.State.ENABLED;
      LoadConfigurationFile (configFile);

      // Create designer elements
      Build ();

      // tell the component manager about all widgets to manage
      SetDockFrame (theDockFrame);
      theStatusBar.Visible = false; // SetStatusBar(theStatusBar);
      theToolBar.Visible = false;   // SetToolBar(theToolBar);
      SetMenuBar (menubar3);

      // scan all *.exe and *.dll files for component factories
      string [] search = { System.IO.Path.Combine(assemblyDirectory, "*.exe"),
                           System.IO.Path.Combine(assemblyDirectory, "*.dll") };
      ComponentFinder.SearchForComponents (search);

      // install all known component menus
      AddComponentMenus ();

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
