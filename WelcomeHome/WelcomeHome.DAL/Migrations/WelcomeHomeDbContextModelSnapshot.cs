﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WelcomeHome.DAL;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    [DbContext(typeof(WelcomeHomeDbContext))]
    partial class WelcomeHomeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SocialPayoutUserCategory", b =>
                {
                    b.Property<long>("SocialPayoutsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserCategoriesId")
                        .HasColumnType("bigint");

                    b.HasKey("SocialPayoutsId", "UserCategoriesId");

                    b.HasIndex("UserCategoriesId");

                    b.ToTable("SocialPayoutUserCategory");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherContacts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Establishment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("EstablishmentTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherContacts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("EstablishmentTypeId");

                    b.ToTable("Establishments");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.EstablishmentType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstablishmentTypes");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EstablishmentId")
                        .HasColumnType("bigint");

                    b.Property<long>("EventTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("VolunteerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.EventType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.PaymentStep", b =>
                {
                    b.Property<long>("StepId")
                        .HasColumnType("bigint");

                    b.Property<long>("SocialPayoutId")
                        .HasColumnType("bigint");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.HasKey("StepId", "SocialPayoutId");

                    b.HasIndex("SocialPayoutId");

                    b.ToTable("PaymentSteps");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.SocialPayout", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SocialPayouts");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Step", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EstablishmentTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentTypeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.StepDocument", b =>
                {
                    b.Property<long>("StepId")
                        .HasColumnType("bigint");

                    b.Property<long>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("ToReceive")
                        .HasColumnType("bit");

                    b.HasKey("StepId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("StepsDocuments");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.UserCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserCategories");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Vacancy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherContacts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Volunteer", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<long?>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<string>("SocialUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialPayoutUserCategory", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.SocialPayout", null)
                        .WithMany()
                        .HasForeignKey("SocialPayoutsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.UserCategory", null)
                        .WithMany()
                        .HasForeignKey("UserCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.City", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Establishment", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.Volunteer", "Creator")
                        .WithMany("Establishments")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.EstablishmentType", "EstablishmentType")
                        .WithMany("Establishments")
                        .HasForeignKey("EstablishmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Creator");

                    b.Navigation("EstablishmentType");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Event", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.Establishment", "Establishment")
                        .WithMany("Events")
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WelcomeHome.DAL.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.Volunteer", "Volunteer")
                        .WithMany("Events")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Establishment");

                    b.Navigation("EventType");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.PaymentStep", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.SocialPayout", "SocialPayout")
                        .WithMany("PaymentSteps")
                        .HasForeignKey("SocialPayoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.Step", "Step")
                        .WithMany("PaymentSteps")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialPayout");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.RefreshToken", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("WelcomeHome.DAL.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Step", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.EstablishmentType", "EstablishmentType")
                        .WithMany("Steps")
                        .HasForeignKey("EstablishmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstablishmentType");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.StepDocument", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.Document", "Document")
                        .WithMany("StepDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WelcomeHome.DAL.Models.Step", "Step")
                        .WithMany("StepDocuments")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Vacancy", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.City", "City")
                        .WithMany("Vacancy")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Volunteer", b =>
                {
                    b.HasOne("WelcomeHome.DAL.Models.Establishment", "Organization")
                        .WithMany("VolunteersInOrganization")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("WelcomeHome.DAL.Models.User", "User")
                        .WithOne("Volunteer")
                        .HasForeignKey("WelcomeHome.DAL.Models.Volunteer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.City", b =>
                {
                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Document", b =>
                {
                    b.Navigation("StepDocuments");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Establishment", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("VolunteersInOrganization");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.EstablishmentType", b =>
                {
                    b.Navigation("Establishments");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.SocialPayout", b =>
                {
                    b.Navigation("PaymentSteps");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Step", b =>
                {
                    b.Navigation("PaymentSteps");

                    b.Navigation("StepDocuments");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("WelcomeHome.DAL.Models.Volunteer", b =>
                {
                    b.Navigation("Establishments");

                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
