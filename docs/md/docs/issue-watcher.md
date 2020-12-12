# Issue Watcher
## Configuration
All of the following options are optional filters that can be stacked.

> [info](:Icon) Whenever the documentation refers to *user* or *username* it refers to the GitHub login name

| Field                      | Description                                                  |
| -------------------------- | ------------------------------------------------------------ |
| **Initial Offset**         | When first launching the plug in, it will retrieve a certain amount of information from the repository. The offset specifies how far back it will retrieve information the first time, expressed in days. Default is `2`. |
| **Issue Assignee**         | Configuring this field will return the amount of issues with the given assignee. |
| **Issue Creator**          | Configuring this field will return the amount of issues that were created by the given user. |
| **Mentioned User**         | Configuring this field will return the amount of issues where the given user was mentioned. |
| **Milestone**              | Configuring this field will return the amount of issues associated with the given milestone. |
| **Filter By**              | Configuring this field will return the amount of issues according to the selected option |
| (Filter By) **All**        | No filter will be applied. Default value.                    |
| (Filter By) **Assigned**   | It will return the amount of issues assigned to the authenticated user |
| (Filter By) **Created**    | It will return the amount of issues created by the authenticated user |
| (Filter By) **Mentioned**  | It will return the amount of issues where the authenticated user is mentioned |
| (Filter By) **Subscribed** | It will return the amount of issues that the authenticated user has subscribed to |
| **State**                  | Configuring this field will return the amount of issues according to the selected state |
| (State) **All**            | No filter will be applied. Default value.                    |
| (State) **Open**           | It will count the amount of open issues                      |
| (State) **Closed**         | It will count the amount of closed issues                    |

<br>

> See [Considerations](/considerations-known-issues) for more details.

> :ToCPrevNext