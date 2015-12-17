using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
	internal interface IWordsFilter
	{
		HashSet<string> BadWords { get; set; }
		IEnumerable<string> Filter(IEnumerable<string> words);
	}
}
