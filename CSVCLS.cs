using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HL_塾管理
{
    class CSVCLS
    {
        public void WriteCSV(string filePathName, List<String[]> ls)
        {
            WriteCSV(filePathName, false, ls); 
        }

        public void WriteCSV(string filePathName, bool append, List<String[]> ls)
        {
            Encoding sjisEnc = Encoding.UTF8;
            StreamWriter fileWriter = new StreamWriter(filePathName, append, sjisEnc);

            foreach (String[] strArr in ls)
            {               
                fileWriter.WriteLine(String.Join (",",strArr) );
            }

            fileWriter.Flush();
            fileWriter.Close();
        }

        public List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            try
            {
                Encoding sjisEnc = Encoding.UTF8;
                StreamReader fileReader = new StreamReader(filePathName, sjisEnc);

                string strLine = "";
                while (strLine != null)
                {
                    strLine = fileReader.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        ls.Add(strLine.Split(','));
                    }
                }
                fileReader.Close();
            }
            catch
            {
                return ls; 
            }
            return ls; 
        }
    }
}
