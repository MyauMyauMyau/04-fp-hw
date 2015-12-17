using System.Drawing;

namespace TagCloud
{
	public interface IImageWriter
	{
		void WriteImage(Bitmap bitmap);
	}
}