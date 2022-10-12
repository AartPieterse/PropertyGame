using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Windows_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception occured: " + e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
