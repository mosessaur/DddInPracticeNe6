using DddInPractice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Data.Configuration;

internal class SnackMachineConfiguration : IEntityTypeConfiguration<SnackMachine>
{
    public void Configure(EntityTypeBuilder<SnackMachine> builder)
    {
        builder.ToTable(nameof(SnackMachine)).HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(sm => sm.MoneyInside, navBldr =>
        {
            navBldr.Property(m => m.OneCentCount).HasColumnName(nameof(Money.OneCentCount));
            navBldr.Property(m => m.TenCentCount).HasColumnName(nameof(Money.TenCentCount));
            navBldr.Property(m => m.QuarterCount).HasColumnName(nameof(Money.QuarterCount));
            navBldr.Property(m => m.OneDollarCount).HasColumnName(nameof(Money.OneDollarCount));
            navBldr.Property(m => m.FiveDollarCount).HasColumnName(nameof(Money.FiveDollarCount));
            navBldr.Property(m => m.TwentyDollarCount).HasColumnName(nameof(Money.TwentyDollarCount));
            navBldr.Ignore(m => m.Amount);
            navBldr.WithOwner();

            navBldr.HasData(new
            {
                SnackMachineId = 1L,
                OneCentCount = 10,
                TenCentCount = 10,
                QuarterCount = 10,
                OneDollarCount = 10,
                FiveDollarCount = 10,
                TwentyDollarCount = 10,
            });
        });

        builder.Navigation(sm => sm.MoneyInside).IsRequired(true);

        builder.Ignore(sm => sm.MoneyInTransaction);

        builder.HasData(new { Id = 1L });
    }
}
