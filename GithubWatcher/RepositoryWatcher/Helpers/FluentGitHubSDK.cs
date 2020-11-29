using Octokit;
using System;
using System.Collections.Generic;

namespace RepositoryWatcher.Helpers
{
    internal sealed class FluentGitHubSDK :
        IFromRepository,
        IWithOwner,
        IGetResources,
        IWithIssueOptions,
        IWithPullRequestOptions
    {
        private string repositoryName;
        private string repositoryOwner;
        private readonly GitHubClient client;
        private RepositoryIssueRequest repositoryIssueRequest;
        private PullRequestRequest pullRequestRequest;

        private FluentGitHubSDK(string token)
        {
            client = new GitHubClient(new ProductHeaderValue("StreamDeck-GitHubWatcher"))
            {
                Credentials = new Credentials(token)
            };
        }
        
        private FluentGitHubSDK()
        {
            client = new GitHubClient(new ProductHeaderValue("StreamDeck-GitHubWatcher"));
        }

        internal static IFromRepository WithCredentials(string token) => !string.IsNullOrWhiteSpace(token) ? new FluentGitHubSDK(token) : new FluentGitHubSDK();
        internal static IFromRepository WithoutCredentials() => new FluentGitHubSDK();

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
                        repositoryIssueRequest)
                    .Result;
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

        IReadOnlyList<PullRequest> IWithPullRequestOptions.GetResult()
        {
            return client
                    .PullRequest
                    .GetAllForRepository(
                        repositoryOwner,
                        repositoryName,
                        pullRequestRequest)
                    .Result;
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

    interface IGetResources
    {
        IWithIssueOptions GetIssues();
        IWithPullRequestOptions GetPullRequests();
    }

    interface IIntermediate
    {
        IWithIssueOptions FilteredBy(string filter);
    }

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
        IReadOnlyList<Issue> GetResult();
    }

    interface IWithPullRequestOptions
    {
        IWithPullRequestOptions WithBaseBranch(string baseBranchName);
        IWithPullRequestOptions WithHead(string head);
        IWithPullRequestOptions SortBy(PullRequestSort sortProperty);
        IWithPullRequestOptions WithSortDirection(SortDirection direction);
        IWithPullRequestOptions WithState(ItemStateFilter state);
        IReadOnlyList<PullRequest> GetResult();
    }
}
