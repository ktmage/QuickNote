using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuickNote.Util;
using QuickNote.View.UserControls;

namespace QuickNote.Model
{
    public class TagSelectorViewModel:INotifyPropertyChanged
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
        private ViewModel.ViewModel _viewModel;
        private TagSelectorView _view;
        private ObservableCollection<string> _allTagCollection;
        private double _upperShadowOpacity;
        private double _lowerShadowOpacity;
        #endregion

        #region Property
        public TagSelectorView View
        {
            get => _view;
            set => _view = value;
        }
        public ObservableCollection<string> AllTagCollection
        {
            get => _allTagCollection;
            set 
            {
                _allTagCollection= value;
                NotifyPropertyChanged(nameof(AllTagCollection));
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
        public ICommand SelectTagCommand { get; }
        #endregion

        #region Constructor
        public TagSelectorViewModel(ViewModel.ViewModel viewModel) 
        {
            _viewModel = viewModel;
            SelectTagCommand = new RelayCommand(SelectTag);
            _allTagCollection= new ObservableCollection<string>();
            InitializeTag();
            _view = new TagSelectorView(this);
        }
        #endregion

        #region Method
        public void InitializeTag()
        {
            AllTagCollection.Clear();
            foreach (var content in _viewModel.Setting.TagButtonNameList) { AllTagCollection.Add(content); }
        }

        private void SelectTag(object obj)
        {
            var tmp = (string)obj;
            if (obj != null)
            {
                if (_viewModel.ModeSelectorViewModel.Mode == "Writer") _viewModel.InputTagCollection.Add(tmp);
                if (_viewModel.ModeSelectorViewModel.Mode == "Viewer") _viewModel.SortTagCollection.Add(tmp);
                AllTagCollection.Remove(tmp);
                // 整列
                //SearchTag("");
            }
        }
        #endregion
    }
}
