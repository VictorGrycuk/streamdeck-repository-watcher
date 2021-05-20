using System;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    interface IWatcher
    {
        Bitmap GetImage();
        string GetUrl();
    }
}
