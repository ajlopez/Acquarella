namespace Acquarella.Tests.Configuration
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Acquarella.Configuration;

    [TestClass]
    public class TextConfigurationTests
    {
        [TestMethod]
        public void SetAndGetFormat()
        {
            TextConfiguration config = new TextConfiguration();

            config.SetFormat("Keyword", "<keyword>");

            Assert.IsNotNull(config.Formats);
            Assert.AreEqual(1, config.Formats.Count);
            Assert.AreEqual("<keyword>", config.Formats["Keyword"]);
        }

        [TestMethod]
        public void SetAndGetFormatWithDots()
        {
            TextConfiguration config = new TextConfiguration();

            config.SetFormat("Keyword", "<keyword>...</keyword>");

            Assert.IsNotNull(config.Formats);
            Assert.AreEqual(2, config.Formats.Count);
            Assert.AreEqual("<keyword>", config.Formats["KeywordBegin"]);
            Assert.AreEqual("</keyword>", config.Formats["KeywordEnd"]);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\TextHtmlDark.txt")]
        public void LoadHtmlDarkFromFile()
        {
            TextConfiguration config = TextConfiguration.LoadFromFile("TextHtmlDark.txt");

            Assert.IsNotNull(config.Formats["KeywordBegin"]);
            Assert.IsNotNull(config.Formats["KeywordEnd"]);
            Assert.IsNotNull(config.Formats["PunctuationBegin"]);
            Assert.IsNotNull(config.Formats["PunctuationEnd"]);
            Assert.AreEqual("<span style=\"color: darkgreen\">", config.Formats["StringBegin"]);
            Assert.AreEqual("</span>", config.Formats["StringEnd"]);
        }

        [TestMethod]
        [DeploymentItem("Configuration", "Configuration")]
        public void LoadHtmlDarkFromConfigurationByName()
        {
            TextConfiguration config = TextConfiguration.Load("HtmlDark");

            Assert.IsNotNull(config.Formats["KeywordBegin"]);
            Assert.IsNotNull(config.Formats["KeywordEnd"]);
            Assert.IsNotNull(config.Formats["PunctuationBegin"]);
            Assert.IsNotNull(config.Formats["PunctuationEnd"]);
            Assert.AreEqual("<span style=\"color: darkgreen\">", config.Formats["StringBegin"]);
            Assert.AreEqual("</span>", config.Formats["StringEnd"]);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\TextHtmlDark.txt")]
        public void LoadHtmlDarkFromCurrentFolderByName()
        {
            TextConfiguration config = TextConfiguration.Load("HtmlDark");

            Assert.IsNotNull(config.Formats["KeywordBegin"]);
            Assert.IsNotNull(config.Formats["KeywordEnd"]);
            Assert.IsNotNull(config.Formats["PunctuationBegin"]);
            Assert.IsNotNull(config.Formats["PunctuationEnd"]);
            Assert.AreEqual("<span style=\"color: darkgreen\">", config.Formats["StringBegin"]);
            Assert.AreEqual("</span>", config.Formats["StringEnd"]);
        }
    }
}
