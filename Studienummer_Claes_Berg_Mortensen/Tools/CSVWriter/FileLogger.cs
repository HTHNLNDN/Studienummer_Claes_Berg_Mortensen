using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studienummer_Claes_Berg_Mortensen.Tools.CSVWriter
{
    public static class FileLogger
    {
        
        public static void WriteToLogFile(string lineToWrite, string filename)

        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Append)))
            {
                sw.WriteLine(lineToWrite); 
            }
        }
    }
}
