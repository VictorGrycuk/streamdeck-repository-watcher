using Octokit;
using System;
using System.Collections.Generic;

namespace RepositoryWatcher.Models
{
    public class Resource
    {
        public ResourceType ResourceType { get; set; }
        public IReadOnlyList<User> Assignees { get; set; }
        public int Number { get; set; }
        public string Body { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public User ClosedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string EventsUrl { get; set; }
        public string HtmlUrl { get; set; }
        public IReadOnlyList<Label> Labels { get; set; }
        public StringEnum<ItemState> State { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public User User { get; set; }
    }

    public enum ResourceType
    {
        ISSUE,
        PR
    }
}
