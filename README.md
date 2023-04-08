
# Bank Management

For Uni Project.

## Installation

First Need to Change the Database Connection From appsettings.json
Change the Server Name
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=BankManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

## Creating DataBase

Use the NuGet package manager to install the Database. From the topbar go to tools>NuGet Package Maneger>Package Maneger Console

```bash
add-migration 1
```
You Will See Like This Message

``` picture
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
```
Then run Again this Command

```bash
update-database
```

## Usage



## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

