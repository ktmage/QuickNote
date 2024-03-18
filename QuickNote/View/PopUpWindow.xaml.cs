using QuickNote.Model;
using QuickNote.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace QuickNote.View
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class PopUpWindow : Window
    {
        public PopUpWindow(Note note)
        {
            InitializeComponent();
            DataContext= note;

            SetNote(note);
        }

        private void SetNote(Note note)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("NoteFormat.xml");
            XmlNodeList elements = doc.SelectNodes($"/NoteFormat/{note.Format}/*");


            MainStack.Children.Add( new Frame() { Height = 40 } );

            int Index = 0;
            foreach( XmlElement element in elements)
            {
                MainStack.Children.Add( new TextBox() {
                    Style = (Style)FindResource("RoundCornerTextBoxStyle"),
                    Height = 40 * int.Parse(element.GetAttribute("Size")),
                    Margin = new Thickness(20,10,20,10),
                    Text = note.Values[Index], 
                } );
                Index++;
            }

            MainStack.Children.Add( new Frame() { Height = 40 } );
        }
    }
}
