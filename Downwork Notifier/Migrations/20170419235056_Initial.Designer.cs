using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Downwork_Notifier.DAL;
using ApiLibrary.ApiEntities.Profiles;
using ApiLibrary.ApiEntities.Const;

namespace Downwork_Notifier.Migrations
{
    [DbContext(typeof(DownworkContext))]
    [Migration("20170419235056_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiEntities.Metadata.CategoryViewModel", b =>
                {
                    b.Property<long>("CategoryViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CategoryViewModelId1");

                    b.Property<long>("Id");

                    b.Property<long?>("MainPageViewModelId");

                    b.Property<string>("Title");

                    b.HasKey("CategoryViewModelId");

                    b.HasIndex("CategoryViewModelId1");

                    b.HasIndex("MainPageViewModelId");

                    b.ToTable("CategoryViewModel");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiEntities.Profiles.ClientViewModel", b =>
                {
                    b.Property<long>("ClientViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<double>("Feedback");

                    b.Property<int>("JobsPosted");

                    b.Property<int>("PastHires");

                    b.Property<string>("PaymentVerificationStatus");

                    b.Property<int>("ReviewsCount");

                    b.HasKey("ClientViewModelId");

                    b.ToTable("ClientViewModel");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiEntities.Profiles.JobViewModel", b =>
                {
                    b.Property<long>("JobViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Budget");

                    b.Property<string>("Category");

                    b.Property<long?>("ClientViewModelId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Duration");

                    b.Property<string>("Id");

                    b.Property<string>("JobStatus");

                    b.Property<int>("JobType");

                    b.Property<string>("SkillsEF")
                        .HasColumnName("Skills");

                    b.Property<string>("Snippet")
                        .HasMaxLength(16000);

                    b.Property<string>("Subcategory");

                    b.Property<long?>("TabViewModelId");

                    b.Property<string>("Title");

                    b.Property<string>("UrlEF")
                        .HasColumnName("Url");

                    b.Property<string>("Workload");

                    b.HasKey("JobViewModelId");

                    b.HasIndex("ClientViewModelId");

                    b.HasIndex("TabViewModelId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.JobSearchParametersViewModel", b =>
                {
                    b.Property<long>("JobSearchParametersViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BudgetRangeParameterViewModelId");

                    b.Property<string>("Category");

                    b.Property<long?>("ClientFeedbackRangeParameterViewModelId");

                    b.Property<long?>("ClientHiresRangeParameterViewModelId");

                    b.Property<int>("DaysPosted");

                    b.Property<int>("Duration");

                    b.Property<int>("JobStatus");

                    b.Property<int>("JobType");

                    b.Property<long?>("PagingViewModelId");

                    b.Property<string>("Query");

                    b.Property<string>("Skills");

                    b.Property<string>("Sort");

                    b.Property<string>("Subcategory");

                    b.Property<long?>("TabViewModelId")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.Property<int>("Workload");

                    b.HasKey("JobSearchParametersViewModelId");

                    b.HasIndex("BudgetRangeParameterViewModelId");

                    b.HasIndex("ClientFeedbackRangeParameterViewModelId");

                    b.HasIndex("ClientHiresRangeParameterViewModelId");

                    b.HasIndex("PagingViewModelId");

                    b.HasIndex("TabViewModelId")
                        .IsUnique();

                    b.ToTable("JobSearchParametersViewModel");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.PagingViewModel", b =>
                {
                    b.Property<long>("PagingViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<long>("Offset");

                    b.Property<long>("Total");

                    b.HasKey("PagingViewModelId");

                    b.ToTable("PagingViewModel");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.RangeParameterViewModel<double>", b =>
                {
                    b.Property<long>("RangeParameterViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Max");

                    b.Property<double?>("Min");

                    b.HasKey("RangeParameterViewModelId");

                    b.ToTable("RangeParameterViewModel<double>");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.RangeParameterViewModel<int>", b =>
                {
                    b.Property<long>("RangeParameterViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Max");

                    b.Property<int?>("Min");

                    b.HasKey("RangeParameterViewModelId");

                    b.ToTable("RangeParameterViewModel<int>");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.MainPageViewModel", b =>
                {
                    b.Property<long>("MainPageViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken");

                    b.Property<string>("AccessTokenSecret");

                    b.Property<string>("AvailableSkillsEF")
                        .HasColumnName("AvailableSkills")
                        .HasAnnotation("SqlCe:ColumnType", "ntext");

                    b.Property<bool>("IsEnabledPopups");

                    b.Property<bool>("IsMinimizedToTray");

                    b.Property<int>("PopupDuration");

                    b.Property<short>("TabIndex");

                    b.HasKey("MainPageViewModelId");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.TabViewModel", b =>
                {
                    b.Property<long>("TabViewModelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FilterName");

                    b.Property<long?>("MainPageViewModelId");

                    b.Property<string>("SelectedSkillsEF")
                        .HasColumnName("SelectedSkills");

                    b.Property<bool>("SettingsExpanded");

                    b.HasKey("TabViewModelId");

                    b.HasIndex("MainPageViewModelId");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiEntities.Metadata.CategoryViewModel", b =>
                {
                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiEntities.Metadata.CategoryViewModel")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryViewModelId1");

                    b.HasOne("Downwork_Notifier.ViewModels.MainPageViewModel")
                        .WithMany("Categories")
                        .HasForeignKey("MainPageViewModelId");
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiEntities.Profiles.JobViewModel", b =>
                {
                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiEntities.Profiles.ClientViewModel", "Client")
                        .WithMany()
                        .HasForeignKey("ClientViewModelId");

                    b.HasOne("Downwork_Notifier.ViewModels.TabViewModel")
                        .WithMany("Jobs")
                        .HasForeignKey("TabViewModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.JobSearchParametersViewModel", b =>
                {
                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.RangeParameterViewModel<int>", "Budget")
                        .WithMany()
                        .HasForeignKey("BudgetRangeParameterViewModelId");

                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.RangeParameterViewModel<double>", "ClientFeedback")
                        .WithMany()
                        .HasForeignKey("ClientFeedbackRangeParameterViewModelId");

                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.RangeParameterViewModel<int>", "ClientHires")
                        .WithMany()
                        .HasForeignKey("ClientHiresRangeParameterViewModelId");

                    b.HasOne("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.PagingViewModel", "Paging")
                        .WithMany()
                        .HasForeignKey("PagingViewModelId");

                    b.HasOne("Downwork_Notifier.ViewModels.TabViewModel")
                        .WithOne("Filter")
                        .HasForeignKey("Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters.JobSearchParametersViewModel", "TabViewModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Downwork_Notifier.ViewModels.TabViewModel", b =>
                {
                    b.HasOne("Downwork_Notifier.ViewModels.MainPageViewModel")
                        .WithMany("Tabs")
                        .HasForeignKey("MainPageViewModelId");
                });
        }
    }
}
