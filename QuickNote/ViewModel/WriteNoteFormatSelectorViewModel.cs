using QuickNote.View.UserControls.NoteFormatTool;
using QuickNote.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Xml;
using System.ComponentModel;
using QuickNote.Model;

namespace QuickNote.ViewModel
{
    public class WriteNoteFormatSelectorViewModel : INotifyPropertyChanged
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
        private WriteNoteFormatSelectorView _view;
        private ObservableCollection<string> _noteFormatCollection;
        private string _selectedWriteNoteFormat;
        #endregion

        #region Property
        public WriteNoteFormatSelectorView View
        {
            get => _view;
            set => _view = value;
        }
        public ObservableCollection<string> NoteFormatCollection
        {
            get => _noteFormatCollection;
            set
            {
                _noteFormatCollection = value;
                NotifyPropertyChanged(nameof(NoteFormatCollection));
            }
        }
        public string SelectedWriteNoteFormat
        {
            get => _selectedWriteNoteFormat;
            set
            {
                _selectedWriteNoteFormat = value;
                NotifyPropertyChanged(nameof(SelectedWriteNoteFormat));

                // 改良の余地あり。
                _viewModel.NoteControllerViewModel.SetNoteForm();
                _viewModel.LeftUpperContent = _viewModel.NoteControllerViewModel.View;

                _viewModel.InputTagCollection.Clear();
                _viewModel.AllTagCollection.Clear();
                _viewModel.TagSelectorViewModel.InitializeTag();
            }
        }
        #endregion

        #region Constructor
        public WriteNoteFormatSelectorViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            SetNoteFormatList();
            _selectedWriteNoteFormat = _viewModel.Setting.LastSelectedFormat;
            _view = new WriteNoteFormatSelectorView(this);
        }
        #endregion

        #region Method
        private void SetNoteFormatList()
        {
            if (_noteFormatCollection == null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("NoteFormat.xml");
                XmlNodeList elements = doc.SelectNodes($"/NoteFormat/*");
                ObservableCollection<string> noteFormatCollection = new ObservableCollection<string>();
                foreach (XmlNode element in elements)
                {
                    noteFormatCollection.Add(element.Name);
                }
                _noteFormatCollection = noteFormatCollection;
            }
        }
        #endregion
    }
}
