# Repository Watcher for StreamDeck
## Description
This is a plug in for [StreamDeck](https://www.elgato.com/en/gaming/stream-deck) that will monitor the count of issues or pull request of a given GitHub repository.

The plug in will check the repository and show the count of either issues or pull requests that meet the filter criteria.

See the features below for more details.

This was done using BarRaider's [Stream Deck Tools](https://github.com/BarRaider/streamdeck-tools).

## Features

- At the moment can monitor **Issues** and **Pull Requests**
- Press the StreamDeck key to open the repository issues/pull requests page
- It can use a **GitHub Personal Access Token** to avoid reaching the limit for unauthenticated calls (or don't if you just want to test the plug in)
- Custom refresh interval
- Several filtering configuration. See each watcher for more info

## Configuration

### Shared Configuration

- **(Optional) GitHub Personal Token.** If you don't use a personal token, you are limited to 60 API calls per hour. If you use a personal token, the limit is raised to 5000 calls per hour
  - Refer to [Rest API Rate Limiting](https://docs.github.com/en/free-pro-team@latest/rest/overview/resources-in-the-rest-api#rate-limiting) documentation for more information
  - Refer to [Creating a personal access token](https://docs.github.com/en/free-pro-team@latest/github/authenticating-to-github/creating-a-personal-access-token) documentation for more information
- **Repository URL.** Simply the URL of the repository to monitor. It just need to be in the format `https://github.com/{repo-owner}/{repo-name}`
- **Refresh Interval.** How often you want the plug in to refresh the information, expressed in minutes. Default value is `30`.
- **Enabled.** Checking this will enable the plug in
- **Page Count, Page Size, Start Page.** This is the pagination for the GitHub API, to avoid hang ups when retrieving information from repositories with thousands of issues and pull requests
  - **Page Count** is the amount of pages you want the API to return. Default is `1`
  - **Page Size** is the amount of items you want per page. Default is `30`
  - **Start Page** is starting from which page you want the results (I can't find it an use for this plug in, but doesn't hurt to add the option anyway). Default is `1`
  - **Note:** Have in mind that having 1 page of size 60 is essentially the same as 2 pages of size 30
  - Refer to [Traversing with pagination](https://docs.github.com/en/free-pro-team@latest/rest/guides/traversing-with-pagination) for more information
- **Custom Filters**. See custom filters section for more information

### Issue Watcher

![](/images/issues-watcher.jpg)

All of these are optional filters that can be stacked.

- **Initial Offset.** When first launching the plug in, it will retrieve a certain amount of information from the repository. The offset specifies how far back it will retrieve information the first time, expressed in days. Default is `2`.
  - **Note:** After it checks the repository, that date time will become the new offset, same as when pressing the key. The logic behind this is that after opening the website, you have already seen the new repository updates, therefore it will start counting since you last saw the repository.

- **Issue Assignee.** Will return the amount of issues with a given assignee
- **Issue Creator.** Will return the amount of issues that were created by a given user
- **Mentioned User.** Will return the amount of issues where a given user was mentioned
- **Milestone.** Will return the amount of issues associated with a given milestone
- **Issue Creator.** Will return the amount of issues that were created by a given user
- **Filter By.** Will return the amount of issues according to the selected option:
  - **All**. No filter will be applied. Default value.
  - **Assigned**. Will return the amount of issues assigned to the authenticated user
  - **Created**. Will return the amount of issues created by the authenticated user
  - **Mentioned**. Will return the amount of issues where the authenticated user is mentioned
  - **Subscribed**. Will return the amount of issues that the authenticated user has subscribed to
- **State**. Will return the amount of issues according to the selected state:
  - **All**. No filter will be applied. Default value.
  - **Open**. Will count the amount of open issues
  - **Closed**. Will count the amount of closed issues

### Pull Request Watcher
![](/images/pullrequests-watcher.jpg)

All of these are optional filters that can be stacked.

The GitHub API is more limited regarding on the filters that can be applied to the pull requests request, so if you wish to fine tune the counting of pull requests,  you will have to use the custom filters.

- **Base Branch.** Will return the amount of pull requests with a given base branch
- **Head.** Will return the amount of pull requests with a given head
- **State**. Will return the amount of pull requests according to the selected state:
  - **All**. No filter will be applied. Default value.
  - **Open**. Will count the amount of open pull requests
  - **Closed**. Will count the amount of closed pull requests

## Considerations

#### What the GitHub API returns

The API returns anything that has been **update** within the specified time frame.

For example, if the plug in is configured to return the amount of issues with the state open with the initial offset time frame of 2 days, it will return all the open issues that has been **updated** within the last two days. It will **not** return the issues that were open in the last two day.

An issue/pull requests is considered updated if it has been:

- Created
- Commented
- Labeled
- Referenced
- Assigned
- etc.

If you want to have a better control on what is being returned and counted, use custom filtering.

#### Every pull request an issue, but not every issue is a pull request

A caveat of the GitHub API, the `GetIssues` method not only return issues but may also return pull requests as well.

According to the [GitHub documentation](https://docs.github.com/en/free-pro-team@latest/rest/reference/issues#list-issues-assigned-to-the-authenticated-user):

> GitHub's REST API v3 considers every pull request an issue, but not every issue is a pull request. For this reason, "Issues" endpoints may return both issues and pull requests in the response. You can identify pull requests by the `pull_request` key. Be aware that the `id` of a pull request returned from "Issues" endpoints will be an *issue id*. To find out the pull request id, use the "[List pull requests](https://docs.github.com/rest/reference/pulls#list-pull-requests)" endpoint.

**However**, with this in mind, the implementation of the API for this plug in returns all the issues *without an associated pull request*.

## Custom Filters

### Description

Since the GitHub API is quite limited on regards the filters that can be applied to the requests, specially for the pull request, I added the possibility to apply custom filters using [Roslyn scripting](https://www.strathweb.com/2018/01/easy-way-to-create-a-c-lambda-expression-from-a-string-with-roslyn/) to the result of the call.

These filters are basically lambdas expressions that are applied to the result of the GitHub request sequentially. This, of course, requires a basic knowledge of the models used by the [Octokit.net library](https://github.com/octokit/octokit.net/tree/main/Octokit/Models/Response).

Since Roslyn executes the scripts inside a sandbox, it requires to also specify the imports directives. The most commons are `Octokit`, `System`, and `System.Linq`, and others depending your need.

- The first line of the Custom Filter text area is reserved for the import directives, they have to be comma-separated

- The lambdas can be declared starting the second line
- All the lines have to end with `;`

### Examples

If you wanted to count only the pull requests or issues that were open within the last 2 days, you can use:

```c#
Octokit,System,System.Linq,System.Collections.Generic;
x => x.CreatedAt > DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0));
```

If you wanted to count only the pull requests or issues that have the label `Team: SuperTeam`, you can use:

```c#
Octokit,System,System.Linq,System.Collections.Generic;
x => x.Labels.Any(x => new string[] { \"Team: SuperTeam\" }.Contains(x.Name));
```

If you wanted to count only the pull requests that are merged *and* updated within the last hour, you can use:

```c#
Octokit,System,System.Linq,System.Collections.Generic;
x => x.Merged == true;
x => x.UpdatedAt > DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
```

### Considerations

- The scripting require basic knowledge of the C# language
- Each expression is parsed as if it were a C# code, so all the rules for the language applies
- They are applied **sequentially**. Imagine that that each expression is joined by an *AND* to the next expression
- Be aware that parsing and executing the expressions can be expensive, computationally speaking. Try to use them only if needed and nothing too crazy

## My others Stream Deck plugins

- **[KeePass for StreamDeck](https://github.com/VictorGrycuk/StreamDeck-KeePass)**
- **[Color Picker](https://github.com/VictorGrycuk/streamdeck-color-picker)**
- **[Magnifier](https://github.com/VictorGrycuk/streamdeck-magnifier)**

---
The icons are modified version of *Repository iOS Glyph* and *Repository Fluent* at [Icon8](https://icons8.com/).