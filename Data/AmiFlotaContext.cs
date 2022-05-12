using AmiFlota.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmiFlota.Data
{
    public class AmiFlotaContext : IdentityDbContext<ApplicationUser>
    {
        public AmiFlotaContext(DbContextOptions<AmiFlotaContext> options)
            : base(options)
        {
        }

        public DbSet<AmiFlota.Models.CarModel> CarModel { get; set; }
    }
}
