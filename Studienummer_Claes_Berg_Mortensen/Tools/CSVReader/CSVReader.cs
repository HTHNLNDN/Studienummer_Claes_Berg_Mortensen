﻿using System.Collections.Generic;
using System.IO;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public abstract class CSVReader<T>
    {
        public string Filename { get; set; }
        public char Seperator { get; set; }
        protected CSVReader(string filename, char seperator)
        {
            Filename = filename;
            Seperator = seperator;
        }

        public List<List<string>> ReadAndSeperateCSVFile()
        {
            List<List<string>> CSVFileData = new List<List<string>>();
            
            if (!File.Exists(Filename))
                throw new FileNotFoundException($"File {Filename} not found, check if directory is written correctly ");
            using(StreamReader sr = File.OpenText(Filename))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    s = s.Replace("<h1>", "").Replace("<h2>", "").Replace("<h3>", "").Replace("<b>", "").Replace("<p>", "").Replace("</h1>", "").Replace("</h2>", "").Replace("</h3>", "").Replace("</b>", "").Replace("</p>", "");
                     CSVFileData.Add(new List<string>(s.Split(Seperator)));
                }
            }
            return CSVFileData;

        }
        public abstract List<T> ParseCSV (List<List<string>> saa);

    }
}
