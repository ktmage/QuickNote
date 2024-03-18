using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using QuickNote.Util;
using QuickNote.View.UserControls;

namespace QuickNote.Model
{
    public class SendButtonViewModel:INotifyPropertyChanged
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
        private SendButtonView _view;
        #endregion

        #region Property
        public SendButtonView View
        {
            get => _view;
            set => _view = value;
        }
        #endregion

        #region Command
        public ICommand SendNoteCommand { get; }
        #endregion

        #region Constructor
        public SendButtonViewModel(ViewModel.ViewModel viewModel)
        {
            _viewModel= viewModel;
            SendNoteCommand = new RelayCommand(SendNote);
            _view = new SendButtonView(this);
        }
        #endregion

        #region Method
        private void SendNote(object obj)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("NoteFormat.xml");
            XmlNodeList elements = doc.SelectNodes($"/NoteFormat/{_viewModel.SelectedWriteNoteFormat}/*");

            string topViewContent = "";

            for (int i = 0;i < elements.Count; i++)
            {
                var element = (XmlElement)elements[i];
                if (element.GetAttribute("TopView") == "True") topViewContent = _viewModel.ContentList[i]; 
            }

            _viewModel.NoteList.Add(
                new Note()
                {
                    Format = new string(_viewModel.SelectedWriteNoteFormat),
                    Keys=  new List<string>(_viewModel.ContentKeyList),
                    Values = new List<string>(_viewModel.ContentList),
                    Tags = new List<string>(_viewModel.InputTagCollection),
                    TopView = topViewContent,
                }
                );
            XMLController.Save(_viewModel.NoteList, _viewModel.NoteList.GetType(), "NoteList.xml");
            //_viewModel.NoteList = (List<Note>)XMLController.Load(typeof(List<Note>), "NoteList.xml");
        }
        #endregion
    }
}
