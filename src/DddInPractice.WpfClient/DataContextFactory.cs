using DddInPractice.Data;
using Microsoft.EntityFrameworkCore;

namespace DddInPractice.WpfClient;

internal static class DataContextFactory
{
    private const string ConnectionString = @"Data Source=.\SqlExpress;Initial Catalog=DddInPractice; Integrated Security=true;";
    public static DataContext CreateDataContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        optionsBuilder.UseSqlServer(ConnectionString,
            x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName));

        return new DataContext(optionsBuilder.Options);
    }
}
