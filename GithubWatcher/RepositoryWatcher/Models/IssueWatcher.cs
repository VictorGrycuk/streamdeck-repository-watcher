using RepositoryWatcher.Helpers.FluentGitHub;
using System;
using System.Drawing;
using System.Linq;

namespace RepositoryWatcher.Models
{
    internal class IssueWatcher : Watcher, IWatcher
    {
        public IssueWatcher(PluginSettings settings) : base(settings) { }

        private int GetResult(DateTimeOffset dateTimeOffset)
        {
            var items = FluentGitHubAPI
                    .WithCredentials(Settings.Token)
                    .FromRepository(Repository.Name)
                    .WithOwner(Repository.Owner)
                    .GetIssues()
                        .FilteredBy(Settings.FilterBy)
                        .WithAssignee(Settings.Assignee)
                        .WithCreator(Settings.Creator)
                        .WithMentionedUser(Settings.MentionedUser)
                        .WithMilestone(Settings.MentionedUser)
                        .WithState(Settings.State)
                        .Since(dateTimeOffset)
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
            return Repository.BaseUrl + "/issues";
        }

        private static string GetDescription(int result)
        {
            return result == 1 ? "Issue" : "Issues";
        }
    }
}
