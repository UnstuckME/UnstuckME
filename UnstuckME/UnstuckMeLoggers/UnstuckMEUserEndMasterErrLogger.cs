using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace UnstuckMeLoggers
{
    public enum ERR_TYPES
    {
        // if you add one of these please add it in the switch statement to be handled
        USER_GUI_LOGIN,
        USER_GUI_LOGOUT,
        USER_SERVER_CONNECTION_ERROR,
        USER_GUI_INTERACTION_ERROR,
        USER_UNABLE_TO_READWRITE
    }
    public class UnstuckMEUserEndMasterErrLogger
    {
        private const string filePath = "./UserGuiAllErrors.txt";
        private string errorDesc = "No Desc Transmitted";
        private List<ErrContainer> ErrorsList = new List<ErrContainer>();
        private static UnstuckMEUserEndMasterErrLogger _instance;

        private UnstuckMEUserEndMasterErrLogger()
        { }

        public static UnstuckMEUserEndMasterErrLogger GetInstance()
        {
            return _instance ?? (_instance = new UnstuckMEUserEndMasterErrLogger());
        }

        public void WriteError(ERR_TYPES err_type, string err, string additionalInfo = null)
        {
            if (err != null)
                errorDesc = err;

            AddErrorsToList(err_type, err, additionalInfo);

            if (ErrorsList.Count > 10)
                OutputErrors();
        }

        public void ForceWrite()
        {
            OutputErrors();
        }

        private void OutputErrors()
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                foreach (ErrContainer errors in ErrorsList)
                    file.WriteLine(errors.ToString());

                ErrorsList.Clear();
            }
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                MaintainFile();
            }).Start();
        }
        private void MaintainFile()
        {
            try
            {
                FileInfo fInfo = new FileInfo(filePath);

                if (fInfo.Length > 10000) //bytes
                {
                    using (StreamWriter file = new StreamWriter(filePath, false))
                    {
                        file.WriteLine("<FileLastReset=" + DateTime.Now + "/>");
                    }
                }
            }
            catch (Exception)
            { }
        }
        private void AddErrorsToList(ERR_TYPES errType, string errMsg, string additionalInfo = null)
        {
            string errTypeStartMark;
            string errTypeEndMark;

            switch (errType)
            {
                case ERR_TYPES.USER_SERVER_CONNECTION_ERROR:
                    // write to the master log file
                    errTypeStartMark = "<UserServerConnectionError>";
                    errTypeEndMark = "<UserServerConnectionError/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));                    
                    break;
                case ERR_TYPES.USER_GUI_INTERACTION_ERROR:
                    errTypeStartMark = "<UserGUIInteractionError>";
                    errTypeEndMark = "<UserGUIInteractionError/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES.USER_GUI_LOGIN:
                    errTypeStartMark = "<UserGUILogin>";
                    errTypeEndMark = "<UserGUILogin/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES.USER_GUI_LOGOUT:
                    errTypeStartMark = "<UserGUILogOut>";
                    errTypeEndMark = "<UserGUILogOut/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES.USER_UNABLE_TO_READWRITE:
                    errTypeStartMark = "<UserUnableToReadOrWrite>";
                    errTypeEndMark = "<UserUnableToReadOrWrite/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                default:
                    break;
            }
        }

        // this is a c# destructor aka a finalize method... its scary but its fine
        ~UnstuckMEUserEndMasterErrLogger()
        {
            OutputErrors();
        }
    }
}
