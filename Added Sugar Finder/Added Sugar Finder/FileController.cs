using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Added_Sugar_Finder
{
    class FileController
    {
        public static List<string> GetContentFromFile(string path)
        {
            List<string> content = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    content.Add(line);
                }
                sr.Close();
            }

            return content;
        }
    }
}
