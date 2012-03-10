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
            Lexer lexer = new Lexer();

            var result = lexer.GetTokens("name").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var token = result[0];

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
        }
    }
}
