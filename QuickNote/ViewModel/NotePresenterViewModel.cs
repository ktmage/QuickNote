using QuickNote.Model;
using QuickNote.Util;
using QuickNote.View;
using QuickNote.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace QuickNote.ViewModel
{
    public class NotePresenterViewModel : INotifyPropertyChanged
    {
        #region ViewModel Base
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion

        #region Field
        private ViewModel _viewModel;
        private NotePresenterView _view;
        private ObservableCollection<Note> _sortedNotes;
        private ObservableCollection<PopUpWindow> _popUpWindows;

        private double _upperShadowOpacity;
        private double _lowerShadowOpacity;
        #endregion

        #region Property
        public NotePresenterView View
        {
            get => _view;
            set => _view = value;
        }
        public ObservableCollection<Note> SortedNotes
        {
            get => _sortedNotes;
            set => _sortedNotes = value;
        }
        public ObservableCollection<PopUpWindow> PopUpWindows
        {
            get => _popUpWindows;
            set
            {
                _popUpWindows = value;
                NotifyPropertyChanged(nameof(PopUpWindows));
            }
        }

        public double UpperShadowOpacity
        {
            get => _upperShadowOpacity;
            set
            {
                _upperShadowOpacity = value;
                NotifyPropertyChanged(nameof(UpperShadowOpacity));
            }
        }
        public double LowerShadowOpacity
        {
            get => _lowerShadowOpacity;
            set
            {
                _lowerShadowOpacity = value;
                NotifyPropertyChanged(nameof(LowerShadowOpacity));
            }
        }
        #endregion

        #region Command
        public ICommand SearchNoteCommand { get; }
        public ICommand OpenNoteCommand { get; }
        #endregion

        #region Constructor
        public NotePresenterViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _sortedNotes= new ObservableCollection<Note>();
            OpenNoteCommand = new RelayCommand(OpenNote);

            _view = new NotePresenterView(this);
        }
        #endregion

        #region Method
        public void OpenNote(object obj)
        {
            var note = (Note)obj;
            var popUpWindow = new PopUpWindow(note);
            popUpWindow.Show();

            //PopUpWindows.Add(popUpWindow);
        }




        #endregion
    }
}
