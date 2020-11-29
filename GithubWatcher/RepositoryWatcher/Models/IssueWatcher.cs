using RepositoryWatcher.Helpers;
using System;
using System.Drawing;

namespace RepositoryWatcher.Models
{
    internal class IssueWatcher : Watcher, IWatcher
    {
        public IssueWatcher(PluginSettings settings) : base(settings) { }

        private int GetResult(DateTimeOffset dateTimeOffset)
        {
            return FluentGitHubSDK
                    .WithCredentials(Settings.Token)
                    .FromRepository(RepositoryName)
                    .WithOwner(RepositoryOwner)
                    .GetIssues()
                        .FilteredBy(Settings.FilterBy)
                        .WithAssignee(Settings.Assignee)
                        .WithCreator(Settings.Creator)
                        .WithMentionedUser(Settings.MentionedUser)
                        .WithMilestone(Settings.MentionedUser)
                        .WithState(Settings.State)
                        .Since(dateTimeOffset)
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
            return result == 1 ? "Issue" : "Issues";
        }
    }
}
