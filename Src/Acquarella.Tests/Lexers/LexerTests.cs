namespace Acquarella.Tests.Lexers
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Acquarella.Lexers;

    [TestClass]
    public class LexerTests
    {
        private Lexer lexer;

        [TestInitialize]
        public void Setup()
        {
            this.lexer = new Lexer();
        }

        [TestMethod]
        public void GetNoTokenOnEmptyString()
        {
            Assert.AreEqual(0, this.lexer.GetTokens(string.Empty).Count());
        }

        [TestMethod]
        public void GetNoTokenOnBlankString()
        {
            Assert.AreEqual(0, this.lexer.GetTokens("   ").Count());
        }

        [TestMethod]
        public void GetNameToken()
        {
            var result = this.lexer.GetTokens("name").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }

        [TestMethod]
        public void GetNameAndPunctuation()
        {
            var result = this.lexer.GetTokens("name!").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            token = result[1];

            Assert.AreEqual("!", token.Value);
            Assert.AreEqual(TokenType.Punctuation, token.Type);
        }

        [TestMethod]
        public void GetTwoNames()
        {
            var result = this.lexer.GetTokens("your name").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("your", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            token = result[1];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }

        [TestMethod]
        public void GetNameTokenWithSpaces()
        {
            var result = this.lexer.GetTokens("  name   ").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }

        [TestMethod]
        public void GetNameTokenWithWhiteChars()
        {
            var result = this.lexer.GetTokens("\t  name \r\n   ").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }

        [TestMethod]
        public void GetSimpleString()
        {
            var result = this.lexer.GetTokens("\"text\"").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("\"text\"", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);
        }

        [TestMethod]
        public void GetInteger()
        {
            var result = this.lexer.GetTokens("123").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("123", token.Value);
            Assert.AreEqual(TokenType.Number, token.Type);
        }

        [TestMethod]
        public void GetReal()
        {
            var result = this.lexer.GetTokens("123.45").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("123.45", token.Value);
            Assert.AreEqual(TokenType.Number, token.Type);
        }

        [TestMethod]
        public void GetSimpleStringWithSingleQuote()
        {
            this.lexer.StringDelimeters = new char[] { '"', '\'' };

            Assert.AreEqual(2, this.lexer.StringDelimeters.Count());

            var result = this.lexer.GetTokens("'text'").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("\'text\'", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);
        }

        [TestMethod]
        public void GetTwoStrings()
        {
            var result = this.lexer.GetTokens("\"text\"\"text2\"").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("\"text\"", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);

            token = result[1];

            Assert.AreEqual("\"text2\"", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);
        }

        [TestMethod]
        public void GetTwoQuotedStrings()
        {
            this.lexer.StringDelimeters = new char[] { '"', '\'' };
            var result = this.lexer.GetTokens("'text' 'text2'").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("'text'", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);

            token = result[1];

            Assert.AreEqual("'text2'", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);
        }

        [TestMethod]
        public void GetKeywordAndName()
        {
            this.lexer.Keywords = new string[] { "for", "foreach", "if" };

            Assert.AreEqual(3, this.lexer.Keywords.Count);

            var result = this.lexer.GetTokens("for k").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("for", token.Value);
            Assert.AreEqual(TokenType.Keyword, token.Type);

            token = result[1];

            Assert.AreEqual("k", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }

        [TestMethod]
        public void GetDotAsPuntuation()
        {
            var result = this.lexer.GetTokens(".").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Punctuation, token.Type);
        }

        [TestMethod]
        public void GetDotAsOperator()
        {
            this.lexer.Operators = new string[] { ".", "+", "-" };

            Assert.AreEqual(3, this.lexer.Operators.Count);

            var result = this.lexer.GetTokens(".").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);
        }

        [TestMethod]
        public void GetOperators()
        {
            this.lexer.Operators = new string[] { ".", "+", "-" };

            Assert.AreEqual(3, this.lexer.Operators.Count);

            var result = this.lexer.GetTokens(".+-").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);

            foreach (var token in result)
                Assert.AreEqual(TokenType.Operator, token.Type);
        }

        [TestMethod]
        public void GetLineCommentWithSheBang()
        {
            this.lexer.LineComments = new string[] { "#" };

            var result = this.lexer.GetTokens("# Line Comment\r\nname").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("# Line Comment", token.Value);
            Assert.AreEqual(TokenType.Comment, token.Type);
        }

        [TestMethod]
        public void GetLineCommentWithDoubleSlash()
        {
            this.lexer.LineComments = new string[] { "#", "//" };

            var result = this.lexer.GetTokens("// Line Comment\r\nname").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var token = result[0];

            Assert.AreEqual("// Line Comment", token.Value);
            Assert.AreEqual(TokenType.Comment, token.Type);
        }

        [TestMethod]
        public void GetPuntuations()
        {
            var text = ";.(){}";
            int position = 0;

            foreach (var token in this.lexer.GetTokens(text))
            {
                Assert.AreEqual(TokenType.Punctuation, token.Type);
                Assert.AreEqual(position, token.Start);
                Assert.AreEqual(1, token.Length);
                Assert.AreEqual(text[position].ToString(), token.Value);
                position++;
            }

            Assert.AreEqual(position, text.Length);
        }

        [TestMethod]
        public void ConfigureWithNonExistentName()
        {
            this.lexer.Configure("Foo");

            Assert.IsNull(this.lexer.StringDelimeters);
            Assert.IsNull(this.lexer.Keywords);
            Assert.IsNull(this.lexer.Operators);
        }

        [TestMethod]
        [DeploymentItem("Configuration", "Configuration")]
        public void ConfigureCSharpByName()
        {
            this.lexer.Configure("CSharp");

            Assert.IsNull(this.lexer.StringDelimeters);
            Assert.IsNotNull(this.lexer.Keywords);
            Assert.IsNotNull(this.lexer.Operators);
            Assert.IsNotNull(this.lexer.LineComments);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\CSharp.txt")]
        public void ConfigureCSharpByFile()
        {
            this.lexer.ConfigureFromFile("CSharp.txt");

            Assert.IsNull(this.lexer.StringDelimeters);
            Assert.IsNotNull(this.lexer.Keywords);
            Assert.IsNotNull(this.lexer.Operators);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\Ruby.txt")]
        public void ConfigureRuby()
        {
            this.lexer.Configure("Ruby");

            Assert.IsNotNull(this.lexer.StringDelimeters);
            Assert.IsNotNull(this.lexer.Keywords);
            Assert.IsNotNull(this.lexer.Operators);

            Assert.IsNotNull(this.lexer.LineComments);
            Assert.IsTrue(this.lexer.LineComments.Contains("#"));
        }

        [TestMethod]
        [DeploymentItem("Configuration\\Python.txt")]
        public void ConfigurePython()
        {
            this.lexer.Configure("Python");

            Assert.IsNotNull(this.lexer.StringDelimeters);
            Assert.IsNotNull(this.lexer.Keywords);
            Assert.IsNotNull(this.lexer.Operators);

            Assert.IsNotNull(this.lexer.LineComments);
            Assert.IsTrue(this.lexer.LineComments.Contains("#"));
        }

        [TestMethod]
        [DeploymentItem("Configuration\\Javascript.txt")]
        public void ConfigureJavascript()
        {
            this.lexer.Configure("Javascript");

            Assert.IsNotNull(this.lexer.StringDelimeters);
            Assert.IsNotNull(this.lexer.Keywords);
            Assert.IsNotNull(this.lexer.Operators);
            Assert.IsNotNull(this.lexer.LineComments);
            Assert.IsTrue(this.lexer.LineComments.Contains("//"));
        }
    }
}
