using Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ShiftRoleConfiguration : IEntityTypeConfiguration<ShiftRole>
{
  public void Configure(EntityTypeBuilder<ShiftRole> builder)
  {
    builder.HasKey(sr => sr.Id);
    builder.Property(sr => sr.Name).IsRequired();
    builder.Property(sr => sr.RequiresApproval).IsRequired();
  }
}
