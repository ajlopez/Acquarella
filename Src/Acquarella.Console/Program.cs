namespace Acquarella.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Acquarella.Configuration;
    using Acquarella.Lexers;

    public class Program
    {
        private static IList<string> languages = new List<string>();
        private static IList<string> styles = new List<string>();

        public static void Main(string[] args)
        {
            IList<string> filenames = new List<string>();

            for (int k = 0; k < args.Length; k++)
                if (args[k] == "-l")
                {
                    languages.Add(args[k + 1]);
                    k++;
                }
                else if (args[k] == "-s")
                {
                    styles.Add(args[k + 1]);
                    k++;
                }
                else
                {
                    filenames.Add(args[k]);
                }

            foreach (var filename in filenames)
                    ProcessFile(filename);
        }

        public static void ProcessFile(string filename)
        {
            string text = File.ReadAllText(filename);
            Lexer lexer = new Lexer();

            if (languages.Count > 0)
            {
                foreach (var lang in languages) 
                {
                    if (IsFilename(lang))
                        lexer.ConfigureFromFile(lang);
                    else
                        lexer.Configure(lang);
                }
            }
            else if (filename.EndsWith(".rb"))
                lexer.Configure("Ruby");
            else if (filename.EndsWith(".cs"))
                lexer.Configure("CSharp");
            else if (filename.EndsWith(".js"))
                lexer.Configure("Javascript");
            else if (filename.EndsWith(".py"))
                lexer.Configure("Python");
            else if (filename.EndsWith(".cob"))
                lexer.Configure("Cobol");
            else if (filename.EndsWith(".ms"))
                lexer.Configure("Mass");

            if (styles.Count == 0)
            {
                WriteToConsole(text, lexer);
                return;
            }

            TextRenderer renderer = new TextRenderer(lexer);

            foreach (var t in styles)
            {
                if (IsFilename(t))
                    renderer.ConfigureFromFile(t);
                else
                    renderer.Configure(t);
            }

            Console.Write(renderer.Render(text));
        }

        public static void WriteToConsole(string text, Lexer lexer)
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Colorizer colorizer = new Colorizer();

            int position = 0;
            TokenType lasttype = TokenType.Space;
            SetColor(lasttype);

            foreach (var colortype in colorizer.GetColorTypes(text, lexer))
            {
                if (colortype != lasttype)
                {
                    SetColor(colortype);
                    lasttype = colortype;
                }

                Console.Write(text[position]);
                position++;
            }

            Console.ForegroundColor = foreground;
        }

        public static void SetColor(TokenType type)
        {
            switch (type)
            {
                case TokenType.Comment:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case TokenType.String:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case TokenType.Number:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case TokenType.Punctuation:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case TokenType.Keyword:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        private static bool IsFilename(string name)
        {
            return name.Contains(':') || name.Contains('\\') || name.Contains('.');
        }
    }
}
