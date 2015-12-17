using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
	class SimpleWordsReader: IWordsReader
	{
		private readonly string path;

		public SimpleWordsReader(string path)
		{
			this.path = path;
		}

		public string[] ReadWords()
		{
			var text = File.ReadAllText(path).ToLower();
			return text.Split(' ');
		}
	}
}
