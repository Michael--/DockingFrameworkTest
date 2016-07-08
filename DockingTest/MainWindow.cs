using System;
using Docking.Components;

public partial class MainWindow : ComponentManager
{
    public MainWindow (string [] args, string application_name): base (args, application_name)
    {
        var assemblyDirectory = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetEntryAssembly ().Location);

        // configuration should stored in shared folder and not in application folder, note: this is an example
        var configFile = System.IO.Path.Combine (assemblyDirectory, "config.xml");

        // load old configuration or init new one if not existing
        LicenseGroup.DefaultState = Docking.Components.LicenseGroup.State.ENABLED;
        LoadConfigurationFile (configFile);

        // Create designer elements
        Build ();

        // tell the component manager about all widgets to manage
        SetDockFrame(theDockFrame);
        // SetStatusBar(theStatusBar);
        // SetToolBar(theToolBar);
        SetMenuBar(menubar3);

        // scan all *.exe and *.dll files for component factories
        string [] search = { System.IO.Path.Combine(assemblyDirectory, "*.exe"),
                             System.IO.Path.Combine(assemblyDirectory, "*.dll") };
        ComponentFinder.SearchForComponents (search);

        // install all known component menus
        AddComponentMenus();

        InstallLanguageMenu ("Options");

        LoadLayout ();
        SetLanguage ("default", true, false);

        // update with own persistence
        LoadPersistency (true);

        // after layout has been set, call component initialization
        // any component could load its persistence data now
        ComponentsLoaded();

        UpdateLanguage (false);
    }
}
