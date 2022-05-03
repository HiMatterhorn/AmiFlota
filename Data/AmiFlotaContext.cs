using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmiFlota.Models;

namespace AmiFlota.Data
{
    public class AmiFlotaContext : DbContext
    {
        public AmiFlotaContext (DbContextOptions<AmiFlotaContext> options)
            : base(options)
        {
        }

        public DbSet<AmiFlota.Models.CarModel> CarModel { get; set; }
    }
}
