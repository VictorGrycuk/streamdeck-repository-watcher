using Newtonsoft.Json;
using Octokit;

namespace RepositoryWatcher
{
    internal class PluginSettings
    {
        public static PluginSettings CreateDefaultSettings()
        {
            return new PluginSettings();
        }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string RepositoryURL { get; set; }

        [JsonProperty(PropertyName = "interval")]
        public int Interval { get; set; }

        [JsonProperty(PropertyName = "initialOffset")]
        public int InitialOffset { get; set; }

        [JsonProperty(PropertyName = "isEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty(PropertyName = "issueAssignee")]
        public string Assignee { get; set; }

        [JsonProperty(PropertyName = "issueCreator")]
        public string Creator { get; set; }

        [JsonProperty(PropertyName = "mentionedUser")]
        public string MentionedUser { get; set; }

        [JsonProperty(PropertyName = "milestone")]
        public string Milestone { get; set; }

        [JsonProperty(PropertyName = "baseBranch")]
        public string BaseBranch { get; set; }

        [JsonProperty(PropertyName = "head")]
        public string Head { get; set; }

        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "startPage")]
        public int StartPage { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string SelectedResourceType { get; set; }

        public ResourceType ResourceType { get; set; }

        [JsonProperty(PropertyName = "filterBy")]
        public string SelectedFilterBy { get; set; }

        public IssueFilter FilterBy { get; set; }

        [JsonProperty(PropertyName = "prSortBy")]
        public string SelectedPullRequestSortBy { get; set; }

        public PullRequestSort PullRequestSortBy { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string SelectedState { get; set; }

        public ItemStateFilter State { get; set; }
    }

    enum ResourceType
    {
        ISSUE,
        PULL_REQUEST
    }
}
