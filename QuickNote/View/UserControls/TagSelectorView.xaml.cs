using QuickNote.Model;
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
    public partial class TagSelectorView : UserControl
    {
        public TagSelectorView(TagSelectorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void TagScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var vm = (TagSelectorViewModel)DataContext;
            double res = Math.Round(TagScroll.VerticalOffset / TagScroll.ScrollableHeight, 1, MidpointRounding.AwayFromZero);
            vm.UpperShadowOpacity = res - 0.6;
            vm.LowerShadowOpacity = 1 - res - 0.6;
        }
    }
}
