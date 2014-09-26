﻿namespace AvalonDockMvvmTest.View.Pane
{
  using System.Windows;
  using System.Windows.Controls;
  using AvalonDockMvvmTest.ViewModel;


  class PanesStyleSelector : StyleSelector
  {
    public Style ToolStyle
    {
      get;
      set;
    }

    public Style FileStyle
    {
      get;
      set;
    }

    public Style RecentFilesStyle
    {
      get;
      set;
    }

    public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
    {
    //  if (item is RecentFilesViewModel)
    //    return RecentFilesStyle;

    //if (item is ToolViewModel)
    //    return ToolStyle;

      if (item is FileViewModel)
        return FileStyle;

      return base.SelectStyle(item, container);
    }
  }
}
