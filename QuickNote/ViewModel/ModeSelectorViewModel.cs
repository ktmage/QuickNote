using QuickNote.Model;
using QuickNote.Util;
using QuickNote.View.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickNote.ViewModel
{
    public class ModeSelectorViewModel : INotifyPropertyChanged
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
        private ModeSelectorView _view;
        private string _mode;
        #endregion

        #region Property
        public ModeSelectorView View
        {
            get => _view;
            set => _view = value;
        }
        public string Mode
        {
            get => _mode;
            set => _mode = value;
        }
        #endregion

        #region Command
        public ICommand SetModeCommand { get; }
        #endregion

        #region Constructor
        public ModeSelectorViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            SetModeCommand = new RelayCommand(SetMode);
            _view = new ModeSelectorView(this);
        }
        #endregion

        #region Method
        public void SetMode(object obj)
        {
            if (Mode == obj) return;
            switch (obj)
            {
                case "Writer":
                    Mode = obj.ToString();
                    ChangeModeWriter();
                    break;

                case "Viewer":
                    Mode = obj.ToString();
                    ChangeModeViewer();
                    break;
            }
        }

        public void ChangeModeWriter()
        {
            _viewModel.InitializeGridContent();
            _viewModel.TagSelectorViewModel.InitializeTag();
            _viewModel.InputTagCollection.Clear();

            // LeftLowerContentのサイズを60pxに。
            _viewModel.MainWindow.LeftContentGrid.RowDefinitions[1].Height = new System.Windows.GridLength(60);

            _viewModel.TopContent = _viewModel.WriteNoteFormatSelectorViewModel.View;
            _viewModel.LeftUpperContent = _viewModel.NoteControllerViewModel.View;
            _viewModel.LeftLowerContent = _viewModel.SendButtonViewModel.View;
            _viewModel.MiddleUpperContent = _viewModel.TagSelectorViewModel.View;
            _viewModel.MiddleLowerContent = _viewModel.TagSearchViewModel.View;
            _viewModel.RightContent = _viewModel.ModeSelectorViewModel.View;

        }

        public void ChangeModeViewer()
        {
            _viewModel.InitializeGridContent();
            _viewModel.TagSelectorViewModel.InitializeTag();
            _viewModel.SortTagCollection.Clear();

            // LeftLowerContentのサイズを0pxに。
            _viewModel.MainWindow.LeftContentGrid.RowDefinitions[1].Height = new System.Windows.GridLength(0);

            _viewModel.TopContent = _viewModel.NoteSelectorViewModel.View;
            _viewModel.LeftUpperContent = _viewModel.NotePresenterViewModel.View;
            _viewModel.MiddleUpperContent = _viewModel.TagSelectorViewModel.View;
            _viewModel.MiddleLowerContent = _viewModel.TagSearchViewModel.View;
            _viewModel.RightContent = _viewModel.ModeSelectorViewModel.View;

        }
        #endregion
    }
}
