# Acquarella

Syntax Highlighter, written in C#

Work in Progress

## Usage

In your project, reference the Acquarella.dll or Acquarella project. Then, you can transform a text using:

```C#
using Acquarella;

// ...

var result = TextRenderer(text, language, style);
```

Actually, language can be one of these options (string value):

- CSharp
- VbNet
- Ruby
- Python
- Javascript

style parameter values (string value):

- HtmlDark
- HtmlLight

You can configure other languages. See Acquarella\Configuration folder.

Example

```C#
var result = TextRenderer(text, "CSharp", "HtmlDark");
```

You can use the configuration filenames instead of the simple names:

```C#
var result = TextRenderer(text, "Configuration\\CSharp.txt", "Configuration\\TextHtmlDark.txt");
```

You can write your own configuration files for languages and styles.

## Configuration Files

TBD

## Extending Lexer

TBD

## Command Line Tool

You can execute the executable from Acquarella.Console project:

	acquarellac <files>...

The file contents are rendered with fixed colors in console. I plan to add HTML output.

You can use

    acquarellac -l <language> -s <style> <files>...
	
In this case, a text without colors is emmitted.

## To Do

- Comment detection
- Case unsensitive option (for Visual Basic alike languages)
- Escape characters in strings
- Doc strings
- Configurable Console Color Palette

