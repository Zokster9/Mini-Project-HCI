using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject
{
    public class CSV
    {
        public static List<string> loadCurrency()
        {
            List<string> foreignExchangeList = new List<string>();
            string path = @"./../../FOREX.csv";

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                foreignExchangeList.Add(line.Split(',')[0]);
            }
            return foreignExchangeList;
        }
    }
}
