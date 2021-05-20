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

            var builder = new UriBuilder(url);

            if (builder.Uri.Segments.Length < 3)
                throw new ArgumentException("Either the name or the owner of the repository could not be retrieved from the URL.");

            Name = builder.Uri.Segments[2].Replace(@"/", string.Empty);
            Owner = builder.Uri.Segments[1].Replace(@"/", string.Empty);


            BaseUrl = $"https://{ builder.Host }/{ Owner }/{ Name }";
        }
    }
}
