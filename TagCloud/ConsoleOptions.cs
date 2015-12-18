using System;
using System.Collections.Generic;
using CommandLine;
namespace TagCloud
{
	class ConsoleOptions
	{
		[Option('i', "input", DefaultValue = "../../input.txt", HelpText = "file to read")]
		public string InputFilePath { get; set; }

		[Option('o', "output", DefaultValue = "../../output", HelpText = "File to write.")]
		public string OutputFilePath { get; set; }

		[Option("format", DefaultValue = ".png", HelpText = "Output file format")]
		public string OutputFormat { get; set; }

		[Option('w', "width", DefaultValue = 512, HelpText = "Width of cloud.")]
		public int Width { get; set; }

		[Option('h', "height", DefaultValue = 512, HelpText = "Height of cloud.")]
		public int Height { get; set; }

		[OptionList('c', "colors", Separator = ':', DefaultValue = new string[] { "red", "blue", "green" }, HelpText = "Colors of words")]
		public IList<string> Color { get; set; }

		[Option("minsize", DefaultValue = 45, HelpText = "Minimum font size")]
		public int MinFontSize { get; set; }																 

		[Option('s', "maxsize", DefaultValue = 90, HelpText = "Maximum font size")]
		public int MaxFontSize { get; set; }

		
	}
}
