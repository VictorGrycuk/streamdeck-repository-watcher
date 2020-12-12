# Getting Started

> [info](:Icon) The plug in uses the Github API to communicate, therefore it is encouraged to use a **GitHub Personal Access Token**.

## Basic Configuration

After adding the plug in you will see several options to configure:

- **(Optional) GitHub Personal Token.** If you don't use a personal token, you are limited to 60 API calls per hour. If you use a personal token, the limit is raised to 5000 calls per hour
  - Refer to [Creating a personal access token](https://docs.github.com/en/free-pro-team@latest/github/authenticating-to-github/creating-a-personal-access-token) documentation for more information
  - Refer to [Rest API Rate Limiting](https://docs.github.com/en/free-pro-team@latest/rest/overview/resources-in-the-rest-api#rate-limiting) documentation for more information
- **Repository URL.** Simply the URL of the repository to monitor. It just need to be in the format `https://github.com/{repo-owner}/{repo-name}`
- **Refresh Interval.** How often you want the plug in to refresh the information automatically, expressed in minutes. Default value is `30`.
- **Enabled.** Checking this will enable the automatic update

See also [Known Issues](./docs/known-issues).

## Advanced Configuration

These configuration revolve mainly around the pagination for the GitHub API, to avoid hang ups when retrieving information from repositories with thousands of issues and pull requests.

> [warning](:Icon) Only increase these values if you think you are reaching the limit of the default configuration.

- **Page Count** is the amount of pages you want the API to return. Default is `1`
- **Page Size** is the amount of items you want per page. Default is `30`
- **Start Page** is starting from which page you want the results (I can't find it an use for this plug in, but doesn't hurt to add the option anyway). Default is `1`
	- [info](:Icon) Have in mind that having 1 page of size 60 is essentially the same as 2. Refer to [Traversing with pagination](https://docs.github.com/en/free-pro-team@latest/rest/guides/traversing-with-pagination) for more information

- **Custom Filters**. See the [Custom Filters](./custom-filters) section for more information



> :ToCPrevNext