using System.Windows;

namespace RPNCalculator.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow(RPNViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
