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
            foreach (var item in lines)
            {
                string line = item.Trim();

                if (string.IsNullOrEmpty(line))
                    continue;

                if (line[0] == '#')
                    continue;

                string[] words = line.Split(' ', '\t');                
                string typename = words[0];

                System.Enum.Parse(typeof(TokenType), typename);

                string format = line.Substring(typename.Length).Trim();

                this.SetFormat(typename, format);
            }
        }
    }
}
