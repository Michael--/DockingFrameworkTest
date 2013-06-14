using System;
using Docking.Components;

namespace Examples
{
   [System.ComponentModel.ToolboxItem(false)]
    public partial class FlorenceDockingDemo : Gtk.Bin
   {
      public FlorenceDockingDemo()
      {
         this.Build();
      }
   }

   #region Starter / Entry Point

   public class FlorenceFactory : ComponentFactory
   {
      public override Type TypeOfInstance { get { return typeof(FlorenceDockingDemo); } }
      public override String MenuPath { get { return @"View\Examples\Florence"; } }
      public override String Comment { get { return "Plot Wave example"; } }
      public override Mode Options { get { return Mode.CloseOnHide; } }
      public override Gdk.Pixbuf Icon { get { return Gdk.Pixbuf.LoadFromResource ("Examples.HelloWorld-16.png"); } }
   }

   #endregion

}

