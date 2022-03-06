### DDDInPractice course sample code
This is .Net6 with EF Core update to [Domain-Driven Design in Practice](https://www.pluralsight.com/courses/domain-driven-design-in-practice) course [sample](https://github.com/vkhorikov/DddInAction) by [Vladimir Khorikov](https://github.com/vkhorikov)

### Solution structure

I implemented different solution structure than the original sample solution. My solution is structured as the following

- **Domain** project: For all domain entities with their business logic. It is `netstandard2.1` project
- **Data** project: For data access (DbContext & repositories implementations) & migrations (EF Core migrations) infrastructure.
- **WpfClient** project: For UI using WPF and MVVM.
- **Domain.Tests** project: For Domain unit tests using [xUnit](https://xUnit.net/)

I used Visual Studio 2022 with .Net6. The DddInPractice.Domain project is using `netstandard2.1`.

You can find a a quick intro at [blog post](https://mosesofegypt.net/blog/ddd-inpractice-source-using-net6-with-efcore).

### Running the solution

1. Update connection string
2. You need to set up the database first using EF Core
3. Run the solution with WpfClient project as your startup project.

Connection string is hardcoded in:
* `DddInPractice.Data/SqlServerDesignTimeDataContextFactory` 
* `DddInPractice.WpfClient/App.xaml.cs`

To run database migrations. You need to have EF Core tools installed. Use this .Net CLI command.

Check If you have EF Core command-line tools:
  `dotnet ef`

Install EF Core command-line tools:
  `dotnet tool install --global dotnet-ef`

Run Db Migrations for the solution:
  `dotnet ef database update --project src/DddInPractice.Data --startup-project src/DddInPractice.Data`

  **Note** that the above command assumes you are running the command from within the solution folder.
