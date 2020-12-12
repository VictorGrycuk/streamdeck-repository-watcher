# Custom Filters

## Description

Because the GitHub API is quite limited on regards the filters that can be applied to the requests, specially for the pull requests, I added the possibility to apply custom filters using [Roslyn scripting](https://www.strathweb.com/2018/01/easy-way-to-create-a-c-lambda-expression-from-a-string-with-roslyn/) to the result of the call.

These filters are basically lambdas expressions that are applied to the result of the GitHub request sequentially. This, of course, requires a basic knowledge of the models used by the [Octokit.net library](https://github.com/octokit/octokit.net/tree/main/Octokit/Models/Response).

Since Roslyn executes the scripts inside a sandbox, it requires to also specify the imports directives. The most commons are `Octokit`, `System`, and `System.Linq`, and others depending your need.

- The first line of the Custom Filter text area is reserved for the import directives, they have to be comma-separated

- The lambdas can be declared starting the second line
- All the lines have to end with `;`

## Considerations

- The scripting require basic knowledge of the C# language
- Each expression is parsed as if it were a C# code, so all the rules for the language applies
- They are applied **sequentially**. Imagine that that each expression is joined by an *AND* to the next expression
- Be aware that parsing and executing the expressions can be expensive, computationally speaking. Try to use them only if needed and nothing too crazy

## Examples
These are some examples to give you an idea on how to use the custom filters.

### Openened within last 2 days
If you wanted to count only the pull requests or issues that were open within the last 2 days, you can use:

```csharp | --no-wmbar
Octokit,System,System.Linq,System.Collections.Generic;
x => x.CreatedAt > DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0));
```
> :CopyButton


### Only with a specific label
If you wanted to count only the pull requests or issues that have the label `Team: SuperTeam`, you can use:

```csharp | --no-wmbar
Octokit,System,System.Linq,System.Collections.Generic;
x => x.Labels.Any(x => new string[] { \"Team: SuperTeam\" }.Contains(x.Name));
```
> :CopyButton


### Count merged within the last hour
If you wanted to count only the pull requests that are merged *and* updated within the last hour, you can use:

```csharp | --no-wmbar
Octokit,System,System.Linq,System.Collections.Generic;
x => x.Merged == true;
x => x.UpdatedAt > DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
```
> :CopyButton


> :ToCPrevNext