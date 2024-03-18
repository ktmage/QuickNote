using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace QuickNote.Util.CustomControl
{
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                "Placeholder",
                typeof(string),
                typeof(PlaceholderTextBox),
                new PropertyMetadata()
                );

        public bool IsTextInput;

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public PlaceholderTextBox()
        {
            StartUp();
            this.GotFocus += _GotFocus;
            this.LostFocus += _LostFocus;
        }

        private void _GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.IsTextInput == false)
            {
                this.Text = string.Empty;
                this.Foreground = Pallet.SubColor_6;
            }
        }
        private void _LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text == string.Empty)
            {
                this.Text = this.Placeholder;
                this.Foreground = Pallet.MainColor_6;
                this.IsTextInput = false;
                return;
            }
            this.IsTextInput = true;
        }

        private void StartUp()
        {
            if (this.Text == string.Empty)
            {
                this.Text = this.Placeholder;
                this.Foreground = Pallet.MainColor_6;
                this.IsTextInput = false;
                return;
            }
            this.IsTextInput = true;
        }
    }
}
