### Database setup

This repository uses EF Core migrations to generate the database objects. 

Here are the steps to apply the migrations: 

1. Install the entity framework tools

```
dotnet tool install --global dotnet-ef
```

2. Navigate to the project directory `src\server`

3. Run the following to update the database

```
dotnet ef database update --project .\HelloCodeaholics.Data --context HelloCodeDbContext --startup-project .\HelloCodeaholics.Api
```