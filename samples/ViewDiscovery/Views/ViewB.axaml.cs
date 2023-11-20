using Avalonia.Controls;

namespace ViewDiscovery.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewB : UserControl
    {
        public ViewB()
        {
            InitializeComponent();
        }


        //关键点 60  本类创建时会调用此构造函数,而不上面的无参构造函数, 为什么?
        public ViewB(ViewA subView)
        {
            InitializeComponent();
            this.FindControl<ContentControl>("Test").Content = subView;
        }
    }
}
