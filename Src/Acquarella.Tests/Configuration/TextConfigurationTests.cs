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
    }
}
