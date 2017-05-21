using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // this catches unhandled errors and makes sure they are logged, then it gracefully closes the program.
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var trace = new StackTrace(e.Exception, true).GetFrame(0).GetMethod();
            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, e.Exception.Message, "A fatal error occured and was not handled, Source = " + trace.Name);
            UnstuckMEUserEndMasterErrLogger.GetInstance().ForceWrite();
            e.Handled = true;
            Current.Shutdown();
        }
    }
}
