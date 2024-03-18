using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace QuickNote.Util.CustomControl
{
    public class TagButton : Button
    {
        public static readonly DependencyProperty TagNameProperty =
        DependencyProperty.Register(
            "TagName",
            typeof(string),
            typeof(TagButton),
            new PropertyMetadata()
            );

        public string TagName
        {
            get { return (string)GetValue(TagNameProperty); }
            set { SetValue(TagNameProperty, value); }
        }

        public TagButton()
        {
            
        }
    }
}
