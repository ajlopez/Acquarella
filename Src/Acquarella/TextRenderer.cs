namespace Acquarella
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Acquarella.Lexers;
    using Acquarella.Configuration;

    public class TextRenderer
    {
        private Lexer lexer;
        private IDictionary<string, string> formats = new Dictionary<string, string>();

        public TextRenderer(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public void SetFormat(string name, string format)
        {
            this.formats[name] = format;
        }

        public string GetFormat(string name)
        {
            if (!this.formats.ContainsKey(name))
                return null;

            return this.formats[name];
        }

        public void ConfigureFromFile(string filename)
        {
            this.Configure(TextConfiguration.LoadFromFile(filename));
        }

        public void Configure(string name)
        {
            this.Configure(TextConfiguration.Load(name));
        }

        public void Configure(TextConfiguration config)
        {
            foreach (var name in config.Formats.Keys)
                this.SetFormat(name, config.Formats[name]);
        }

        public string Render(string text)
        {
            Colorizer colorizer = new Colorizer();
            int position = 0;
            TokenType lasttype = TokenType.Space;

            StringBuilder sb = new StringBuilder(text.Length * 2);

            string prologue = this.GetFormat("TextBegin");

            if (prologue != null)
                sb.Append(prologue);

            foreach (var colortype in colorizer.GetColorTypes(text, this.lexer))
            {
                if (colortype != lasttype)
                {
                    if (position > 0)
                    {
                        string endformat = this.GetFormat(System.Enum.GetName(typeof(TokenType), lasttype) + "End");
                        if (!string.IsNullOrEmpty(endformat))
                            sb.Append(endformat);
                    }

                    string beginformat = this.GetFormat(System.Enum.GetName(typeof(TokenType), colortype) + "Begin");
                    if (!string.IsNullOrEmpty(beginformat))
                        sb.Append(beginformat);

                    lasttype = colortype;
                }

                sb.Append(text[position]);
                position++;
            }

            if (position > 0)
            {
                string endformat = this.GetFormat(System.Enum.GetName(typeof(TokenType), lasttype) + "End");
                if (!string.IsNullOrEmpty(endformat))
                    sb.Append(endformat);
            }

            string epilog = this.GetFormat("TextEnd");

            if (epilog != null)
                sb.Append(epilog);

            return sb.ToString();
        }
    }
}
