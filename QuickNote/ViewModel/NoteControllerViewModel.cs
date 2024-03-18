using QuickNote.View.UserControls;
using QuickNote.View.UserControls.NoteFormatTool;
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
using System.Windows.Input;
using QuickNote.Util;

namespace QuickNote.ViewModel
{
    public class NoteControllerViewModel : INotifyPropertyChanged
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
        private NoteControllerView _view;
        private List<string> _contentList;
        private List<string> _contentKeyList;
        private ObservableCollection<string> _inputTagCollection;
        private double _upperShadowOpacity;
        private double _lowerShadowOpacity;
        #endregion

        #region Property
        public NoteControllerView View
        {
            get => _view;
            set => _view = value;
        }
        public List<string> ContentList
        {
            get => _contentList;
            set
            {
                _contentList = value;
                NotifyPropertyChanged(nameof(ContentList));
            }
        }
        public List<string> ContentKeyList
        {
            get => _contentKeyList;
            set
            {
                _contentKeyList = value;
                NotifyPropertyChanged(nameof(ContentKeyList));
            }
        }
        public ObservableCollection<string> InputTagCollection
        {
            get => _inputTagCollection;
            set
            {
                _inputTagCollection = value;
                NotifyPropertyChanged(nameof(InputTagCollection));
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
        public double UpperShadowOpacity
        {
            get => _upperShadowOpacity;
            set
            {
                _upperShadowOpacity = value;
                NotifyPropertyChanged(nameof(UpperShadowOpacity));
            }
        }
        #endregion

        #region Command
        public ICommand RemoveTagCommand { get; }
        #endregion

        #region Constructor
        public NoteControllerViewModel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            RemoveTagCommand = new RelayCommand(RemoveTag);

            _inputTagCollection = new ObservableCollection<string>();
            _contentList = new List<string>();
            _contentKeyList = new List<string>();
            SetNoteForm();
        }
        #endregion

        #region Method
        private void RemoveTag(object obj)
        {
            var tmp = (string)obj;
            if (obj != null)
            {
                _viewModel.AllTagCollection.Add(tmp);
                InputTagCollection.Remove(tmp);

                // 整列
                _viewModel.TagSearchViewModel.SearchTag(null);
            }
        }
        public void SetNoteForm()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("NoteFormat.xml");
            XmlNodeList elements = doc.SelectNodes($"/NoteFormat/{_viewModel.SelectedWriteNoteFormat}/*");

            ContentList.Clear();
            ContentKeyList.Clear();

            StackPanel stackPanel = new StackPanel() { VerticalAlignment = VerticalAlignment.Center };
            stackPanel.Children.Add(new Frame() { Height = 20 });

            int Index = 0;
            foreach (XmlElement element in elements)
            {
                ContentList.Add(element.Name);
                ContentKeyList.Add(element.Name);
                NormalForm defaultForm = new NormalForm(40 * int.Parse(element.GetAttribute("Size")), new Thickness(20, 10, 20, 10), new Binding($"ContentList[{Index}]"), element.Name);
                stackPanel.Children.Add(defaultForm);
                Index++;
            }

            TagForm tagForm = new TagForm(this, 40, new Thickness(20, 10, 20, 10));
            stackPanel.Children.Add(tagForm);

            stackPanel.Children.Add(new Frame() { Height = 20 });

            View = new NoteControllerView(this, stackPanel);
        }
        #endregion
    }
}
