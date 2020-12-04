using RepositoryWatcher.Helpers.FluentGitHub;
using System;
using System.Drawing;
using System.Linq;

namespace RepositoryWatcher.Models
{
    internal class PullRequestWatcher : Watcher, IWatcher
    {
        public PullRequestWatcher(PluginSettings settings) : base(settings) { }

        private int GetResult(DateTimeOffset dateTimeOffset)
        {
            var items = FluentGitHubAPI
                    .WithCredentials(Settings.Token)
                    .FromRepository(Repository.Name)
                    .WithOwner(Repository.Owner)
                    .GetPullRequests()
                        .WithBaseBranch(Settings.BaseBranch)
                        .WithHead(Settings.Head)
                        .WithState(Settings.State)
                        .WithPageCount(Settings.PageCount)
                        .WithPageSize(Settings.PageSize)
                        .WithStartPage(Settings.StartPage)
                    .GetResult();

            items = ApplyCustomFilters(items.ToList());

            return items.Count;
        }

        public Bitmap GetImage(DateTimeOffset dateTimeOffset)
        {
            var result = GetResult(dateTimeOffset);
            var img = SetResultAndDescription(result, GetDescription(result));

            return img;
        }

        public string GetUrl()
        {
            return Repository.BaseUrl + "/pulls";
        }

        private static string GetDescription(int result)
        {
            return result == 1 ? "PR" : "PR's";
        }
    }
}
