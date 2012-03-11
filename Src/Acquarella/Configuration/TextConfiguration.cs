namespace Acquarella.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TextConfiguration
    {
        private IDictionary<string, string> formats = new Dictionary<string, string>();

        public IDictionary<string, string> Formats { get { return this.formats; } }

        public void SetFormat(string name, string format)
        {
            this.formats[name] = format;
        }
    }
}
