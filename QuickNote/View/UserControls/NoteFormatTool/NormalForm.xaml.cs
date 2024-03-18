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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickNote.View.UserControls.NoteFormatTool
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class NormalForm : UserControl
    {
        public NormalForm(int height, Thickness thickness, Binding binding ,string text)
        {
            InitializeComponent();
            //DataContext= viewModel;

            WrapGrid.Height = height;
            WrapGrid.Margin = thickness;
            Form.SetBinding(TextBox.TextProperty, binding);
            Form.Placeholder = text;
        }
    }
}
