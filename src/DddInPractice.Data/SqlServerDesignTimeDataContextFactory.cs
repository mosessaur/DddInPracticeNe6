using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace DddInPractice.Data;

public class SqlServerDesignTimeDataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    private const string ConnectionString = @"Data Source=.\SqlExpress;Initial Catalog=DddInPractice; Integrated Security=true;";

    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        optionsBuilder.UseSqlServer(ConnectionString,
            x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

        return new DataContext(optionsBuilder.Options);
    }
}
