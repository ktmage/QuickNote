using QuickNote.Model;
using QuickNote.Util;
using QuickNote.Util.CustomControl;
using QuickNote.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickNote.ViewModel
{
    public class NoteSelectorViewModel: INotifyPropertyChanged
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
        private NoteSelectorView _view;

        private ObservableCollection<string> _sortTagCollection;
        private string _sortContent;
        private string _sortedNoteFormat;
        //private ObservableCollection<string> _noteFormatCollection;
        #endregion

        

        #region Property
        public NoteSelectorView View
        {
            get => _view;
            set => _view = value;
        }
        public ObservableCollection<string> SortTagCollection
        {
            get => _sortTagCollection;
            set
            {
                _sortTagCollection = value;
                NotifyPropertyChanged(nameof(SortTagCollection));
                SortNote();
            }
        }
        public string SortContent
        {
            get => _sortContent;
            set
            {
                _sortContent= value;
                NotifyPropertyChanged(nameof(SortContent));
                SortNote();
            }
        }
        public string SortedNoteFormat
        {
            get => _sortedNoteFormat;
            set
            {
                _sortedNoteFormat= value;
                NotifyPropertyChanged(nameof(SortedNoteFormat));
                SortNote();
            }
        }
        public ObservableCollection<string> NoteFormatCollection
        {
            get => _viewModel.NoteFormatCollection; 
        }
        #endregion

        #region Command
        public ICommand RemoveTagCommand { get; }
        #endregion

        #region Constructor
        public NoteSelectorViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            RemoveTagCommand = new RelayCommand(RemoveTag);
            _view = new NoteSelectorView(this);

            _sortTagCollection = new ObservableCollection<string>();
            _sortTagCollection.CollectionChanged += OnCollectionChanged;
            _sortContent = "Search";
            _sortedNoteFormat = "CaptureWeb";

            SortNote();

        }
        #endregion

        #region Method
        private void RemoveTag(object obj)
        {
            var tmp = (string)obj;
            if (obj != null)
            {
                _viewModel.AllTagCollection.Add(tmp);
                SortTagCollection.Remove(tmp);

                // 整列
                _viewModel.TagSearchViewModel.SearchTag(null);
            }
        }

        public void SortNote()
        {
            _viewModel.SortedNotes.Clear();
            var SearchTextBox = (PlaceholderTextBox)_view.FindName("Search");

            string sortTag = "";
            foreach (string sortPease in SortTagCollection) { sortTag += sortPease; }

            foreach (Note note in _viewModel.NoteList)
            {
                // Formatが違ったら除外
                if (note.Format != SortedNoteFormat) continue;

                // Textが含まれなかったら除外
                string text ="";
                foreach (string textPease in note.Values) { text += textPease; }
                if (!text.Contains(SortContent) && SearchTextBox.IsTextInput) continue;

                // Tagが含まれなかったら除外
                string noteTag ="";
                foreach (string tagPease in note.Tags) { noteTag += tagPease; }
                if (!noteTag.Contains(sortTag)) continue;

                _viewModel.SortedNotes.Add(note);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SortNote();
        }
        #endregion
    }
}
