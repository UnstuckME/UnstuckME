using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckMeLoggers
{
    internal class UnstuckMEUserEndServerErrLogger
    {
        string filePath = "./UserGuiServerErrors.txt";
        string errTypeStartMark = "<UserServerConnectionError>";
        string errTypeEndMark = "<UserServerConnectionError/>";
        string errorDesc = "No Desc Transmitted";

        public void WriteError(string err)
        {
            if (err != null)
            {
                errorDesc = err;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                file.WriteLine(errTypeStartMark);
                file.WriteLine("    " + DateTime.Now.ToString());
                file.WriteLine("        " + errorDesc);
                file.WriteLine(errTypeEndMark);
            }
        }
    }
}
