using E_HospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_HospitalServer.Entities.Configurations;

internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(p => p.Price).HasColumnType("money");
        builder.HasQueryFilter(filter => !filter.Doctor!.IsDeleted || !filter.Patient!.IsDeleted);
    }
}