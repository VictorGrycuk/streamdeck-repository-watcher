# Considerations
## What the GitHub API returns

The GitHub API returns anything that has been **updated** within the specified time frame.

For example, if the plug in is configured to return the amount of issues with the state open with the initial offset time frame of 2 days, it will return all the open issues that has been **updated** within the last two days. It will **not** return the issues that were open in the last two day.

An issue/pull requests is considered updated if it has been:

- Created
- Commented
- Labeled
- Referenced
- Assigned
- etc.

If you want to have a better control on what is being returned and counted, use custom filtering.

## Every pull request an issue, but not every issue is a pull request

A caveat of the GitHub API is that the `GetIssues` method not only return issues but may also return pull requests as well.

According to the [GitHub documentation](https://docs.github.com/en/free-pro-team@latest/rest/reference/issues#list-issues-assigned-to-the-authenticated-user):

> GitHub's REST API v3 considers every pull request an issue, but not every issue is a pull request. For this reason, "Issues" endpoints may return both issues and pull requests in the response. You can identify pull requests by the `pull_request` key. Be aware that the `id` of a pull request returned from "Issues" endpoints will be an *issue id*. To find out the pull request id, use the "[List pull requests](https://docs.github.com/rest/reference/pulls#list-pull-requests)" endpoint.

**However**, with this in mind, the implementation of the API for this plug in returns all the issues *without an associated pull request*.

# Known Issues

## Automatic Refresh

At the moment, the automatic refresh does not work if a folder is opened or a profile switched.

I am planning to fix this in the next releases.

> :ToCPrevNext