using System;

namespace RepositoryWatcher.Models
{
    internal static class WatcherFactory
    {
        internal static IWatcher GetWatcher(PluginSettings settings)
        {
            switch (settings.ResourceType)
            {
                case global::RepositoryWatcher.ResourceType.ISSUE:
                    return new IssueWatcher(settings);
                case global::RepositoryWatcher.ResourceType.PULL_REQUEST:
                    return new PullRequestWatcher(settings);
                default:
                    throw new NotImplementedException("Resource type not supported");
            }
        }
    }
}
