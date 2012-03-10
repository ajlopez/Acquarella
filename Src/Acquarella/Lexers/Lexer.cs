﻿namespace Acquarella.Lexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private int position;
        private int length;
        private string text;

        public IEnumerable<Token> GetTokens(string text)
        {
            this.position = 0;
            this.text = text;
            this.length = text.Length;

            Token token;

            while ((token = this.GetNextToken()) != null)
                yield return token;
        }

        private Token GetNextToken()
        {
            this.SkipWhiteSpaces();

            if (this.position >= this.length)
                return null;

            int start = this.position;
            int length;

            char ch = this.text[this.position];

            if (ch == '"')
            {
                this.position++;

                while (this.position < this.length && this.text[this.position] != '"')
                    this.position++;

                if (this.position < this.length)
                    this.position++;

                length = this.position - start;

                return new Token(TokenType.String, this.text, start, length);
            }

            if (!this.IsLetterOrDigit(ch))
            {
                this.position++;

                return new Token(TokenType.Punctuation, this.text, start, 1);
            }

            if (this.IsLetter(ch))
            {
                while (this.position < this.length && !this.IsWhiteSpace(this.text[this.position]))
                    this.position++;

                length = this.position - start;

                return new Token(TokenType.Name, this.text, start, length);
            }

            this.position++;

            return new Token(TokenType.Unknown, this.text, start, 1);
        }

        private void SkipWhiteSpaces()
        {
            while (this.position < this.length && this.IsWhiteSpace(this.text[this.position]))
                this.position++;
        }

        private bool IsWhiteSpace(char ch)
        {
            return char.IsWhiteSpace(ch);
        }

        private bool IsLetter(char ch)
        {
            return char.IsLetter(ch);
        }

        private bool IsLetterOrDigit(char ch)
        {
            return char.IsLetterOrDigit(ch);
        }
    }
}
