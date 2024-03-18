using QuickNote.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using QuickNote.View.UserControls;
using System.Windows.Input;
using System.Diagnostics;
using QuickNote.Util;
using QuickNote.Model;
using System.Threading.Channels;
using System.Collections.ObjectModel;
using System.Windows;
using QuickNote.View.UserControls.NoteFormatTool;
using System.Xml;
using QuickNote.Util.CustomControl;
using System.Windows.Data;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace QuickNote.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
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

        #region Fields
        private MainWindow _mainWindow;

        private WriteNoteFormatSelectorViewModel _writeNoteFormatSelectorViewModel;
        private NoteControllerViewModel _noteControllerViewModel;
        private TagSelectorViewModel _tagSelectorViewModel;
        private SendButtonViewModel _sendButtonViewModel;
        private TagSearchViewModel _tagSearchViewModel;
        private ModeSelectorViewModel _modeSelectorViewModel;
        
        private NotePresenterViewModel _notePresenterViewModel;
        private NoteSelectorViewModel _noteSelectorViewModel;

        private UserControl _topContent;
        private UserControl _leftUpperContent;
        private UserControl _leftLowerContent;
        private UserControl _middleUpperContent;
        private UserControl _middleLowerContent;
        private UserControl _rightContent;

        private Setting _setting;
        private List<Note> _noteList;
        #endregion

        #region Properties
        public MainWindow MainWindow 
        {
            get => _mainWindow;
            set => _mainWindow = value;
        }

        #region ViewModel
        public NoteControllerViewModel NoteControllerViewModel
        {
            get => _noteControllerViewModel;
        }
        public TagSearchViewModel TagSearchViewModel
        {
            get => _tagSearchViewModel;
        }
        public TagSelectorViewModel TagSelectorViewModel
        {
            get => _tagSelectorViewModel;
        }
        public WriteNoteFormatSelectorViewModel WriteNoteFormatSelectorViewModel
        {
            get => _writeNoteFormatSelectorViewModel;
        }
        public SendButtonViewModel SendButtonViewModel
        {
            get=> _sendButtonViewModel;
        }
        public ModeSelectorViewModel ModeSelectorViewModel
        {
            get => _modeSelectorViewModel;
        }
        public NotePresenterViewModel NotePresenterViewModel
        {
            get => _notePresenterViewModel;
        }
        public NoteSelectorViewModel NoteSelectorViewModel
        {
            get => _noteSelectorViewModel;
        }
        #endregion

        #region Grids
        public UserControl TopContent
        {
            get => _topContent;
            set { _topContent = value; NotifyPropertyChanged(nameof(TopContent)); }
        }
        public UserControl LeftUpperContent
        {
            get => _leftUpperContent;
            set { _leftUpperContent = value; NotifyPropertyChanged(nameof(LeftUpperContent)); }
        }
        public UserControl LeftLowerContent
        {
            get => _leftLowerContent;
            set { _leftLowerContent = value; NotifyPropertyChanged(nameof(LeftLowerContent)); }
        }
        public UserControl MiddleUpperContent
        {
            get => _middleUpperContent;
            set { _middleUpperContent = value; NotifyPropertyChanged(nameof(MiddleUpperContent)); }
        }
        public UserControl MiddleLowerContent
        {
            get => _middleLowerContent;
            set { _middleLowerContent = value; NotifyPropertyChanged(nameof(MiddleLowerContent)); }
        }
        public UserControl RightContent
        {
            get => _rightContent;
            set { _rightContent = value; NotifyPropertyChanged(nameof(RightContent)); }
        }
        #endregion

        #region NoteController Property

        public List<string> ContentList
        {
            get => _noteControllerViewModel.ContentList;
            set { _noteControllerViewModel.ContentList = value; NotifyPropertyChanged(nameof(ContentList)); }
        }
        public List<string> ContentKeyList
        {
            get => _noteControllerViewModel.ContentKeyList;
            set { _noteControllerViewModel.ContentKeyList = value; NotifyPropertyChanged(nameof(ContentKeyList)); }
        }
        public ObservableCollection<string> InputTagCollection 
        { 
            get => _noteControllerViewModel.InputTagCollection; 
            set{ _noteControllerViewModel.InputTagCollection = value; NotifyPropertyChanged(nameof(InputTagCollection)); } 
        }
        #endregion

        #region TagSelector Property
        public ObservableCollection<string> AllTagCollection
        {
            get => _tagSelectorViewModel.AllTagCollection;
            set {_tagSelectorViewModel.AllTagCollection = value; NotifyPropertyChanged(nameof(AllTagCollection)); }
        }
        #endregion

        #region NoteFormatSelector Property
        public ObservableCollection<string> NoteFormatCollection 
        {
            get => _writeNoteFormatSelectorViewModel.NoteFormatCollection;
            set { _writeNoteFormatSelectorViewModel.NoteFormatCollection = value;NotifyPropertyChanged(nameof(NoteFormatCollection)); }
        }
        public string SelectedWriteNoteFormat 
        {
            get => _writeNoteFormatSelectorViewModel.SelectedWriteNoteFormat;
            set {
                _writeNoteFormatSelectorViewModel.SelectedWriteNoteFormat = value;
                NotifyPropertyChanged(nameof(SelectedWriteNoteFormat));

                //InputTagCollection.Clear();
                //AllTagCollection.Clear();
                //_tagSelectorViewModel.InitializeTag();

                //_noteControllerViewModel.SetNoteForm();
                //LeftUpperContent = _noteControllerViewModel.View;
            }
        }
        #endregion

        #region NotePresenter Property
        public ObservableCollection<Note> SortedNotes
        {
            get => _notePresenterViewModel.SortedNotes;
            set 
            { 
                _notePresenterViewModel.SortedNotes = value; 
                NotifyPropertyChanged(nameof(SortedNotes)); 
            }
        }
        #endregion

        #region NoteSelector Property
        public ObservableCollection<string> SortTagCollection
        {
            get => _noteSelectorViewModel.SortTagCollection;
            set
            {
                _noteSelectorViewModel.SortTagCollection = value;
                NotifyPropertyChanged(nameof(SortTagCollection));
            }
        }
        #endregion

        #region ModeSelector
        public string Mode
        {
            get => _modeSelectorViewModel.Mode;
        }
        #endregion

        public Setting Setting 
        { 
            get => _setting;
            set => _setting = value; 
        }
        public List<Note> NoteList
        { 
            get => _noteList;
            set { _noteList = value; NotifyPropertyChanged(nameof(NoteList)); }
        }

        #endregion

        #region Commands
        public ICommand DebugCommand { get; }
        #endregion

        #region Constructor
        public ViewModel() 
        {
            _setting = (Setting)XMLController.Load(typeof(Setting), "setting.xml");
            _noteList = (List<Note>)XMLController.Load(typeof(List<Note>), "NoteList.xml");

            _writeNoteFormatSelectorViewModel = new WriteNoteFormatSelectorViewModel(this);
            _noteControllerViewModel = new NoteControllerViewModel(this);
            _tagSelectorViewModel = new TagSelectorViewModel(this);
            _sendButtonViewModel = new SendButtonViewModel(this);
            _tagSearchViewModel = new TagSearchViewModel(this);
            _modeSelectorViewModel = new ModeSelectorViewModel(this);

            _notePresenterViewModel = new NotePresenterViewModel(this);
            _noteSelectorViewModel = new NoteSelectorViewModel(this);

            DebugCommand = new RelayCommand(Debug);

            SetupWindow();
            _modeSelectorViewModel.SetMode(Setting.DefaultMode);
        }
        #endregion

        #region Method
        private void SetupWindow()
        {
            MainWindow = new MainWindow(this);
            MainWindow.Closing += CloseWindowEvent;
            MainWindow.Show();
        }
        private void CloseWindowEvent(object sender, CancelEventArgs e)
        {
            XMLController.Save(Setting, typeof(Setting), "setting.xml");
        }
        public void InitializeGridContent()
        {
            TopContent = null;
            LeftLowerContent = null;
            LeftUpperContent = null;
            MiddleLowerContent = null;
            MiddleUpperContent = null;
            RightContent = null;
        }

        #endregion

        #region Debug
        private void Debug(object obj)
        {
            
        }
        #endregion
    }
}
