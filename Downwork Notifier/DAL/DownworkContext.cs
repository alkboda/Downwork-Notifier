using Downwork_Notifier.ViewModels;
using Downwork_Notifier.ViewModels.API.ApiEntities.Profiles;
using Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters;
using Microsoft.EntityFrameworkCore;

namespace Downwork_Notifier.DAL
{
    public class DownworkContext : DbContext
    {
        #region Tables
        public DbSet<MainPageViewModel> ApplicationSettings { get; set; }
        public DbSet<TabViewModel> Tabs { get; set; }
        public DbSet<JobViewModel> Jobs { get; set; }
        #endregion Tables

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlCe(@"Data Source=.\Downwork.sdf");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainPageViewModel>()
                .Property(mp => mp.AvailableSkillsEF)
                .ForSqlCeHasColumnType("ntext");

            modelBuilder.Entity<JobViewModel>()
                .Ignore(j => j.Url)
                .Ignore(j => j.Skills)
                .Property(j => j.Snippet).HasMaxLength(16000);

            // Cascade settings for TabVM
            //
            modelBuilder.Entity<TabViewModel>()
                .HasOne(t => t.Filter)
                .WithOne()
                .HasForeignKey<JobSearchParametersViewModel>($"{nameof(TabViewModel)}Id")
                .IsRequired();
            modelBuilder.Entity<TabViewModel>()
                .HasMany(t => t.Jobs)
                .WithOne()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
        }
    }
}
