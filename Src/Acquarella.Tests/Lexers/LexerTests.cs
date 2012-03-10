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
        public void GetDotAsPuntuation()
        {
            var result = this.lexer.GetTokens(".").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual(".", token.Value);
            Assert.AreEqual(TokenType.Punctuation, token.Type);
        }
    }
}
