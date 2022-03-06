using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Data.Configuration;
internal class AtmEntityConfiguration : IEntityTypeConfiguration<Atm>
{
    public void Configure(EntityTypeBuilder<Atm> builder)
    {
        builder.ToTable(nameof(Atm)).HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        // ValueObject config
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
                AtmId = 1L,
                OneCentCount = 100,
                TenCentCount = 100,
                QuarterCount = 100,
                OneDollarCount = 100,
                FiveDollarCount = 100,
                TwentyDollarCount = 100,
            });
        });

        builder.Navigation(atm => atm.MoneyInside).IsRequired();

        builder.HasData(new { Id = 1L, MoneyCharged = 0m });
    }
}
