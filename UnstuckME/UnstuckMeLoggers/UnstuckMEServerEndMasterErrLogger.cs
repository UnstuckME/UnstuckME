using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace UnstuckMeLoggers
{
    public class UnstuckMEServerEndMasterErrLogger
    {
        private string filePath = "./ServerAllErrors.txt";
        private string errorDesc = "No Desc Transmitted";
        private List<ErrContainerServer> ErrorsList = new List<ErrContainerServer>();
        private static UnstuckMEServerEndMasterErrLogger _instance;

        private UnstuckMEServerEndMasterErrLogger()
        { }

        public static UnstuckMEServerEndMasterErrLogger GetInstance()
        {
            if (_instance == null)
                _instance = new UnstuckMEServerEndMasterErrLogger();

            return _instance;
        }

        public void WriteError(ERR_TYPES_SERVER err_type, string err, string additionalInfo = null)
        {
            if (err != null)
                errorDesc = err;

            AddErrorsToList(err_type, err, additionalInfo);

            if (ErrorsList.Count > 30)
                OutputErrors();
        }
        private void OutputErrors()
        {
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                foreach (ErrContainerServer errors in ErrorsList)
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
                if (fInfo.Length > 100000) //bytes
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
        private void AddErrorsToList(ERR_TYPES_SERVER errType, string errMsg, string additionalInfo = null)
        {
            string errTypeStartMark;
            string errTypeEndMark;

            switch (errType)
            {
                case ERR_TYPES_SERVER.DATABASE_CONNECTION_ERROR:
                    // write to the master log file
                    errTypeStartMark = "<DatabaseConnectionError>";
                    errTypeEndMark = "<DatabaseConnectionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.DATABASE_RETURN_ERROR:
                    errTypeStartMark = "<DatabaseReturnError>";
                    errTypeEndMark = "<DatabaseReturnError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_CONNECTION_ERROR:
                    errTypeStartMark = "<ServerConnectionError>";
                    errTypeEndMark = "<ServerConnectionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_GUI_INTERACTION_ERROR:
                    errTypeStartMark = "<ServerGUIInteractionError>";
                    errTypeEndMark = "<ServerGUIInteractionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_GUI_LOGIN:
                    errTypeStartMark = "<ServerGUILogin>";
                    errTypeEndMark = "<ServerGUILogin/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_GUI_LOGOUT:
                    errTypeStartMark = "<ServerGUILogOut>";
                    errTypeEndMark = "<ServerGUILogOut/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_START:
                    errTypeStartMark = "<ServerStart>";
                    errTypeEndMark = "<ServerStart/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case ERR_TYPES_SERVER.SERVER_STOP:
                    errTypeStartMark = "<ServerStop>";
                    errTypeEndMark = "<ServerStop/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                default:
                    break;
            }
        }

        // this is a c# destructor aka a finalize method... its scary but its fine
        ~UnstuckMEServerEndMasterErrLogger()
        {
            OutputErrors();
        }
    }
}