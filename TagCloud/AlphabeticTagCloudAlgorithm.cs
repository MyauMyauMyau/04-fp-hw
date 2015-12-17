using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TagCloud
{
	 
	class AlphabeticTagCloudAlgorithm : IAlgorithm
	{
		const float SizeToPixelsCoefficient = 1.333f;
		private readonly IOptions options;
		public AlphabeticTagCloudAlgorithm(IOptions options)
		{
			this.options = options;
		}
		public Bitmap BuildTagCloud(string[] words)
		{
			var wordsWithSize = GetWordsWithSize(words);
			Bitmap image = new Bitmap(options.GetBitmapWidth(), options.GetBitmapWidth());	
			while (wordsWithSize.Count>0)
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
			var image = new Bitmap(options.GetBitmapWidth(), options.GetBitmapHeight());
			using (var g = Graphics.FromImage(image))
			{
				float currX = 0;
				float currY = 0;
				addedWordsCount = 0;
				foreach (var word in wordsWithSize.OrderBy(w => w.Key))
				{
					var colorIndex = addedWordsCount%options.GetColor().Count;
					var drawBrush = new SolidBrush(Color.FromName(options.GetColor()[colorIndex]));
					var drawFont = new Font("Arial", word.Value);
					var measuredWord = g.MeasureString(word.Key, drawFont);
					if (currX + measuredWord.Width > options.GetBitmapWidth())
					{
						currX = 0;
						currY += options.GetMaxFontSize()*SizeToPixelsCoefficient;
						//no place for this word currently
						if (currY + options.GetMaxFontSize()*SizeToPixelsCoefficient > options.GetBitmapHeight())
							break;
					}
					//no place for this word anywhere
					if (currX + measuredWord.Width > options.GetBitmapWidth())
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

		public Dictionary<string, int> GetWordsWithSize(string[] words)
		{
			var wordsWithFrequency = words
				.GroupBy(w => w)
				.OrderBy(g => -g.Count());
			var maxFreq = wordsWithFrequency.Max(g => g.Count());
			return wordsWithFrequency.ToDictionary(g => g.Key,
				g =>(int) Math.Ceiling(
					((double)g.Count() / maxFreq)*(options.GetMaxFontSize() - options.GetMinFontSize()) + options.GetMinFontSize()));
		} 
	}
}
