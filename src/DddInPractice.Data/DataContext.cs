using System.Reflection;
using DddInPractice.Domain;
using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
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

    public DbSet<Atm> Atm { get; set; }
    public DbSet<SnackMachine> SnackMachines { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.HasSequence("SnackMachineSeq").StartsAt(1).IncrementsBy(5).HasMin(0).HasMax(long.MaxValue);
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}
#nullable enable
