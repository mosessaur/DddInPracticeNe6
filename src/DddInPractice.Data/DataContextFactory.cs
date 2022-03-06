using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Data;

internal static class DataContextFactory
{
    private static string? _connectionString;
    
    internal static void Init(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

        _connectionString = connectionString;
    }

    internal static DataContext CreateDataContext()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
            throw new InvalidOperationException("Connection string is not initialized");

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        optionsBuilder.UseSqlServer(_connectionString,
            opt => opt.MigrationsAssembly(typeof(DataContext).Assembly.FullName));

        return new DataContext(optionsBuilder.Options);
    }
}
