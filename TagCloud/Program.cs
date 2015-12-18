using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagCloud
{

    class Program
    {
	    private const string BadWordsFilePath = "../../prepositions.txt";

	    static void Main(string[] args)
	    {
		    var options = new ConsoleOptions();
			if (!CommandLine.Parser.Default.ParseArguments(args, options))
				return;
		    var badwords = File.ReadAllText(BadWordsFilePath).Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
			var words = ReadWords(options.InputFilePath).Where(w => !badwords.Contains(w));
			var image = new TagCloudBuilder(options).BuildAlphabeticCloud(words);
		    WriteImage(image, options.OutputFilePath, options.OutputFormat);
        }
		private static string[] ReadWords(string path) => File.ReadAllText(path).ToLower().Split(' ');
		public static void WriteImage(Bitmap bitmap, string path, string format)
		{
			var imageFormatFromString = new Dictionary<string, ImageFormat>
			{
				[".png"] = ImageFormat.Png,
				[".bmp"] = ImageFormat.Bmp,
				[".jpg"] = ImageFormat.Jpeg,
				[".gif"] = ImageFormat.Gif
			};
			bitmap.Save(path + format, imageFormatFromString[format]);
		}
    }										
}
