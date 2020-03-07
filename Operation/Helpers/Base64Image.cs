using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;



namespace WhatsAppApi.Operation.Helpers
{
    public class Base64Image
    {
        private readonly ImageFormat _format;

        public string ContentType { get; set; }
        public byte[] FileContents { get; set; }


        ~Base64Image()
        {
            ContentType = default;
            FileContents = default;
        }
        public Base64Image() { }

        public Base64Image(Image image, bool needThumbnail = false)
        {
            _format = image.RawFormat;
            FileContents = !needThumbnail ? ConvertImageToBase64(image) : ConvertAndResizeImageToBase64(image);
        }

        public Base64Image(string imagePath, bool needThumbnail = false)
        {
            if (Uri.IsWellFormedUriString(imagePath, UriKind.RelativeOrAbsolute))
            {
                using (var image = LoadedBitmap(imagePath))
                {
                    _format = image.RawFormat;
                    FileContents = !needThumbnail ? ConvertImageToBase64(image) : ConvertAndResizeImageToBase64(image);
                }
            }
            else
            {
                using (var image = Image.FromFile(imagePath))
                {
                    _format = image.RawFormat;
                    FileContents = !needThumbnail ? ConvertImageToBase64(image) : ConvertAndResizeImageToBase64(image);
                }
            }
        }


        private static Bitmap LoadedBitmap(string imagePath)
        {
            var request = WebRequest.Create(imagePath);
            var response = request.GetResponse();
            Bitmap loadedBitmap;
            using (var responseStream = response.GetResponseStream())
            {
                loadedBitmap = new Bitmap(responseStream);
                responseStream?.Dispose();
            }

            return loadedBitmap;
        }
        private byte[] ConvertImageToBase64(Image image)
        {
            ContentType = ContentTypeDetect(image);

            using (var mStream = new MemoryStream())
            {
                image?.Save(mStream, _format);
                return mStream.ToArray();
            }
        }
        private byte[] ConvertAndResizeImageToBase64(Image image)
        {
            var bothSize = 300;
            var base64Size = ConvertImageToBase64(image);

            while (Convert.ToBase64String(base64Size).Length > 20000)
            {
                using (var img = ResizeImage(image, new Size(bothSize, bothSize)))
                    base64Size = ConvertImageToBase64(img);

                bothSize -= 50;
            }

            return base64Size;
        }
        private static string ContentTypeDetect(Image image)
        {
            if (image.RawFormat.Equals(ImageFormat.Jpeg)) return "image/jpeg";
            if (image.RawFormat.Equals(ImageFormat.Png)) return "image/png";
            if (image.RawFormat.Equals(ImageFormat.Tiff)) return "image/tiff";
            if (image.RawFormat.Equals(ImageFormat.Bmp)) return "image/bmp";
            if (image.RawFormat.Equals(ImageFormat.Gif)) return "image/gif";

            return string.Empty;
        }
        public static Base64Image Parse(string base64Content)
        {
            if (string.IsNullOrEmpty(base64Content)) return null;

            var indexOfSemiColon = base64Content.IndexOf(";", StringComparison.OrdinalIgnoreCase);

            var dataLabel = base64Content.Substring(0, indexOfSemiColon);

            var contentType = dataLabel.Split(':').Last();

            var startIndex = base64Content.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;

            var fileContents = base64Content.Substring(startIndex);

            var bytes = Convert.FromBase64String(fileContents);

            return new Base64Image
            {
                ContentType = contentType,
                FileContents = bytes
            };
        }
        private static Image ResizeImage(Image image, Size size)
        {
            var sourceWidth = image.Width;
            var sourceHeight = image.Height;

            var nPercentW = (decimal)size.Width / sourceWidth;
            var nPercentH = (decimal)size.Height / sourceHeight;

            var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);



            var b = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(b);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, 0, 0, destWidth, destHeight);
            g.Dispose();


            return b;
        }


        public static explicit operator Base64Image(string x) => new Base64Image(x);


        public override string ToString()
        {
            return $"data:{ContentType};base64,{Convert.ToBase64String(FileContents)}";
        }
    }
}