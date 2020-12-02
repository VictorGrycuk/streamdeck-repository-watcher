using Octokit;
using System.Collections.Generic;

namespace RepositoryWatcher.Helpers.FluentGitHub
{
    interface IWithPullRequestOptions
    {
        IWithPullRequestOptions WithBaseBranch(string baseBranchName);
        IWithPullRequestOptions WithHead(string head);
        IWithPullRequestOptions SortBy(PullRequestSort sortProperty);
        IWithPullRequestOptions WithSortDirection(SortDirection direction);
        IWithPullRequestOptions WithState(ItemStateFilter state);
        IWithPullRequestOptions WithPageCount(int pageCount);
        IWithPullRequestOptions WithPageSize(int pageSize);
        IWithPullRequestOptions WithStartPage(int startPage);
        IReadOnlyList<PullRequest> GetResult();
    }
}
