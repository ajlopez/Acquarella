namespace Acquarella.Lexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Acquarella.Configuration;

    public class Lexer
    {
        private int position;
        private int length;
        private string text;

        private IList<char> stringdelimeters;
        private IList<string> keywords;
        private IList<string> operators;
        private IList<string> linecomments;

        public IList<char> StringDelimeters
        {
            get { return this.stringdelimeters; }
            set { this.stringdelimeters = value; }
        }

        public IList<string> Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        public IList<string> Operators
        {
            get { return this.operators; }
            set { this.operators = value; }
        }

        public IList<string> LineComments
        {
            get { return this.linecomments; }
            set { this.linecomments = value; }
        }

        public IEnumerable<Token> GetTokens(string text)
        {
            this.position = 0;
            this.text = text;
            this.length = text.Length;

            Token token;

            while ((token = this.GetNextToken()) != null)
                yield return token;
        }

        public void ConfigureFromFile(string filename)
        {
            LexerConfiguration config = LexerConfiguration.LoadFromFile(filename);
            this.Configure(config);
        }

        public void Configure(string name)
        {
            LexerConfiguration config = LexerConfiguration.Load(name);
            this.Configure(config);
        }

        public void Configure(LexerConfiguration config)
        {
            if (config == null)
                return;

            if (config.Keywords != null)
                if (this.keywords == null)
                    this.keywords = config.Keywords;
                else
                    this.keywords = this.keywords.Union(config.Keywords).ToList();

            if (config.Operators != null)
                if (this.operators == null)
                    this.operators = config.Operators;
                else
                    this.operators = this.operators.Union(config.Operators).ToList();

            if (config.StringDelimeters != null)
                if (this.stringdelimeters == null)
                    this.stringdelimeters = config.StringDelimeters;
                else
                    this.stringdelimeters = this.stringdelimeters.Union(config.StringDelimeters).ToList();

            if (config.LineComments != null)
                if (this.linecomments == null)
                    this.linecomments = config.LineComments;
                else
                    this.linecomments = this.linecomments.Union(config.LineComments).ToList();
        }

        private Token GetNextToken()
        {
            this.SkipWhiteSpaces();

            if (this.position >= this.length)
                return null;

            int start = this.position;

            int length;

            char ch = this.text[this.position];

            if (this.IsLineComment(ch))
            {
                for (this.position++; this.position < this.length; this.position++)
                {
                    ch = this.text[this.position];
                    if (ch == '\r' || ch == '\n')
                        break;
                }

                length = this.position - start;

                return new Token(TokenType.Comment, this.text, start, length);
            }

            if (this.IsStringDelimeter(ch))
            {
                this.position++;

                while (this.position < this.length && this.text[this.position] != ch)
                    this.position++;

                if (this.position < this.length)
                    this.position++;

                length = this.position - start;

                return new Token(TokenType.String, this.text, start, length);
            }

            if (!this.IsLetterOrDigit(ch))
            {
                this.position++;

                if (this.IsOperator(ch))
                    return new Token(TokenType.Operator, this.text, start, 1);

                return new Token(TokenType.Punctuation, this.text, start, 1);
            }

            if (this.IsLetter(ch))
            {
                while (this.position < this.length && !this.IsWhiteSpace(this.text[this.position]) && this.IsLetterOrDigit(this.text[this.position]))
                    this.position++;

                length = this.position - start;

                if (this.IsKeyword(this.text.Substring(start, length)))
                    return new Token(TokenType.Keyword, this.text, start, length);

                return new Token(TokenType.Name, this.text, start, length);
            }

            if (this.IsDigit(ch))
            {
                while (this.position < this.length && this.IsDigit(this.text[this.position]))
                    this.position++;

                if (this.position < this.length - 1 && this.text[this.position] == '.' && this.IsDigit(this.text[this.position + 1]))
                {
                    this.position++;

                    while (this.position < this.length && this.IsDigit(this.text[this.position]))
                        this.position++;
                }

                length = this.position - start;

                return new Token(TokenType.Number, this.text, start, length);
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

        private bool IsLineComment(char ch)
        {
            if (this.linecomments == null)
                return false;

            string delimiter = ch.ToString();

            if (this.linecomments.Contains(delimiter))
                return true;

            if (this.position >= this.length - 1)
                return false;

            delimiter += this.text[this.position + 1];

            if (this.linecomments.Contains(delimiter))
                return true;

            return false;
        }

        private bool IsDigit(char ch)
        {
            return char.IsDigit(ch);
        }

        private bool IsLetter(char ch)
        {
            return char.IsLetter(ch);
        }

        private bool IsLetterOrDigit(char ch)
        {
            return char.IsLetterOrDigit(ch);
        }

        private bool IsStringDelimeter(char ch)
        {
            if (this.stringdelimeters == null)
                return ch == '"';

            return this.stringdelimeters.Contains(ch);
        }

        private bool IsKeyword(string name)
        {
            if (this.keywords == null)
                return false;

            return this.keywords.Contains(name);
        }

        private bool IsOperator(char ch)
        {
            if (this.operators == null)
                return false;

            return this.operators.Contains(ch.ToString());
        }
    }
}
