﻿namespace AvalonDockMvvmTest.View.Pane
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Windows.Controls;
  using System.Windows;
  using AvalonDock.Layout;
  using AvalonDockMvvmTest.ViewModel;

  class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {
        
        }


        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate RecentFilesViewTemplate
        {
          get;
          set;
        }

        public DataTemplate FileStatsViewTemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
                return FileViewTemplate;

            //if (item is FileStatsViewModel)
            //    return FileStatsViewTemplate;

            //if (item is RecentFilesViewModel)
            //  return RecentFilesViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
