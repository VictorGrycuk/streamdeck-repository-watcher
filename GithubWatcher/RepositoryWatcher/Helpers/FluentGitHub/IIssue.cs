using Octokit;
using System;
using System.Collections.Generic;

namespace RepositoryWatcher.Helpers.FluentGitHub
{
    interface IWithIssueOptions
    {
        IWithIssueOptions WithAssignee(string assigneeName);
        IWithIssueOptions WithCreator(string creatorName);
        IWithIssueOptions FilteredBy(IssueFilter issueFilter);
        IWithIssueOptions WithMentionedUser(string mentionedUser);
        IWithIssueOptions WithMilestone(string milestone);
        IWithIssueOptions Since(DateTimeOffset since);
        IWithIssueOptions SortBy(IssueSort sortProperty);
        IWithIssueOptions WithSortDirection(SortDirection direction);
        IWithIssueOptions WithState(ItemStateFilter state);
        IWithIssueOptions WithPageCount(int pageCount);
        IWithIssueOptions WithPageSize(int pageSize);
        IWithIssueOptions WithStartPage(int startPage);
        IReadOnlyList<Issue> GetResult();
    }
}
