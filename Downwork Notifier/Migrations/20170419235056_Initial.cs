using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Downwork_Notifier.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientViewModel",
                columns: table => new
                {
                    ClientViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    Country = table.Column<string>(nullable: true),
                    Feedback = table.Column<double>(nullable: false),
                    JobsPosted = table.Column<int>(nullable: false),
                    PastHires = table.Column<int>(nullable: false),
                    PaymentVerificationStatus = table.Column<string>(nullable: true),
                    ReviewsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientViewModel", x => x.ClientViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "PagingViewModel",
                columns: table => new
                {
                    PagingViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    Count = table.Column<int>(nullable: false),
                    Offset = table.Column<long>(nullable: false),
                    Total = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagingViewModel", x => x.PagingViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "RangeParameterViewModel<double>",
                columns: table => new
                {
                    RangeParameterViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    Max = table.Column<double>(nullable: true),
                    Min = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangeParameterViewModel<double>", x => x.RangeParameterViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "RangeParameterViewModel<int>",
                columns: table => new
                {
                    RangeParameterViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    Max = table.Column<int>(nullable: true),
                    Min = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangeParameterViewModel<int>", x => x.RangeParameterViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    MainPageViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    AccessToken = table.Column<string>(nullable: true),
                    AccessTokenSecret = table.Column<string>(nullable: true),
                    AvailableSkills = table.Column<string>(type: "ntext", nullable: true),
                    IsEnabledPopups = table.Column<bool>(nullable: false),
                    IsMinimizedToTray = table.Column<bool>(nullable: false),
                    PopupDuration = table.Column<int>(nullable: false),
                    TabIndex = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.MainPageViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryViewModel",
                columns: table => new
                {
                    CategoryViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    CategoryViewModelId1 = table.Column<long>(nullable: true),
                    Id = table.Column<long>(nullable: false),
                    MainPageViewModelId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryViewModel", x => x.CategoryViewModelId);
                    table.ForeignKey(
                        name: "FK_CategoryViewModel_CategoryViewModel_CategoryViewModelId1",
                        column: x => x.CategoryViewModelId1,
                        principalTable: "CategoryViewModel",
                        principalColumn: "CategoryViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryViewModel_ApplicationSettings_MainPageViewModelId",
                        column: x => x.MainPageViewModelId,
                        principalTable: "ApplicationSettings",
                        principalColumn: "MainPageViewModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tabs",
                columns: table => new
                {
                    TabViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    FilterName = table.Column<string>(nullable: true),
                    MainPageViewModelId = table.Column<long>(nullable: true),
                    SelectedSkills = table.Column<string>(nullable: true),
                    SettingsExpanded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.TabViewModelId);
                    table.ForeignKey(
                        name: "FK_Tabs_ApplicationSettings_MainPageViewModelId",
                        column: x => x.MainPageViewModelId,
                        principalTable: "ApplicationSettings",
                        principalColumn: "MainPageViewModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    Budget = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    ClientViewModelId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    JobStatus = table.Column<string>(nullable: true),
                    JobType = table.Column<int>(nullable: false),
                    Skills = table.Column<string>(nullable: true),
                    Snippet = table.Column<string>(maxLength: 16000, nullable: true),
                    Subcategory = table.Column<string>(nullable: true),
                    TabViewModelId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Workload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobViewModelId);
                    table.ForeignKey(
                        name: "FK_Jobs_ClientViewModel_ClientViewModelId",
                        column: x => x.ClientViewModelId,
                        principalTable: "ClientViewModel",
                        principalColumn: "ClientViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Tabs_TabViewModelId",
                        column: x => x.TabViewModelId,
                        principalTable: "Tabs",
                        principalColumn: "TabViewModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSearchParametersViewModel",
                columns: table => new
                {
                    JobSearchParametersViewModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlCe:ValueGeneration", "True"),
                    BudgetRangeParameterViewModelId = table.Column<long>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    ClientFeedbackRangeParameterViewModelId = table.Column<long>(nullable: true),
                    ClientHiresRangeParameterViewModelId = table.Column<long>(nullable: true),
                    DaysPosted = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    JobStatus = table.Column<int>(nullable: false),
                    JobType = table.Column<int>(nullable: false),
                    PagingViewModelId = table.Column<long>(nullable: true),
                    Query = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    Sort = table.Column<string>(nullable: true),
                    Subcategory = table.Column<string>(nullable: true),
                    TabViewModelId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Workload = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSearchParametersViewModel", x => x.JobSearchParametersViewModelId);
                    table.ForeignKey(
                        name: "FK_JobSearchParametersViewModel_RangeParameterViewModel<int>_BudgetRangeParameterViewModelId",
                        column: x => x.BudgetRangeParameterViewModelId,
                        principalTable: "RangeParameterViewModel<int>",
                        principalColumn: "RangeParameterViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSearchParametersViewModel_RangeParameterViewModel<double>_ClientFeedbackRangeParameterViewModelId",
                        column: x => x.ClientFeedbackRangeParameterViewModelId,
                        principalTable: "RangeParameterViewModel<double>",
                        principalColumn: "RangeParameterViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSearchParametersViewModel_RangeParameterViewModel<int>_ClientHiresRangeParameterViewModelId",
                        column: x => x.ClientHiresRangeParameterViewModelId,
                        principalTable: "RangeParameterViewModel<int>",
                        principalColumn: "RangeParameterViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSearchParametersViewModel_PagingViewModel_PagingViewModelId",
                        column: x => x.PagingViewModelId,
                        principalTable: "PagingViewModel",
                        principalColumn: "PagingViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSearchParametersViewModel_Tabs_TabViewModelId",
                        column: x => x.TabViewModelId,
                        principalTable: "Tabs",
                        principalColumn: "TabViewModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryViewModel_CategoryViewModelId1",
                table: "CategoryViewModel",
                column: "CategoryViewModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryViewModel_MainPageViewModelId",
                table: "CategoryViewModel",
                column: "MainPageViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ClientViewModelId",
                table: "Jobs",
                column: "ClientViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TabViewModelId",
                table: "Jobs",
                column: "TabViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSearchParametersViewModel_BudgetRangeParameterViewModelId",
                table: "JobSearchParametersViewModel",
                column: "BudgetRangeParameterViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSearchParametersViewModel_ClientFeedbackRangeParameterViewModelId",
                table: "JobSearchParametersViewModel",
                column: "ClientFeedbackRangeParameterViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSearchParametersViewModel_ClientHiresRangeParameterViewModelId",
                table: "JobSearchParametersViewModel",
                column: "ClientHiresRangeParameterViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSearchParametersViewModel_PagingViewModelId",
                table: "JobSearchParametersViewModel",
                column: "PagingViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSearchParametersViewModel_TabViewModelId",
                table: "JobSearchParametersViewModel",
                column: "TabViewModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_MainPageViewModelId",
                table: "Tabs",
                column: "MainPageViewModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryViewModel");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobSearchParametersViewModel");

            migrationBuilder.DropTable(
                name: "ClientViewModel");

            migrationBuilder.DropTable(
                name: "RangeParameterViewModel<int>");

            migrationBuilder.DropTable(
                name: "RangeParameterViewModel<double>");

            migrationBuilder.DropTable(
                name: "PagingViewModel");

            migrationBuilder.DropTable(
                name: "Tabs");

            migrationBuilder.DropTable(
                name: "ApplicationSettings");
        }
    }
}
