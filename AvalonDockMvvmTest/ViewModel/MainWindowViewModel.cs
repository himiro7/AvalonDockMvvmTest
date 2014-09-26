using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AvalonDockMvvmTest.ViewModel
{
  public class MainWindowViewModel : NotificationObject
  {
    public MainWindowViewModel()
    {

    }

    ObservableCollection<FileViewModel> _Files = new ObservableCollection<FileViewModel>();
    ReadOnlyObservableCollection<FileViewModel> _ReadOnlyFiles = null;
    public ReadOnlyObservableCollection<FileViewModel> Files
    {
      get
      {
        return this._ReadOnlyFiles = this._ReadOnlyFiles ??
          new ReadOnlyObservableCollection<FileViewModel>(_Files);
      }
    }

    #region NewCommand
    DelegateCommand _NewCommand = null;
    public DelegateCommand NewCommand
    {
      get
      {
        return this._NewCommand = this._NewCommand ??
          new DelegateCommand(OnNew);
      }
    }

    private void OnNew()
    {
      _Files.Add(new FileViewModel(this));
      ActiveDocument = _Files.Last();

    }
    #endregion

    #region ActiveDocument
    private FileViewModel _ActiveDocument = null;
    public FileViewModel ActiveDocument
    {
      get { return _ActiveDocument; }
      set
      {
        if(_ActiveDocument != value)
        {
          _ActiveDocument = value;
          RaisePropertyChanged(() => ActiveDocument);
          if(ActiveDoumentChanged != null)
          {
            ActiveDoumentChanged(this, EventArgs.Empty);
          }
          
        }
      }
    }

    public event EventHandler ActiveDoumentChanged;
    #endregion

    internal void Close(FileViewModel fileToClose)
    {
      if (fileToClose.IsDirty)
      {
        var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", fileToClose.FileName), "AvalonDock Test App", MessageBoxButton.YesNoCancel);
        if (res == MessageBoxResult.Cancel)
          return;
        if (res == MessageBoxResult.Yes)
        {
          Save(fileToClose);
        }
      }

      _Files.Remove(fileToClose);
    }

    internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
    {
      if (fileToSave.FilePath == null || saveAsFlag)
      {
        var dlg = new SaveFileDialog();
        if (dlg.ShowDialog().GetValueOrDefault())
          fileToSave.FilePath = dlg.SafeFileName;
      }

      File.WriteAllText(fileToSave.FilePath, fileToSave.TextContent);
      ActiveDocument.IsDirty = false;
    }

    #region OpenCommand
    DelegateCommand<object> _openCommand = null;
    public DelegateCommand<object> OpenCommand
    {
      get
      {
        if (_openCommand == null)
        {
          _openCommand = new DelegateCommand<object>((p) => OnOpen(p), (p) => CanOpen(p));
        }

        return _openCommand;
      }
    }

    private bool CanOpen(object parameter)
    {
      return true;
    }

    private void OnOpen(object parameter)
    {
      var dlg = new OpenFileDialog();
      if (dlg.ShowDialog().GetValueOrDefault())
      {
        var fileViewModel = Open(dlg.FileName);
        ActiveDocument = fileViewModel;
      }
    }

    public FileViewModel Open(string filepath)
    {
      // Verify whether file is already open in editor, and if so, show it
      var fileViewModel = _Files.FirstOrDefault(fm => fm.FilePath == filepath);

      if (fileViewModel != null)
      {
        this.ActiveDocument = fileViewModel; // File is already open so shiw it

        return fileViewModel;
      }

      fileViewModel = _Files.FirstOrDefault(fm => fm.FilePath == filepath);
      if (fileViewModel != null)
        return fileViewModel;

      fileViewModel = new FileViewModel(filepath, this);
      _Files.Add(fileViewModel);
      //this.RecentFiles.AddNewEntryIntoMRU(filepath);

      return fileViewModel;
    }

    #endregion

  }
}
