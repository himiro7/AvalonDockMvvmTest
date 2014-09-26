﻿using System;

using Microsoft.Practices.Prism.ViewModel;

namespace AvalonDockMvvmTest.ViewModel
{
  public class PaneViewModel : NotificationObject
  {
    public PaneViewModel()
    { }

    protected MainWindowViewModel Workspace;

    #region Title

    private string _title = null;
    public string Title
    {
      get { return _title; }
      set
      {
        if (_title != value)
        {
          _title = value;
          RaisePropertyChanged(()=>Title);
        }
      }
    }

    #endregion

    public virtual Uri IconSource
    {
      get;

      protected set;
    }

    #region ContentId

    private string _contentId = null;
    public string ContentId
    {
      get { return _contentId; }
      set
      {
        if (_contentId != value)
        {
          _contentId = value;
          RaisePropertyChanged(()=>ContentId);
        }
      }
    }

    #endregion

    #region IsSelected

    private bool _isSelected = false;
    public bool IsSelected
    {
      get { return _isSelected; }
      set
      {
        if (_isSelected != value)
        {
          _isSelected = value;
          RaisePropertyChanged(()=>IsSelected);
        }
      }
    }

    #endregion

    #region IsActive

    private bool _isActive = false;
    public bool IsActive
    {
      get { return _isActive; }
      set
      {
        if (_isActive != value)
        {
          _isActive = value;
          RaisePropertyChanged("IsActive");
        }
      }
    }

    #endregion


  }
}
