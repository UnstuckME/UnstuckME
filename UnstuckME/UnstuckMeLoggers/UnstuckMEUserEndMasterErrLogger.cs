using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMeLoggers
{
    public enum ERR_TYPES
    {
        USER_GUI_LOGIN,
        USER_GUI_LOGOUT,
        USER_SERVER_CONNECTION_ERROR,
        USER_GUI_INTERACTION_ERROR
    }
    public class UnstuckMEUserEndMasterErrLogger
    {
        string filePath = "./UserGuiAllErrors.txt";
        string errorDesc = "No Desc Transmitted";

        public void WriteError(ERR_TYPES err_type, string err)
        {
            if (err != null)
            {
                errorDesc = err;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                string errTypeStartMark = "<UserServerConnectionError>";
                string errTypeEndMark = "<UserServerConnectionError/>";

                switch (err_type)
                {                    
                    case (ERR_TYPES.USER_SERVER_CONNECTION_ERROR):
                        // write to the master log file
                        errTypeStartMark = "<UserServerConnectionError>";
                        errTypeEndMark = "<UserServerConnectionError/>";
                        file.WriteLine(errTypeStartMark);
                        file.WriteLine("    " + DateTime.Now.ToString());
                        file.WriteLine("        " + errorDesc);
                        file.WriteLine(errTypeEndMark);
                        // write to old file type
                        // probably not needed at all but im leaving it for now
                        UnstuckMEUserEndServerErrLogger log = new UnstuckMEUserEndServerErrLogger();
                        log.WriteError(err);
                        break;
                    case (ERR_TYPES.USER_GUI_INTERACTION_ERROR):
                        errTypeStartMark = "<UserGUIInteractionError>";
                        errTypeEndMark = "<UserGUIInteractionError/>";
                        file.WriteLine(errTypeStartMark);
                        file.WriteLine("    " + DateTime.Now.ToString());
                        file.WriteLine("        " + errorDesc);
                        file.WriteLine(errTypeEndMark);
                        break;
                    case (ERR_TYPES.USER_GUI_LOGIN):
                        errTypeStartMark = "<UserGUILogin>";
                        errTypeEndMark = "<UserGUILogin/>";
                        file.WriteLine(errTypeStartMark);
                        file.WriteLine("    " + DateTime.Now.ToString());
                        file.WriteLine("        " + errorDesc + "Logged In");
                        file.WriteLine(errTypeEndMark);
                        break;
                    case (ERR_TYPES.USER_GUI_LOGOUT):
                        errTypeStartMark = "<UserGUILogOut>";
                        errTypeEndMark = "<UserGUILogOut/>";
                        file.WriteLine(errTypeStartMark);
                        file.WriteLine("    " + DateTime.Now.ToString());
                        file.WriteLine("        " + errorDesc + "Logged Out");
                        file.WriteLine(errTypeEndMark);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
