using RepositoryWatcher.Helpers.FluentGitHub;
using System;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    internal class PullRequestWatcher : Watcher, IWatcher
    {
        public PullRequestWatcher(PluginSettings settings) : base(settings) { }

        private int GetResult(DateTimeOffset dateTimeOffset)
        {
            return FluentGitHubSDK
                    .WithCredentials(Settings.Token)
                    .FromRepository(RepositoryName)
                    .WithOwner(RepositoryOwner)
                    .GetPullRequests()
                        .WithBaseBranch(Settings.BaseBranch)
                        .WithHead(Settings.Head)
                        .WithState(Settings.State)
                        .WithPageCount(Settings.PageCount)
                        .WithPageSize(Settings.PageSize)
                        .WithStartPage(Settings.StartPage)
                    .GetResult()
                    .Count;
        }

        public Bitmap GetImage(DateTimeOffset dateTimeOffset)
        {
            var result = GetResult(dateTimeOffset);
            var img = SetResultAndDescription(result, GetDescription(result));

            return img;
        }

        private static string GetDescription(int result)
        {
            return result == 1 ? "PR" : "PR's";
        }
    }
}
