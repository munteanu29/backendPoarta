﻿using System.Drawing;
using itec_mobile_api_final.Entities;
using itec_mobile_api_final.Heater;
using itec_mobile_api_final.HeaterSchedule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace itec_mobile_api_final.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
          public DbSet<HeaterEntity> HeaterEntities { get; set; }
          public DbSet<TemperatureMeasurementEntity> TemperatureMeasurementEntities { get; set; }
          public DbSet<HeaterScheduleEntity> HeaterScheduleEntities { get; set; }
          


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