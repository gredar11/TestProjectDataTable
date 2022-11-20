using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;

namespace TestProjectDataTable
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace(".ViewModels.", ".CustomNamespace.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register other needed services here
        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            return window;
        }
    }
}