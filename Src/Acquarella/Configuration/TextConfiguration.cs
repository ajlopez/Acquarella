namespace Acquarella.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Acquarella.Lexers;

    public class TextConfiguration
    {
        private IDictionary<string, string> formats = new Dictionary<string, string>();

        public IDictionary<string, string> Formats { get { return this.formats; } }

        public static TextConfiguration LoadFromFile(string filename)
        {
            var result = new TextConfiguration();
            result.Load(File.ReadAllLines(filename));
            return result;
        }

        public static TextConfiguration Load(string name)
        {
            string filename = "Text" + name + ".txt";

            if (File.Exists(filename))
                return LoadFromFile(filename);

            filename = Path.Combine("Configuration", filename);

            if (File.Exists(filename))
                return LoadFromFile(filename);

            return null;
        }

        public void SetFormat(string name, string format)
        {
            int p = format.IndexOf("...");

            if (p >= 0)
            {
                string begin = format.Substring(0, p);
                string end = format.Substring(p + 3);
                this.formats[name + "Begin"] = begin;
                this.formats[name + "End"] = end;
            }
            else
                this.formats[name] = format;
        }

        private void Load(string[] lines)
        {
            bool intextbegin = false;
            bool intextend = false;
            IList<string> textbegin = new List<string>();
            IList<string> textend = new List<string>();

            foreach (var item in lines)
            {
                string line = item.Trim();

                if (string.IsNullOrEmpty(line))
                    continue;

                if (line[0] == '#')
                    continue;

                string[] words = line.Split(' ', '\t');                
                string typename = words[0];

                if (!intextbegin && !intextend)
                {
                    if (typename.Equals("text", StringComparison.InvariantCultureIgnoreCase))
                    {
                        intextbegin = true;
                        continue;
                    }

                    System.Enum.Parse(typeof(TokenType), typename);

                    string format = line.Substring(typename.Length).Trim();

                    this.SetFormat(typename, format);
                }
                else if (intextbegin)
                {
                    if (words.Length == 1 && typename == "...")
                    {
                        intextbegin = false;
                        intextend = true;
                        continue;
                    }

                    if (words.Length == 1 && typename.Equals("end", StringComparison.InvariantCultureIgnoreCase))
                    {
                        intextbegin = false;
                        intextend = false;
                        continue;
                    }

                    textbegin.Add(item);
                }
                else if (intextend)
                {
                    if (words.Length == 1 && typename.Equals("end", StringComparison.InvariantCultureIgnoreCase))
                    {
                        intextbegin = false;
                        intextend = false;
                        continue;
                    }

                    textend.Add(item);
                }
            }

            string tbegin = string.Empty;
            string tend = string.Empty;

            foreach (var line in textbegin)
                tbegin += line + "\r\n";
            
            foreach (var line in textend)
                tend += line + "\r\n";

            if (textend.Count == 0 && textbegin.Count > 0)
                this.SetFormat("Text", tbegin);
            else if (textend.Count > 0 && textbegin.Count > 0)
            {
                this.SetFormat("TextBegin", tbegin);
                this.SetFormat("TextEnd", tend);
            }
        }
    }
}
