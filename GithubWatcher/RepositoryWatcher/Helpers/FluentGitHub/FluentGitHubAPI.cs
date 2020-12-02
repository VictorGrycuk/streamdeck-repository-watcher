using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryWatcher.Helpers.FluentGitHub
{
    // This class is far from optimal, but it will do for the moment
    internal sealed class FluentGitHubAPI :
        IFromRepository,
        IWithOwner,
        IGetResources,
        IWithIssueOptions,
        IWithPullRequestOptions
    {
        private string repositoryName;
        private string repositoryOwner;
        private readonly GitHubClient client = new GitHubClient(new ProductHeaderValue("StreamDeck-GitHubWatcher"));
        private readonly ApiOptions apiOptions = new ApiOptions { PageCount = 1, PageSize = 30, StartPage = 1 };
        private RepositoryIssueRequest repositoryIssueRequest;
        private PullRequestRequest pullRequestRequest;

        private FluentGitHubAPI(string token)
        {
            client.Credentials = new Credentials(token);
        }
        
        private FluentGitHubAPI() { }

        internal static IFromRepository WithCredentials(string token) => !string.IsNullOrWhiteSpace(token) ? new FluentGitHubAPI(token) : new FluentGitHubAPI();
        internal static IFromRepository WithoutCredentials() => new FluentGitHubAPI();

        public IWithOwner FromRepository(string repositoryName)
        {
            this.repositoryName = repositoryName;

            return this;
        }

        public IGetResources WithOwner(string repositoryOwner)
        {
            this.repositoryOwner = repositoryOwner;

            return this;
        }

        public IWithIssueOptions GetIssues()
        {
            repositoryIssueRequest = new RepositoryIssueRequest();

            return this;
        }

        public IWithPullRequestOptions GetPullRequests()
        {
            pullRequestRequest = new PullRequestRequest();

            return this;
        }

        public IWithIssueOptions WithAssignee(string assigneeName)
        {
            if(!string.IsNullOrWhiteSpace(assigneeName))
                repositoryIssueRequest.Assignee = assigneeName;

            return this;
        }

        public IWithIssueOptions WithCreator(string creatorName)
        {
            if (!string.IsNullOrWhiteSpace(creatorName))
                repositoryIssueRequest.Creator = creatorName;

            return this;
        }

        public IWithIssueOptions FilteredBy(IssueFilter issueFilter)
        {
            repositoryIssueRequest.Filter = issueFilter;

            return this;
        }

        public IWithIssueOptions WithMentionedUser(string mentionedUser)
        {
            if (!string.IsNullOrWhiteSpace(mentionedUser))
                repositoryIssueRequest.Mentioned = mentionedUser;

            return this;
        }

        public IWithIssueOptions WithMilestone(string milestone)
        {
            if (!string.IsNullOrWhiteSpace(milestone))
                repositoryIssueRequest.Milestone = milestone;

            return this;
        }

        public IWithIssueOptions Since(DateTimeOffset since)
        {
            if (since != null)
                repositoryIssueRequest.Since = since;

            return this;
        }

        public IWithIssueOptions SortBy(IssueSort property)
        {
            repositoryIssueRequest.SortProperty = property;

            return this;
        }

        public IWithIssueOptions WithSortDirection(SortDirection direction)
        {
            repositoryIssueRequest.SortDirection = direction;

            return this;
        }

        public IWithIssueOptions WithState(ItemStateFilter state)
        {
            repositoryIssueRequest.State = state;

            return this;
        }

        public IReadOnlyList<Issue> GetResult()
        {
            return client
                    .Issue
                    .GetAllForRepository(
                        repositoryOwner,
                        repositoryName,
                        repositoryIssueRequest,
                        apiOptions)
                    .Result
                    // Filter our those issues that are actually a pull request
                    .Where(issue => issue.PullRequest == null).ToList();
        }

        public IWithPullRequestOptions WithBaseBranch(string baseBranchName)
        {
            if (!string.IsNullOrWhiteSpace(baseBranchName))
                pullRequestRequest.Base = baseBranchName;

            return this;
        }

        public IWithPullRequestOptions WithHead(string head)
        {
            if (!string.IsNullOrWhiteSpace(head))
                pullRequestRequest.Head = head;

            return this;
        }

        public IWithPullRequestOptions SortBy(PullRequestSort sortProperty)
        {
            pullRequestRequest.SortProperty = sortProperty;

            return this;
        }

        IWithPullRequestOptions IWithPullRequestOptions.WithSortDirection(SortDirection direction)
        {
            pullRequestRequest.SortDirection = direction;

            return this;
        }

        IWithPullRequestOptions IWithPullRequestOptions.WithState(ItemStateFilter state)
        {
            pullRequestRequest.State = state;

            return this;
        }

        public IWithIssueOptions WithPageCount(int pageCount)
        {
            apiOptions.PageCount = pageCount;

            return this;
        }

        public IWithIssueOptions WithPageSize(int pageSize)
        {
            apiOptions.PageSize = pageSize;

            return this;
        }

        public IWithIssueOptions WithStartPage(int startPage)
        {
            apiOptions.StartPage = startPage;

            return this;
        }

        IReadOnlyList<PullRequest> IWithPullRequestOptions.GetResult()
        {
            return client
                    .PullRequest
                    .GetAllForRepository(
                        repositoryOwner,
                        repositoryName,
                        pullRequestRequest,
                        apiOptions)
                    .Result;
        }

        IWithPullRequestOptions IWithPullRequestOptions.WithPageCount(int pageCount)
        {
            apiOptions.PageCount = pageCount;

            return this;
        }

        IWithPullRequestOptions IWithPullRequestOptions.WithPageSize(int pageSize)
        {
            apiOptions.PageSize = pageSize;

            return this;
        }

        IWithPullRequestOptions IWithPullRequestOptions.WithStartPage(int startPage)
        {
            apiOptions.StartPage = startPage;

            return this;
        }
    }

    interface IFromRepository
    {
        IWithOwner FromRepository(string repositoryName);
    }

    interface IWithOwner
    {
        IGetResources WithOwner(string repositoryOwner);
    }

    interface IIntermediate
    {
        IWithIssueOptions FilteredBy(string filter);
    }

    interface IGetResources
    {
        IWithIssueOptions GetIssues();
        IWithPullRequestOptions GetPullRequests();
    }
}
