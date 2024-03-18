using QuickNote.Util;
using QuickNote.View.UserControls.NoteFormatTool;
using QuickNote.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using System.Xml;
using QuickNote.Model;
using System.Diagnostics;
using QuickNote.Util.CustomControl;

namespace QuickNote.ViewModel
{
    public class TagSearchViewModel:INotifyPropertyChanged
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
        private TagSearchView _view;
        private string _searchTextBoxContent;
        #endregion

        #region Property
        public TagSearchView View
        {
            get => _view;
            set => _view = value;
        }
        public string SearchTextBoxContent
        {
            get => _searchTextBoxContent;
            set
            {
                _searchTextBoxContent = value;
                NotifyPropertyChanged(nameof(SearchTextBoxContent));
            }
        }
        #endregion

        #region Command
        public ICommand SearchTagCommand { get; }
        #endregion

        #region Constructor
        public TagSearchViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _searchTextBoxContent= "Search";
            SearchTagCommand = new RelayCommand(SearchTag);
            _view= new TagSearchView(this);

        }
        #endregion

        #region Method
        public void SearchTag(object obj)
        {
            var SearchTextBox = (PlaceholderTextBox)_view.FindName("Search");
            if (_viewModel.AllTagCollection == null) return;

            _viewModel.AllTagCollection.Clear();
            foreach (string name in _viewModel.Setting.TagButtonNameList)
            {
                if (_viewModel.InputTagCollection.Contains(name) && _viewModel.Mode == "Writer") continue;
                if (_viewModel.SortTagCollection.Contains(name) && _viewModel.Mode == "Viewer") continue;

                if (!SearchTextBox.IsTextInput)
                {
                    _viewModel.AllTagCollection.Add(name);
                    continue;
                }

                if (SearchTextBox.IsTextInput && name.Contains(SearchTextBoxContent))
                {
                    _viewModel.AllTagCollection.Add(name);
                    continue;
                }
            }

            if (_viewModel.AllTagCollection.Count == 0)
            {
                Trace.WriteLine("0 hit word.");
            }
        }
        #endregion
    }
}
