namespace Acquarella.Tests.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Acquarella.Configuration;
    using Acquarella.Lexers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerConfigurationTests
    {
        [TestMethod]
        public void GetEmptyConfiguration()
        {
            LexerConfiguration config = new LexerConfiguration();

            Assert.IsNull(config.Keywords);
            Assert.IsNull(config.Operators);
            Assert.IsNull(config.StringDelimeters);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\CSharp.txt")]
        public void GetCSharpConfigurationFromFile()
        {
            LexerConfiguration config = LexerConfiguration.LoadFromFile("CSharp.txt");

            Assert.IsNotNull(config.Keywords);
            Assert.IsNotNull(config.Operators);
            Assert.IsNull(config.StringDelimeters);

            Assert.AreEqual(8, config.Operators.Count);
            Assert.AreEqual(77, config.Keywords.Count);

            Assert.IsTrue(config.Operators.Contains("="));
            Assert.IsTrue(config.Keywords.Contains("for"));
            Assert.IsTrue(config.Keywords.Contains("foreach"));
            Assert.IsTrue(config.Keywords.Contains("class"));
            Assert.IsTrue(config.Keywords.Contains("if"));
            Assert.IsTrue(config.Keywords.Contains("while"));
        }

        [TestMethod]
        [DeploymentItem("Configuration", "Configuration")]
        public void GetCSharpConfigurationWithNameFromConfigurationFolder()
        {
            LexerConfiguration config = LexerConfiguration.Load("CSharp");

            Assert.IsNotNull(config.Keywords);
            Assert.IsNotNull(config.Operators);
            Assert.IsNull(config.StringDelimeters);

            Assert.AreEqual(8, config.Operators.Count);
            Assert.AreEqual(77, config.Keywords.Count);

            Assert.IsTrue(config.Operators.Contains("="));
            Assert.IsTrue(config.Keywords.Contains("for"));
            Assert.IsTrue(config.Keywords.Contains("foreach"));
            Assert.IsTrue(config.Keywords.Contains("class"));
            Assert.IsTrue(config.Keywords.Contains("if"));
            Assert.IsTrue(config.Keywords.Contains("while"));
        }

        [TestMethod]
        [DeploymentItem("Configuration\\CSharp.txt")]
        public void GetCSharpConfigurationWithNameFromCurrentFolder()
        {
            LexerConfiguration config = LexerConfiguration.Load("CSharp");

            Assert.IsNotNull(config.Keywords);
            Assert.IsNotNull(config.Operators);
            Assert.IsNull(config.StringDelimeters);
            Assert.IsNotNull(config.LineComments);

            Assert.AreEqual(8, config.Operators.Count);
            Assert.AreEqual(77, config.Keywords.Count);
            Assert.AreEqual(1, config.LineComments.Count);

            Assert.IsTrue(config.Operators.Contains("="));
            Assert.IsTrue(config.Keywords.Contains("for"));
            Assert.IsTrue(config.Keywords.Contains("foreach"));
            Assert.IsTrue(config.Keywords.Contains("class"));
            Assert.IsTrue(config.Keywords.Contains("if"));
            Assert.IsTrue(config.Keywords.Contains("while"));
            Assert.IsTrue(config.LineComments.Contains("//"));
        }

        [TestMethod]
        [DeploymentItem("Files\\InvalidLanguage.txt")]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenInvalidFileContent()
        {
            LexerConfiguration config = LexerConfiguration.LoadFromFile("InvalidLanguage.txt");
        }

        [TestMethod]
        [DeploymentItem("Configuration", "Configuration")]
        public void GetJavascriptConfigurationWithName()
        {
            LexerConfiguration config = LexerConfiguration.Load("Javascript");

            Assert.IsNotNull(config.Keywords);
            Assert.IsNotNull(config.Operators);
            Assert.IsNotNull(config.StringDelimeters);

            Assert.AreEqual(2, config.StringDelimeters.Count);
        }
    }
}
