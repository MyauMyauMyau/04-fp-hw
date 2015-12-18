using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace TagCloud
{
	class TagCloudBuilder
	{
		private readonly ConsoleOptions options;
		private const float SizeToPixelsCoefficient = 1.33f;

		public TagCloudBuilder(ConsoleOptions options)
		{
			this.options = options;
		}

		public Bitmap BuildAlphabeticCloud(IEnumerable<string> words)
		{
			var wordsWithSize = GetWordsWithSize(words);
			Bitmap image = new Bitmap(options.Width, options.Height);	   
			//try to draw all of the most frequent words until success or proof that its impossible to do that way 
			while (wordsWithSize.Count > 0)
			{
				int addedWordsCount;
				var newImage = TryToDrawAllWords(wordsWithSize, out addedWordsCount);
				wordsWithSize = wordsWithSize.Take(addedWordsCount).ToDictionary(w => w.Key, w => w.Value);
				if (newImage != null)
					return newImage;
			}
			Console.WriteLine("impossible to draw the cloud");
			return image;
		}

		private Bitmap TryToDrawAllWords(Dictionary<string, int> wordsWithSize, out int addedWordsCount)
		{
			var image = new Bitmap(options.Width, options.Height);
			using (var g = Graphics.FromImage(image))
			{
				float currX = 0;
				float currY = 0;
				addedWordsCount = 0;
				foreach (var word in wordsWithSize.OrderBy(w => w.Key))
				{
					var colorIndex = addedWordsCount % options.Color.Count;
					var drawBrush = new SolidBrush(Color.FromName(options.Color[colorIndex]));
					var drawFont = new Font("Arial", word.Value);
					var measuredWord = g.MeasureString(word.Key, drawFont);
					if (currX + measuredWord.Width > options.Width)
					{
						currX = 0;
						currY += options.MaxFontSize * SizeToPixelsCoefficient;
						//no place for this word currently
						if (currY + options.MaxFontSize * SizeToPixelsCoefficient > options.Height)
							break;
					}
					//no place for this word anywhere
					if (currX + measuredWord.Width > options.Width)
					{
						addedWordsCount = 0;
						break;
					}
					g.DrawString(word.Key, drawFont, drawBrush, new PointF(currX, currY));
					currX += measuredWord.Width;
					addedWordsCount++;
				}
				if (wordsWithSize.Count != addedWordsCount)
					return null;
			}
			return image;
		}

		public Dictionary<string, int> GetWordsWithSize(IEnumerable<string> words)
		{
			var wordsWithFrequency = words
				.GroupBy(w => w)
				.OrderBy(g => -g.Count());
			var maxFreq = wordsWithFrequency.Max(g => g.Count());
			return wordsWithFrequency.ToDictionary(g => g.Key,
				g => (int)Math.Ceiling(
					((double)g.Count() / maxFreq) * (options.MaxFontSize - options.MinFontSize) + options.MinFontSize));
		}

	}
}
