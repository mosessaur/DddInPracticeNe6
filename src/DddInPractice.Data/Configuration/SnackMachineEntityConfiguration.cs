using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
using DddInPractice.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Data.Configuration;

internal class SnackMachineEntityConfiguration : IEntityTypeConfiguration<SnackMachine>
{
    public void Configure(EntityTypeBuilder<SnackMachine> builder)
    {
        builder.ToTable(nameof(SnackMachine)).HasKey(t => t.Id);

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
                SnackMachineId = 1L,
                OneCentCount = 0,
                TenCentCount = 0,
                QuarterCount = 0,
                OneDollarCount = 0,
                FiveDollarCount = 0,
                TwentyDollarCount = 0,
            });
        });

        builder.Navigation(sm => sm.MoneyInside).IsRequired();

        // Configuring aggregate (DDD).
        // Alt: Can use separate entity configuration class for Slot and use HasMany WithOne here
        builder.OwnsMany<Slot>(SnackMachine.SlotsNavigationPropertyName, slotNavBldr =>
        {
            slotNavBldr.ToTable(nameof(Slot)).HasKey(slot => slot.Id);
            slotNavBldr.Property(slot => slot.Position).HasColumnName(nameof(Slot.Position));

            // ValueObject config
            slotNavBldr.OwnsOne(slot => slot.SnackPile, snackPileNavBldr =>
              {
                  const string snackIdFkColumnName = "SnackId";
                  snackPileNavBldr.Property(sp => sp.Price)
                                  .HasPrecision(12, 2).HasColumnName(nameof(SnackPile.Price));

                  snackPileNavBldr.Property(sp => sp.Quantity)
                                  .HasColumnName(nameof(SnackPile.Quantity));

                  // Slot 1..* Snack relationship One to many
                  snackPileNavBldr.HasOne(sp => sp.Snack)
                                  .WithMany()
                                  .HasForeignKey(snackIdFkColumnName)
                                  .IsRequired();
                  
                  
                  snackPileNavBldr.Property<long>(snackIdFkColumnName)
                                  .HasColumnName(snackIdFkColumnName);

                  snackPileNavBldr.Navigation(sp => sp.Snack)
                                  .AutoInclude();
                  
                  snackPileNavBldr.WithOwner();
                  
                  snackPileNavBldr.HasData(new { SlotId = 1L, SnackId = 1L, Price = 3m, Quantity = 10 },
                                           new { SlotId = 2L, SnackId = 2L, Price = 2m, Quantity = 15 },
                                           new { SlotId = 3L, SnackId = 3L, Price = 1m, Quantity = 20 });
              });
            slotNavBldr.WithOwner(slot => slot.SnackMachine);
            slotNavBldr.HasData(new { Id = 1L, Position = 1, SnackMachineId = 1L },
                                new { Id = 2L, Position = 2, SnackMachineId = 1L },
                                new { Id = 3L, Position = 3, SnackMachineId = 1L });
        });

        builder.Ignore(sm => sm.MoneyInTransaction);

        builder.HasData(new { Id = 1L });
    }
}
