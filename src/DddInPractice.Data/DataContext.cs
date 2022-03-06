using System.Reflection;
using DddInPractice.Domain;
using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Data;

#nullable disable
public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SnackMachine> SnackMachines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.HasSequence("SnackMachineSeq").StartsAt(1).IncrementsBy(5).HasMin(0).HasMax(long.MaxValue);
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
#nullable enable
