using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
	class TagCloudBuilder: ITagCloudBuilder
	{
		private readonly IWordsReader reader;
		private readonly IAlgorithm algorithm;
		private readonly IImageWriter writer;
		private readonly IWordsFilter filter;
		public TagCloudBuilder(IWordsReader reader, IAlgorithm algorithm, IImageWriter writer, IWordsFilter filter)
		{
			this.filter = filter;
			this.reader = reader;
			this.algorithm = algorithm;
			this.writer = writer;
		}
		public void Build()
		{
			var words = filter.Filter(reader.ReadWords()).ToArray();
			var cloud = algorithm.BuildTagCloud(words);
			writer.WriteImage(cloud);
		}
	}
}
