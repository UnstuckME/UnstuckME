using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        string filePath = "./UserGuiAllErrors.txt";
        string errorDesc = "No Desc Transmitted";
        List<ErrContainer> ErrorsList = new List<ErrContainer>();
        static UnstuckMEUserEndMasterErrLogger _instance = null;
        private UnstuckMEUserEndMasterErrLogger()
        {

        }
        public static UnstuckMEUserEndMasterErrLogger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UnstuckMEUserEndMasterErrLogger();
            }
            return _instance;
        }

        public void WriteError(ERR_TYPES err_type, string err, string additionalInfo = null)
        {
            if (err != null)
            {
                errorDesc = err;
            }
            AddErrorsToList(err_type, err, additionalInfo);
            if (ErrorsList.Count > 10)
            {
                outputErrors();
            }         
        }
        private void outputErrors()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                foreach (ErrContainer errors in ErrorsList)
                {
                    file.WriteLine(errors.ToString());
                }
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
                System.IO.FileInfo fInfo = new System.IO.FileInfo(filePath);
                if (fInfo.Length > 10000) //bytes
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, false))
                        {
                            file.WriteLine("<FileLastReset=" + DateTime.Now.ToString() + "/>");
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void AddErrorsToList(ERR_TYPES errType, string errMsg, string additionalInfo = null)
        {
            string errTypeStartMark = "<UserServerConnectionError>";
            string errTypeEndMark = "<UserServerConnectionError/>";
            switch (errType)
            {
                case (ERR_TYPES.USER_SERVER_CONNECTION_ERROR):
                    // write to the master log file
                    errTypeStartMark = "<UserServerConnectionError>";
                    errTypeEndMark = "<UserServerConnectionError/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));                    
                    break;
                case (ERR_TYPES.USER_GUI_INTERACTION_ERROR):
                    errTypeStartMark = "<UserGUIInteractionError>";
                    errTypeEndMark = "<UserGUIInteractionError/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES.USER_GUI_LOGIN):
                    errTypeStartMark = "<UserGUILogin>";
                    errTypeEndMark = "<UserGUILogin/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES.USER_GUI_LOGOUT):
                    errTypeStartMark = "<UserGUILogOut>";
                    errTypeEndMark = "<UserGUILogOut/>";
                    ErrorsList.Add(new ErrContainer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES.USER_UNABLE_TO_READWRITE):
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
            outputErrors();
        }
    }
}
