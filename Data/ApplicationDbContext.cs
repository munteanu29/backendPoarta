﻿using itec_mobile_api_final.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace itec_mobile_api_final.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
          public DbSet<DoorEntity.DoorEntity> DoorEntities { get; set; }
          


          public IRepository<T> GetRepository<T>() where T: Entity
        {
            return new Repository<T>(this);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
//            builder.Entity<StationEntity>()
//                .Property(e => e.Location).HasConversion(
//                    v => JsonConvert.SerializeObject(v),
//                    v => JsonConvert.DeserializeObject<PointF>(v));
            
            base.OnModelCreating(builder);
        }
    }
}