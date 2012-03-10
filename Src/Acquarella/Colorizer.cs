namespace Acquarella
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Acquarella.Lexers;

    public class Colorizer
    {
        public IEnumerable<TokenType> GetColorTypes(string text, Lexer lexer)
        {
            int position = 0;

            foreach (var token in lexer.GetTokens(text))
            {
                while (position < token.Start)
                {
                    yield return TokenType.Space;
                    position++;
                }

                while (position < token.Start + token.Length)
                {
                    yield return token.Type;
                    position++;
                }
            }

            int length = text.Length;

            while (position < length)
            {
                yield return TokenType.Space;
                position++;
            }
        }
    }
}
