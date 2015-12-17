using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
	public interface IOptions
	{
		string GetInputFile();
		string GetOutputFile();
		int GetBitmapWidth();
		int GetBitmapHeight();
		IList<string> GetColor();
		int GetMaxFontSize();
		int GetMinFontSize();
		string GetOutputFormat();

	}
}
