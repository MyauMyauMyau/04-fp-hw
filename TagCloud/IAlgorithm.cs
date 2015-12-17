using System.Drawing;

namespace TagCloud
{
	internal interface IAlgorithm
	{
		Bitmap BuildTagCloud(string[] words);
	}
}