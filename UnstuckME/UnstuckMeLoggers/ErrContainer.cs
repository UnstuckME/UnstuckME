using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMeLoggers
{
    internal class ErrContainer
    {
        string i_ErrorMsg;
        string i_ErrTypeStartMark;
        string i_ErrTypeEndMark;
        string i_ErrorTime;

        internal ErrContainer(string ErrorMsg, string ErrTypeStartMark, string ErrTypeEndMark, string ErrorTime)
        {
            i_ErrorMsg = ErrorMsg;
            i_ErrTypeStartMark = ErrTypeStartMark;
            i_ErrTypeEndMark = ErrTypeEndMark;
            i_ErrorTime = ErrorTime;
        }
        public override string ToString()
        {
            string temp = i_ErrTypeStartMark + Environment.NewLine + "\t" + "<ErrMsg=" + i_ErrorMsg + "/>" +
                Environment.NewLine + "\t" + "<ErrTime=" + i_ErrorTime + "/>" + Environment.NewLine + i_ErrTypeEndMark;
            return temp;
        }
    }
}
