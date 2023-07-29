# RouteTesting

## Run Migrations

### From Package Manager Console

1. Bring up `Package Manager Console` by clicking `Tools=>NuGet Package Manager=>Package Manager Console`.
2. Run `Add-Migration InitialMigration -verbose -StartupProject Maa.Vacations.WebApi -Project Maa.Vacations.Entities`.
    - `-Project` means which project contains the DbContext class 
    - `-StartupProject` means which project contains the Db connection info and other information.
3. Update the database by running `Update-Database`.

### From PowerShell

1. Install these tools.
    - `dotnet tool install --global dotnet-ef`
    - `dotnet tool update --global dotnet-ef`
2. Run `dotnet ef migrations add InitialMigration -verbose --startup-project Maa.Vacations.WebApi --project Maa.Vacations.Entities`
    - `--project` means which project contains the DbContext class
    - `--startup-project` means which project contains the Db connection info and other information.
3. Update the database by running `dotnet ef database update`.

## For more documentation

- [Entity Framework Core tools reference - .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#other-target-frameworks)

## RFCs To Know About

- [RFC 9110 - HTTP Semantics](https://www.rfc-editor.org/rfc/rfc9110)
- [RFC 6902 - JavaScript Object Notation (JSON) Patch](https://www.rfc-editor.org/rfc/rfc6902)
- [RFC 7396 - JSON Merge Patch](https://www.rfc-editor.org/rfc/rfc7396)

## Good Reference

- [Should HTTP PUT create a resource if it does not exist?](https://stackoverflow.com/questions/56240547/should-http-put-create-a-resource-if-it-does-not-exist)
- [Updating data with JsonPatch [13 of 18] | Web APIs for Beginners](https://www.youtube.com/watch?v=2MDlJRa4iHs)
- [Proper way to include data with an HTTP PATCH request](https://stackoverflow.com/questions/17375867/proper-way-to-include-data-with-an-http-patch-request)
