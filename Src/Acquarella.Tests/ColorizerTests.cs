namespace Acquarella.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Acquarella.Lexers;

    [TestClass]
    public class ColorizerTests
    {
        private Colorizer colorizer;

        [TestInitialize]
        public void Setup()
        {
            this.colorizer = new Colorizer();
        }

        [TestMethod]
        public void GetColorTypesForSimpleName()
        {
            var result = this.colorizer.GetColorTypes("name", new Lexer());

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            Assert.IsTrue(result.All(r => r == TokenType.Name));
        }

        [TestMethod]
        public void GetColorTypesForTwoNames()
        {
            var result = this.colorizer.GetColorTypes("my name", new Lexer());

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Where(r => r == TokenType.Name).Count());
            Assert.AreEqual(1, result.Where(r => r == TokenType.Space).Count());
            Assert.AreEqual(7, result.Count());
        }
    }
}
