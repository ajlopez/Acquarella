namespace Acquarella.Tests.Lexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Acquarella.Lexers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void CreateSimpleToken()
        {
            Token token = new Token(TokenType.Name, "my name", 3, 4);

            Assert.AreEqual("name", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual(3, token.Start);
            Assert.AreEqual(4, token.Length);
        }
    }
}
