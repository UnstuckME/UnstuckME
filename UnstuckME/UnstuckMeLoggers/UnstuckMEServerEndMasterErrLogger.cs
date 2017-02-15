using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnstuckMeLoggers
{

    class UnstuckMEServerEndMasterErrLogger
    {
        string filePath = "./ServerAllErrors.txt";
        string errorDesc = "No Desc Transmitted";
        List<ErrContainerServer> ErrorsList = new List<ErrContainerServer>();
        static UnstuckMEServerEndMasterErrLogger _instance = null;
        private UnstuckMEServerEndMasterErrLogger()
        {

        }
        public static UnstuckMEServerEndMasterErrLogger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UnstuckMEServerEndMasterErrLogger();
            }
            return _instance;
        }

        public void WriteError(ERR_TYPES_SERVER err_type, string err, string additionalInfo = null)
        {
            if (err != null)
            {
                errorDesc = err;
            }
            AddErrorsToList(err_type, err, additionalInfo);
            if (ErrorsList.Count > 30)
            {
                outputErrors();
            }


        }
        private void outputErrors()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                foreach (ErrContainerServer errors in ErrorsList)
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
                if (fInfo.Length > 100000) //bytes
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
        private void AddErrorsToList(ERR_TYPES_SERVER errType, string errMsg, string additionalInfo = null)
        {
            string errTypeStartMark;
            string errTypeEndMark;
            switch (errType)
            {
                case (ERR_TYPES_SERVER.DATABASE_CONNECTION_ERROR):
                    // write to the master log file
                    errTypeStartMark = "<DatabaseConnectionError>";
                    errTypeEndMark = "<DatabaseConnectionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.DATABASE_RETURN_ERROR):
                    errTypeStartMark = "<DatabaseReturnError>";
                    errTypeEndMark = "<DatabaseReturnError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_CONNECTION_ERROR):
                    errTypeStartMark = "<ServerConnectionError>";
                    errTypeEndMark = "<ServerConnectionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_GUI_INTERACTION_ERROR):
                    errTypeStartMark = "<ServerGUIInteractionError>";
                    errTypeEndMark = "<ServerGUIInteractionError/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_GUI_LOGIN):
                    errTypeStartMark = "<ServerGUILogin>";
                    errTypeEndMark = "<ServerGUILogin/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_GUI_LOGOUT):
                    errTypeStartMark = "<ServerGUILogOut>";
                    errTypeEndMark = "<ServerGUILogOut/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_START):
                    errTypeStartMark = "<ServerStart>";
                    errTypeEndMark = "<ServerStart/>";
                    ErrorsList.Add(new ErrContainerServer(errMsg, errTypeStartMark, errTypeEndMark, DateTime.Now.ToString(), additionalInfo));
                    break;
                case (ERR_TYPES_SERVER.SERVER_STOP):
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
            outputErrors();
        }
    }
}

