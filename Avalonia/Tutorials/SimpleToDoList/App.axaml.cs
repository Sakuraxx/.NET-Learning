#region Usings

using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SimpleToDoList.Services;
using SimpleToDoList.ViewModels;
using SimpleToDoList.Views;

#endregion Usings

namespace SimpleToDoList
{
    public partial class App : Application
    {
        // This is a reference to our MainWindowViewModel which we use to save the list on shutdown. You can also use Dependency Injection
        // in your App.
        private readonly MainWindowViewModel _mainViewModel = new MainWindowViewModel();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _mainViewModel
                };

                // Listen to the ShutdownRequested-event
                desktop.ShutdownRequested += DesktopOnShutdownRequested;
            }

            base.OnFrameworkInitializationCompleted();

            // Init the MainViewModel
            await InitMainViewModelAsync();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }

        #region Save and load data

        // We want to save our ToDoList before we actually shutdown the App. As File I/O is async, we need to wait until file is closed
        // before we can actually close this window

        private bool _canClose; // This flag is used to check if window is allowed to close
        private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            e.Cancel = !_canClose; // cancel closing event first time

            if (!_canClose)
            {
                // To save the items, we map them to the ToDoItem-Model which is better suited for I/O operations
                var itemsToSave = _mainViewModel.ToDoItems.Select(item => item.GetToDoItem());
                await ToDoListFileService.SaveToFileAsync(itemsToSave);

                // Set _canClose to true and Close this Window again
                _canClose = true;
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            }
        }

        private async Task InitMainViewModelAsync()
        {
            // get the items to load
            var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();

            if (itemsLoaded is not null)
            {
                foreach (var item in itemsLoaded)
                {
                    //_mainViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
                    //var viewModel = new ToDoItemViewModel(item, _mainViewModel.RemoveItem);
                    //_mainViewModel.ToDoItems.Add(viewModel);
                    _mainViewModel.ToDoItems.Add(new ToDoItemViewModel(item, vm => _mainViewModel.RemoveItemCommand.Execute(vm)));
                }
            }
        }

        #endregion Save and load data
    }
}