﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // this catches unhandled errors and makes sure they are logged, then it gracefully closes the program.
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            
            UnstuckMeLoggers.UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(UnstuckMeLoggers.ERR_TYPES.USER_GUI_INTERACTION_ERROR, e.Exception.Message, "A fatal error occured and was not handled");
            UnstuckMeLoggers.UnstuckMEUserEndMasterErrLogger.GetInstance().ForceWrite();
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
