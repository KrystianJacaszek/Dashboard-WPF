using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    [ContentProperty(Name=nameof(Children))]
    public sealed partial class Widget : UserControl
    {
        public Widget()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(UIElement), typeof(Widget), new PropertyMetadata(typeof(UIElement)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(Widget), new PropertyMetadata(typeof(string)));

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
