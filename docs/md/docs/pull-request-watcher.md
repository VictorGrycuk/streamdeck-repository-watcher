# Pull Request Watcher
## Configuration
All of the following options are optional filters that can be stacked.

The GitHub API is more limited regarding on the filters that can be applied to the pull requests request, so if you wish to fine tune the counting of pull requests, you will have to use the [Custom Filters](./custom-filters).

> [info](:Icon) Whenever the documentation refers to *user* or *username* it refers to the GitHub login name

| Field              | Description                                                  |
| ------------------ | ------------------------------------------------------------ |
| **Base Branch**    | Configuring this field will return the amount of pull requests with a given base branch |
| **Head**           | Configuring this field will return the amount of pull requests with a given head branch |
| **State**          | Configuring this field will return the amount of issues according to the selected state |
| (State) **All**    | No filter will be applied. Default value.                    |
| (State) **Open**   | It will count the amount of open pull requests               |
| (State) **Closed** | It will count the amount of closed pull requests             |
<br>

> See [Considerations](/considerations-known-issues) for more details.

> :ToCPrevNext

