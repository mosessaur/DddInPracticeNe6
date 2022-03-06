using DddInPractice.Domain.BoundedContext.Management.HeadOfficeAggregate;
using DddInPractice.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Data.Configuration;
internal class HeadOfficeEntityConfiguration: IEntityTypeConfiguration<HeadOffice>
{
    public void Configure(EntityTypeBuilder<HeadOffice> builder)
    {
        builder.ToTable(nameof(HeadOffice)).HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        // ValueObject config
        builder.OwnsOne(ho => ho.Cash, navBldr =>
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
                HeadOfficeId = 1L,
                OneCentCount = 0,
                TenCentCount = 0,
                QuarterCount = 0,
                OneDollarCount = 0,
                FiveDollarCount = 0,
                TwentyDollarCount = 0,
            });
        });

        builder.Navigation(atm => atm.Cash).IsRequired();

        builder.HasData(new { Id = 1L, Balance = 0m });
    }
}
