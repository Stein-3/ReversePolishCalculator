using System.Windows;
using Unity;
using Calculator;

namespace RPNCalculator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IUnityContainer _container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _container.RegisterFactory<ICalculator>(fuctory => CalculatorFactory.CreateCalculator());
            _container.RegisterType<RPNViewModel>();
            _container.RegisterType<MainWindow>();

            MainWindow = _container.Resolve<MainWindow>();
            MainWindow.Show();
        }
    }
}
