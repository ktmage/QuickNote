using QuickNote.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace QuickNote.View.UserControls
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteControllerView : UserControl
    {
        public NoteControllerView(NoteControllerViewModel viewModel , StackPanel stackPanel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Scroll.Content = stackPanel;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var vm = (NoteControllerViewModel)DataContext;
            double res = Math.Round(Scroll.VerticalOffset / Scroll.ScrollableHeight, 1, MidpointRounding.AwayFromZero);
            vm.UpperShadowOpacity = res - 0.6;
            vm.LowerShadowOpacity = 1 - res - 0.6;
        }
    }
}
