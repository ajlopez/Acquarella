namespace Acquarella.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.IO;
using Acquarella.Lexers;

    class Program
    {
        static void Main(string[] args)
        {
            foreach (var filename in args)
                ProcessFile(filename);
        }

        static void ProcessFile(string filename)
        {
            ConsoleColor foreground = Console.ForegroundColor;
            string text = File.ReadAllText(filename);
            Colorizer colorizer = new Colorizer();

            int position = 0;
            TokenType lasttype = TokenType.Space;
            SetColor(lasttype);

            Lexer lexer = new Lexer();

            if (filename.EndsWith(".rb"))
                lexer.Configure("Ruby");
            else if (filename.EndsWith(".cs"))
                lexer.Configure("CSharp");
            else if (filename.EndsWith(".js"))
                lexer.Configure("Javascript");

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

        static void SetColor(TokenType type)
        {
            switch (type) 
            {
                case TokenType.String:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case TokenType.Punctuation:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case TokenType.Keyword:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }
}
