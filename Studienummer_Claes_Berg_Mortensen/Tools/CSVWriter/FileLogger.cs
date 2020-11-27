using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studienummer_Claes_Berg_Mortensen.Tools.CSVWriter
{
    public static class FileLogger
    {
        /// <summary>
        /// writes to a file based on filename and the string it is given to write
        /// </summary>
        /// <param name="lineToWrite"></param>
        /// <param name="filename"></param>
        public static void WriteToLogFile(string lineToWrite, string filename)

        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Append)))
            {
                sw.WriteLine(lineToWrite); 
            }
        }
    }
}
