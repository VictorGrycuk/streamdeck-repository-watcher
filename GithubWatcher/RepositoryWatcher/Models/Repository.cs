using System;

namespace RepositoryWatcher.Models
{
    public class Repository
    {
        public string BaseUrl { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        public Repository(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("The URL for the repository cannot be empty.");
            
            var uri = new UriBuilder(url).Uri;

            if (uri.Segments.Length < 3)
                throw new ArgumentException("Either the name or the owner of the repository could not be retrieved from the URL.");

            Name = uri.Segments[2].Replace(@"/", string.Empty);
            Owner = uri.Segments[1].Replace(@"/", string.Empty);
            BaseUrl = $"https://github.com/{ Owner }/{ Name }";
        }
    }
}
