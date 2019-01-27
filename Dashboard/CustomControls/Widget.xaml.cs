using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    [ContentProperty(Name=nameof(Children))]
    public sealed partial class Widget : UserControl
    {
        public Widget()
        {
            this.InitializeComponent();
            // this.Children.Add();
        }

        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register(nameof(Children), typeof(UIElement), typeof(Widget), new PropertyMetadata(typeof(UIElement)));
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(Widget), new PropertyMetadata(typeof(string)));

        public UIElement Children
        {
            get { return (UIElement)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
