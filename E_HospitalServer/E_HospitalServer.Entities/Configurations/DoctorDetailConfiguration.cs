using E_HospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_HospitalServer.Entities.Configurations;

internal sealed class DoctorDetailConfiguration : IEntityTypeConfiguration<DoctorDetail>
{
    public void Configure(EntityTypeBuilder<DoctorDetail> builder)
    {
        builder.HasKey(key => key.UserId);
        builder.Property(p => p.AppointmentPrice).HasColumnType("money");
    }
}