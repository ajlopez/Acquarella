﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acquarella.Lexers;

namespace Acquarella.Tests
{
    [TestClass]
    public class TextRendererTests
    {
        [TestMethod]
        public void GetPlainTextWithSimpleRenderer()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());
            string text = "foreach (var k in values) {";

            var result = renderer.Render(text);

            Assert.AreEqual(text, result);
        }

        [TestMethod]
        public void GetTextWithNames()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());

            renderer.SetFormat("NameBegin", "<name>");
            renderer.SetFormat("NameEnd", "</name>");

            string text = "foreach (var k in values) {";

            var result = renderer.Render(text);

            Assert.AreEqual("<name>foreach</name> (<name>var</name> <name>k</name> <name>in</name> <name>values</name>) {", result);
        }

        [TestMethod]
        public void GetTextWithPuntuation()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());

            renderer.SetFormat("PunctuationBegin", "<pt>");
            renderer.SetFormat("PunctuationEnd", "</pt>");

            string text = "foreach (var k in values) {";

            var result = renderer.Render(text);

            Assert.AreEqual("foreach <pt>(</pt>var k in values<pt>)</pt> <pt>{</pt>", result);
        }

        [TestMethod]
        public void GetTextWithTextBeginAndEd()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());

            renderer.SetFormat("TextBegin", "<div>\r\n");
            renderer.SetFormat("TextEnd", "\r\n</div>\r\n");

            string text = "foreach (var k in values) {";

            var result = renderer.Render(text);

            Assert.AreEqual("<div>\r\nforeach (var k in values) {\r\n</div>\r\n", result);
        }

        [TestMethod]
        [DeploymentItem("Configuration\\TextHtmlDark.txt")]
        public void LoadHtmlDarkFromFile()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());
            renderer.ConfigureFromFile("TextHtmlDark.txt");
            Assert.AreEqual("<span style=\"color: darkgreen\">", renderer.GetFormat("StringBegin"));
            Assert.AreEqual("</span>", renderer.GetFormat("StringEnd"));
        }

        [TestMethod]
        [DeploymentItem("Configuration\\TextHtmlDark.txt")]
        public void LoadHtmlDarkByName()
        {
            TextRenderer renderer = new TextRenderer(new Lexer());
            renderer.Configure("HtmlDark");
            Assert.AreEqual("<span style=\"color: darkgreen\">", renderer.GetFormat("StringBegin"));
            Assert.AreEqual("</span>", renderer.GetFormat("StringEnd"));
        }
    }
}
