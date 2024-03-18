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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickNote.View.UserControls
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class NotePresenterView : UserControl
    {
        public NotePresenterView(NotePresenterViewModel viewModel)
        {
            InitializeComponent();
            DataContext= viewModel;
        }

        private void PageScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var vm = (NotePresenterViewModel)DataContext;
            double res = Math.Round(PageScroll.VerticalOffset / PageScroll.ScrollableHeight, 1, MidpointRounding.AwayFromZero);
            vm.UpperShadowOpacity = res - 0.6;
            vm.LowerShadowOpacity = 1 - res - 0.6;
        }
    }
}
