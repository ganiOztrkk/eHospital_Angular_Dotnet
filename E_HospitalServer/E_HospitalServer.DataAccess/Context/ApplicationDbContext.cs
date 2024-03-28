using E_HospitalServer.Entities.Models;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace E_HospitalServer.DataAccess.Context;

internal sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DoctorDetail> DoctorDetails { get; set; }
    public DbSet<Appointment> Appointments  { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();        
        builder.Ignore<IdentityUserRole<Guid>>();        
        builder.Ignore<IdentityUserClaim<Guid>>();        
    }
}