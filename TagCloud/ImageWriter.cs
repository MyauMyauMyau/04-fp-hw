using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
	public class ImageWriter: IImageWriter
	{
		private readonly string path;
		private readonly string format;

		public ImageWriter(string path, string format)
		{
			this.path = path;
			this.format = format;
		}

		public void WriteImage(Bitmap bitmap)
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