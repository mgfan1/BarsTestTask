using Bars.Business;
using Bars.ViewModels;
using DevExpress.Xpf.Core;

namespace Bars
{
    public partial class MainWindow : ThemedWindow
    {
        internal ContractsViewModel ViewModel { get; }
        public MainWindow()
        {
            InitializeComponent();
            BusinessComponentsRegistrator.Register();
            ViewModel = new ContractsViewModel();
            DataContext = ViewModel;
        }
    }
}