using System;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class CSVUserParser : CSVReader<User>
    {
        public CSVUserParser(string filename, char seperator) : base(filename, seperator)
        {
        }

        /// <summary>
        /// parss a CSV to be used in the user lidt, does this based on a filename a seperator and a list of lists
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public override List<User> ParseCSV(List<List<string>> saa)
        {
            List<User> users = new List<User>();
            foreach(List<string> singleLine in saa)
            {
                try
                {
                    User user = new User(int.Parse(singleLine[0]), singleLine[1], singleLine[2], singleLine[3], singleLine[5], Convert.ToDecimal(singleLine[4]));
                    users.Add(user);
                }
                catch(FormatException e)
                {

                }
            }
            return users;

        }
    }
}
