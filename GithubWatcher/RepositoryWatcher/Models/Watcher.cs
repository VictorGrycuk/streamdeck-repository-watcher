using BarRaider.SdTools;
using RepositoryWatcher.Helpers;
using System;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    internal class Watcher
    {
        protected string RepositoryName { get; set; }
        protected string RepositoryOwner { get; set; }
        protected Point NumberLocation { get; set; }
        protected Point NumberMaxDimension { get; set; }
        protected Point DescriptionLocation { get; set; }
        protected Point DescriptionMaxLocation { get; set; }
        protected PluginSettings Settings { get; set; }

        public Watcher(PluginSettings settings)
        {
            Settings = settings;
            var repositoryUri = new UriBuilder(settings.RepositoryURL).Uri;
            RepositoryName = repositoryUri.Segments[2].Replace(@"/", string.Empty);
            RepositoryOwner = repositoryUri.Segments[1].Replace(@"/", string.Empty);
            NumberLocation = new Point { X = 72, Y = 72 };
            NumberMaxDimension = new Point { X = 142, Y = 100 };
            DescriptionLocation = new Point { X = 72, Y = 110 };
            DescriptionMaxLocation = new Point { X = 142, Y = 35 };
        }

        private static Bitmap GetBaseImage()
        {
            var bmp = Tools.GenerateGenericKeyImage(out Graphics graphics);
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmp.Width, bmp.Height);

            return bmp;
        }

        protected Bitmap SetResultAndDescription(int number, string description)
        {
            var img = GetBaseImage();
            img = ImageHelper.WriteOnImage(img, number.ToString(), NumberLocation, NumberMaxDimension);
            img = ImageHelper.WriteOnImage(img, description, DescriptionLocation, DescriptionMaxLocation);

            return img;
        }
    }
}