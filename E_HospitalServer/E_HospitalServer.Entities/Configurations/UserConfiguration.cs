using E_HospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_HospitalServer.Entities.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
        builder.HasIndex(x => x.EmailConfirmCode).IsUnique();
    }
}