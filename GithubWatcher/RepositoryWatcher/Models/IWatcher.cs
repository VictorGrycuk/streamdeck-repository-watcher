using System;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    interface IWatcher
    {
        Bitmap GetImage(DateTimeOffset dateTimeOffset);
        void GoToRepository();
    }
}
