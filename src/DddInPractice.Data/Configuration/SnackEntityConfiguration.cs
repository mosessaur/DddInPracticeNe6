using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Data.Configuration;

internal class SnackEntityConfiguration : IEntityTypeConfiguration<Snack>
{
    public void Configure(EntityTypeBuilder<Snack> builder)
    {
        builder.ToTable(nameof(Snack)).HasKey(t => t.Id);

        builder.Property(t => t.Name).HasMaxLength(250).IsRequired();

        builder.HasData(
            new { Id = 1L, Name = "Chocolate" },
            new { Id = 2L, Name = "Soda" },
            new { Id = 3L, Name = "Gum" });
    }
}
