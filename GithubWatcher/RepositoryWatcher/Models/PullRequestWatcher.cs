using RepositoryWatcher.Helpers;
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
