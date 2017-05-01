using System;
using System.IO;

namespace UnstuckMeLoggers
{
    internal class UnstuckMEUserEndServerErrLogger
    {
        private string filePath = "./UserGuiServerErrors.txt";
        private string errTypeStartMark = "<UserServerConnectionError>";
        private string errTypeEndMark = "<UserServerConnectionError/>";
        private string errorDesc = "No Desc Transmitted";

        public void WriteError(string err)
        {
            if (err != null)
                errorDesc = err;

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(errTypeStartMark);
                file.WriteLine("    " + DateTime.Now);
                file.WriteLine("        " + errorDesc);
                file.WriteLine(errTypeEndMark);
            }
        }
    }
}
