using BarRaider.SdTools;
using RepositoryWatcher.Helpers;
using System.Collections.Generic;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    internal class Watcher
    {
        protected readonly CustomFilter CustomFilter;
        protected readonly Repository Repository;
        protected Point NumberLocation { get; set; }
        protected Point NumberMaxDimension { get; set; }
        protected Point DescriptionLocation { get; set; }
        protected Point DescriptionMaxLocation { get; set; }
        protected PluginSettings Settings { get; set; }

        public Watcher(PluginSettings settings)
        {
            Settings = settings;
            Repository = new Repository(settings.RepositoryURL);
            NumberLocation = new Point { X = 72, Y = 72 };
            NumberMaxDimension = new Point { X = 142, Y = 100 };
            DescriptionLocation = new Point { X = 72, Y = 110 };
            DescriptionMaxLocation = new Point { X = 142, Y = 35 };

            CustomFilter = CustomFilter.CreateNewCustomFilter(settings.CustomFilters);
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

        protected List<T> ApplyCustomFilters<T>(List<T> items)
        {
            return CustomFilter != null
                ? CustomFilter.ApplyFilters(items)
                : items;
        }
    }
}