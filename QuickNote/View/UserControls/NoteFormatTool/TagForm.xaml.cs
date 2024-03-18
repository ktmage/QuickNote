using QuickNote.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class TagForm : UserControl
    {
        public bool IsSetShowTagButton;
        public TagForm(NoteControllerViewModel viewModel, int height, Thickness thickness)
        {
            InitializeComponent();
            DataContext = viewModel;

            IsSetShowTagButton = false;
            MainGrid.Height = height;
            MainGrid.Margin = thickness;
        }

        private void MainWrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {


            WrapPanel wrapPanel = sender as WrapPanel;
            double AllTagWidth = 0;

            foreach (UIElement element in wrapPanel.Children)
            {
                AllTagWidth += element.DesiredSize.Width;
            }

            if (AllTagWidth <= wrapPanel.DesiredSize.Width)
            {
                // ここに詳細表示ボタンを消す処理を書く。
                IsSetShowTagButton = false;
            }
            if (AllTagWidth > wrapPanel.DesiredSize.Width && !IsSetShowTagButton)
            {
                // ここに詳細表示ボタンを表示する処理を書く。
                Trace.WriteLine("溢れましたよ。");
                IsSetShowTagButton = true;
            } 
        }
    }
}
