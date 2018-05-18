using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader lec = new StreamReader("..//..//..//Analytic/DataTrainingText.txt");
            List<string> lines = new List<string>();
            string line;
            while ((line = lec.ReadLine()) != null&&!line.Equals(""))
            {
                string[] tmp = line.Split('|');
                int.TryParse(tmp[2],out int num);
                tmp[2] = "" + (num<0?-1:(num>0?1:0));
                string ne = tmp[0] + "|" + tmp[1] + "|" + tmp[2];
                lines.Add(ne);
            }
            lec.Close();
            StreamWriter esc = new StreamWriter("..//..//..//Analytic/DataTrainingText.txt");

            foreach (var item in lines)
            {
                esc.Write(item);
                esc.WriteLine();
            }
            esc.Close();
        }
    }
}
