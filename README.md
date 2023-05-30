
<h1 align="center">
  <br>
  <a href="http://www.facebook.com/snpsuj0n"><img src="https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/BankABCLogo.svg/1200px-BankABCLogo.svg.png" alt="PSN Bank" width="200"></a>
  <br>
  PSN Bank
  <br>
</h1>
<p align="center">
  <a href="#installation">Installation</a> •
  <a href="#creating-dataBase">Creating DataBase</a> •
  <a href="#usage">Usage</a> •
  <a href="#contributing">Contributing</a>
</p>

# Bank Management

For Uni Project.

## Installation

> First Need to Change the **Database** Connection From `appsettings.json`
Change the Server Name as your Server from ***_SQL Server_***
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=BankManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

## Creating DataBase

>Use the NuGet package manager to install the Database. From the topbar go to `tools>NuGet Package Manager>Package Manager Console`

```bash
add-migration 1
```
>You Will See Like This Message

```screenshot
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
```
>Then run Again this Command

```bash
update-database
```
>You will Get a Success Messege Like

```screenshot
Build started...
Build succeeded.
Done.
```

## Usage

> After Completing Installation Run the Project. There Has a Defualt User and Password for admin
<br/> _UserName_ ▶︎ 
```bash
admin
``` 
> _Password_ ▶︎ 
```bash
123456
``` 

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

