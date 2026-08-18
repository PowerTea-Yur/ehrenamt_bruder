using Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

    builder.OwnsOne(
        e => e.Location,
        location =>
        {
          location.Property(l => l.Street).HasColumnName("LocationStreet").IsRequired();
        }
    );

    builder.OwnsMany(
        e => e.Shifts,
        shift =>
        {
          shift.HasKey(s => s.Id);

          shift.Property(s => s.ShiftDate).IsRequired();
          shift.Property(s => s.StartTime).IsRequired();
          shift.Property(s => s.EndTime).IsRequired();
          shift.Property(s => s.IsCancelled).IsRequired();

          shift.OwnsMany(
                  s => s.RoleAssignments,
                  shiftRoleAssignment =>
                  {
                    shiftRoleAssignment.HasKey(sra => sra.Id);
                    shiftRoleAssignment.Property(sra => sra.MaxVolunteers).IsRequired();

                    shiftRoleAssignment
                            .HasOne(sra => sra.Role)
                            .WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.Restrict);
                  }
              );
          shift
                  .Navigation(s => s.RoleAssignments)
                  .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    );
    builder.Navigation(e => e.Shifts).UsePropertyAccessMode(PropertyAccessMode.Field);

    builder
        .HasMany(e => e.ShiftRoles)
        .WithOne()
        .HasForeignKey("EventId")
        .OnDelete(DeleteBehavior.Cascade);

    builder.Navigation(e => e.ShiftRoles).UsePropertyAccessMode(PropertyAccessMode.Field);
  }
}
