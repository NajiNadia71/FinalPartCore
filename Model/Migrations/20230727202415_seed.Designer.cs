﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model;

#nullable disable

namespace Model.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230727202415_seed")]
    partial class seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

                    b.HasData(
                        new
                        {
                            Id = "E8015CA3-DB29-46AB-95DF-04E484D78835",
                            ConcurrencyStamp = "",
                            Name = "GetAllGroupRoleNames",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "2A18E626-6A6F-4755-BB1A-06E3BD741519",
                            ConcurrencyStamp = "",
                            Name = "EditRoleGroupd",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "B1C8B66B-7B9A-4C72-A758-08B95A348188",
                            ConcurrencyStamp = "",
                            Name = "AddRoleToRoleGroup",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "3F781779-E0A0-4245-8183-1837B7554A3D",
                            ConcurrencyStamp = "",
                            Name = "GetAllUsers",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "E0967BB8-DCB8-441D-BB3F-3628BC743302",
                            ConcurrencyStamp = "",
                            Name = "AddRoleGroupForUser",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "BB109ED0-2183-47A7-BC7B-464047400115",
                            ConcurrencyStamp = "",
                            Name = "CreateRoleGroup",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "3BD071B4-EBA4-4C47-845E-4F255BD8C4C0",
                            ConcurrencyStamp = "",
                            Name = "DeleteRoleFromRoleGroup",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "FE84485D-7F8D-43E0-B069-5157BBB68F0F",
                            ConcurrencyStamp = "",
                            Name = "CreateRoleGroupd",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "E1F01805-25A1-478F-80FF-5338B379840E",
                            ConcurrencyStamp = "",
                            Name = "EditRoleGroup",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "1EA03C12-3EEE-43D1-B9C6-5EB06321A130",
                            ConcurrencyStamp = "",
                            Name = "GetAllRolesOfGroupRole",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "F18B19B5-962F-4E1E-A484-882BF1FE398E",
                            ConcurrencyStamp = "",
                            Name = "DeleteRoleGroup",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "7E3A93C4-BFA4-4098-8742-9185B123DC31",
                            ConcurrencyStamp = "",
                            Name = "DeleteRolesForUser",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "3FFBD3F9-B616-4164-8EA7-A473EBC4B1BD",
                            ConcurrencyStamp = "",
                            Name = "GetAllRolesGroupsForUser",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "E1B4457B-2E32-431D-8A0A-A8CD59828821",
                            ConcurrencyStamp = "",
                            Name = "DeleteRoleGroupForUser",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "030FB67C-A050-4D47-98A8-C857DBA89576",
                            ConcurrencyStamp = "",
                            Name = "GetUserInfo",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "6AF8DEB0-DEC7-4BD5-A734-D5AF20380857",
                            ConcurrencyStamp = "",
                            Name = "AddRolesForUser",
                            NormalizedName = ""
                        },
                        new
                        {
                            Id = "7D5CBCE5-CBC1-4DC4-9F94-E394A2BFB7A9",
                            ConcurrencyStamp = "",
                            Name = "GetAllRolesForUser",
                            NormalizedName = ""
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Model.ModelClass.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("Model.ModelClass.GroupName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("GroupNames");
                });

            modelBuilder.Entity("Model.ModelClass.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ActionName");

                    b.Property<string>("ControllerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ControllerName");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Model.ModelClass.RoleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GroupNameId")
                        .HasColumnType("int")
                        .HasColumnName("GroupNameId");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("GroupNameId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleGroups");
                });

            modelBuilder.Entity("Model.ModelClass.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GroupNameId")
                        .HasColumnType("int")
                        .HasColumnName("GroupNameId");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupNameId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Model.ModelClass.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Model.ModelClass.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Model.ModelClass.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.ModelClass.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Model.ModelClass.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.ModelClass.RoleGroup", b =>
                {
                    b.HasOne("Model.ModelClass.GroupName", "GroupName")
                        .WithMany("RoleGroups")
                        .HasForeignKey("GroupNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.ModelClass.Role", "Role")
                        .WithMany("RoleGroups")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupName");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Model.ModelClass.UserGroup", b =>
                {
                    b.HasOne("Model.ModelClass.GroupName", "GroupName")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.ModelClass.ApplicationUser", "ApplicationUser")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("GroupName");
                });

            modelBuilder.Entity("Model.ModelClass.UserRole", b =>
                {
                    b.HasOne("Model.ModelClass.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.ModelClass.ApplicationUser", "ApplicationUser")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Model.ModelClass.ApplicationUser", b =>
                {
                    b.Navigation("UserGroups");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Model.ModelClass.GroupName", b =>
                {
                    b.Navigation("RoleGroups");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("Model.ModelClass.Role", b =>
                {
                    b.Navigation("RoleGroups");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
