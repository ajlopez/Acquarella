namespace Acquarella.Lexers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum TokenType
    {
        Name,
        Keyword,
        String,
        Number,
        Operator,
        Punctuation,
        Special,
        Space,
        Unknown
    }
}
