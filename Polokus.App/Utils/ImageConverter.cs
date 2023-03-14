using Svg;
using System.Drawing.Imaging;
using System.Text;

namespace Polokus.App.Utils
{
    public static class ImageConverter
    {
        public static void SavePngFromSvg(string svgString)
        {
            var byteArray = Encoding.ASCII.GetBytes(svgString);
            using (var stream = new MemoryStream(byteArray))
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                var bitmap = svgDocument.Draw();

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                bitmap.Save(Path.Combine(desktopPath, "diagram.png"), ImageFormat.Png);
            }
        }

        public static Bitmap GetBitmapFromSvg(string svgString)
        {
            var byteArray = Encoding.ASCII.GetBytes(svgString);
            using (var stream = new MemoryStream(byteArray))
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                var bitmap = svgDocument.Draw();
                return bitmap;
            }
        }
    }
}
