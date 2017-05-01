using System;

namespace UnstuckMeLoggers
{
    public enum ERR_TYPES_SERVER
    {
        // if you add one of these please add it in the switch statement to be handled
        SERVER_GUI_LOGIN,
        SERVER_GUI_LOGOUT,
        SERVER_CONNECTION_ERROR,
        SERVER_GUI_INTERACTION_ERROR,
        SERVER_START,
        SERVER_STOP,
        DATABASE_RETURN_ERROR,
        DATABASE_CONNECTION_ERROR
    }
    internal class ErrContainerServer
    {
        string i_ErrorMsg;              // the error message string contained by the thrown exception
        string i_ErrTypeStartMark;      // the xml like tag that defines the start of the error
        string i_ErrTypeEndMark;        // the xml like tag that defines the end of the error
        string i_ErrorTime;             // the string representaition of the time the errors occurance
        string i_AdditionalInfo;        // a string that holds any additional info you would like logged such as what caused the error

        internal ErrContainerServer(string ErrorMsg, string ErrTypeStartMark, string ErrTypeEndMark, string ErrorTime, string AdditionalInfo = null)
        {
            i_ErrorMsg = ErrorMsg;
            i_ErrTypeStartMark = ErrTypeStartMark;
            i_ErrTypeEndMark = ErrTypeEndMark;
            i_ErrorTime = ErrorTime;
            i_AdditionalInfo = AdditionalInfo;
        }
        public override string ToString()
        {
            string temp = i_ErrTypeStartMark + Environment.NewLine + "\t" + "<ErrMsg=" + i_ErrorMsg + "/>";
            if (i_AdditionalInfo != null)
                temp = temp + Environment.NewLine + "<AdditionalInfo=" + i_AdditionalInfo + "/>";

            temp = temp + Environment.NewLine + "\t" + "<ErrTime=" + i_ErrorTime + "/>" + Environment.NewLine + i_ErrTypeEndMark;
            return temp;
        }
    }
}
