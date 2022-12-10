using MvvmPrism.ViewModels.Services;
using MvvmPrism.ViewModels.ViewModels;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace MvvmPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<IBookService, BookService>();
        }

        protected override Window CreateShell()
        {
            MainWindow window = Container.Resolve<MainWindow>();
            return window;
        }
    }
}
