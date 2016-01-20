using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlienationConverter
{
    class Program
    {
        static void Main(string[] args)
        {                       
            Convert(AppDomain.CurrentDomain.BaseDirectory+"citaj.txt");
            
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void Convert(String filepath)
        {            
            //string text = System.IO.File.ReadAllText(filepath);         
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
                        
            String[] lines = System.IO.File.ReadAllLines(filepath);
            String[] output = new String[10];
            Dictionary<String, LinkedList<LinkedList<String>>> types = new Dictionary<string, LinkedList<LinkedList<String>>>();
            Dictionary<String, LinkedList<String>> typesList = new Dictionary<string, LinkedList<string>>();
            for (int i = 0; i < lines.Length; i++)
            {

                var arrProp = parseLine(lines[i]);
                if (arrProp == null) continue;

                string t = "";
                LinkedList<String> props = new LinkedList<string>();
                for (int j = 0; j < arrProp.Length-1; j++)
                {
                    //Console.WriteLine(arrProp[j]);
                    if (j % 2 == 0)
                    {
                        props.AddLast(arrProp[j]);
                      
                    } else
                    {
                        
                    }
                    if (arrProp[j] == "event_type")
                    {
                        t = arrProp[j + 1];
                    }
                }

                if (!types.ContainsKey(t))
                {
                    types.Add(t, new LinkedList<LinkedList<String>>());
                }
                types[t].AddLast(props);

                
            }

            var keys = types.Keys;
            
            foreach(var key in keys)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var entries in types[key])
                {
                    foreach(var entry in entries)
                    {
                        sb.Append(entry);
                        sb.AppendLine(",");
                    }                    
                }
                System.IO.File.WriteAllText(key+"_Event.txt", sb.ToString());
            }



            
        }


        //static String clean(String line)
        //{
        //    for(int i = 0; i<line.Length; i++)
        //    {
        //        if(line[i] == '{' || line[i] == '}' || line[i] == '"' || line[i] == ':') 
        //    }
        //    return line;
        //}



         static String getEventType(String [] arrProp)
        {
            string t = "";
            for (int j = 0; j < arrProp.Length; j++)
            {
                if (arrProp[j] == "event_type")
                {
                    t = arrProp[j + 1];
                }
            }
            return t;
        }


        static String[] parseLine(String line)
        {            
            var start = line.IndexOf('{') + 1;
            var end = line.LastIndexOf('}');
            if (start > 0 && end > 0)
            {
                var first = line.Substring(start, end - start);
                first = first.Replace("{", "").Replace("}", "");
                first = first.Replace(",", " ").Replace("\"", "").Replace(":", " ").Replace("	", " "); ;
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                first = regex.Replace(first, @" ");

                return first.Split(' ');

                //Dictionary<String, String> properties = new Dictionary<string, string>();

                //foreach(var item in arr)                                {
                //    var p = item.Split(':');
                //    p[0];
                //    p[1];
                //}
            }
            return null;
        }
    }
}
