using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Infrastructure.Language;

namespace TagCloud
{
	class PrepositionsFilter: IWordsFilter
	{
		public HashSet<string> BadWords { get; set; } = 
			new HashSet<string>(File.ReadAllText("../../prepositions.txt").Split(' ', '\n', '\r'));
		public IEnumerable<string> Filter(IEnumerable<string> words) => words.Except(BadWords);
	}
}
