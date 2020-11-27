using System;
using System.Collections.Generic;

namespace Studienummer_Claes_Berg_Mortensen.Core
{
    public class CSVProductParser : CSVReader<Product>
    {
        public CSVProductParser(string filename, char seperator) : base(filename, seperator)
        {
        }

        /// <summary>
        /// parses a list of lists into a list of products, returns a list of prodcts
        /// </summary>
        /// <param name="saa"></param>
        /// <returns></returns>
        public override List<Product> ParseCSV(List<List<string>> saa)
        {
            List<Product> products = new List<Product>();
            foreach (List<string> singleLine in saa)
            {
                try
                {

                    Product product = new Product(Convert.ToInt32(singleLine[0]), singleLine[1], Convert.ToDecimal(singleLine[2]), Convert.ToInt32(singleLine[3]) != 0, false);
                    products.Add(product);
                }
                catch(FormatException e)
                {

                }
            }
            return products;
        }
    }
}
