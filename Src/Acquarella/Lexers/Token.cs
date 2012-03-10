namespace Acquarella.Lexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Token
    {
        private string text;
        private int start;
        private int length;
        private TokenType type;

        public Token(TokenType type, string text, int start, int length)
        {
            this.type = type;
            this.text = text;
            this.start = start;
            this.length = length;
        }

        public string Value
        {
            get
            {
                return this.text.Substring(this.start, this.length);
            }
        }

        public TokenType Type { get { return this.type; } }

        public int Start { get { return this.start; } }

        public int Length { get { return this.length; } }
    }
}
