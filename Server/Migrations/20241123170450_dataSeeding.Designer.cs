﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RequestHub.Server.Data;

#nullable disable

namespace RequestHub.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241123170450_dataSeeding")]
    partial class dataSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RequestHub.Shared.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentName = "Software Engineering"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentName = "Cloud Architect"
                        });
                });

            modelBuilder.Entity("RequestHub.Shared.SiteLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SiteLocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 2,
                            Name = "United States"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Switzerland"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Romania"
                        });
                });

            modelBuilder.Entity("RequestHub.Shared.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<int>("SiteLocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SiteLocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comment = "enter comment here...",
                            DepartmentId = 1,
                            Description = "Enter description here",
                            EquipmentName = "enter equipment name here",
                            IsValid = true,
                            SiteLocationId = 1,
                            Timestamp = new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(3982),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("RequestHub.Shared.UploadFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.Property<string>("TrustedFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("UploadFiles");
                });

            modelBuilder.Entity("RequestHub.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RequestorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(4723),
                            Email = "testuser@user.com",
                            PasswordHash = new byte[] { 117, 249, 81, 48, 244, 31, 74, 71, 104, 122, 66, 175, 60, 156, 2, 191, 91, 72, 43, 19, 221, 12, 140, 49, 78, 56, 65, 1, 70, 212, 30, 58, 233, 155, 214, 222, 83, 95, 66, 188, 202, 23, 140, 210, 237, 198, 133, 118, 195, 127, 158, 182, 46, 60, 254, 153, 208, 90, 223, 161, 226, 25, 98, 139 },
                            PasswordSalt = new byte[] { 48, 69, 163, 203, 163, 198, 137, 237, 110, 25, 16, 165, 165, 128, 130, 14, 56, 56, 224, 124, 199, 214, 217, 192, 142, 214, 40, 244, 238, 31, 202, 3, 3, 133, 42, 52, 58, 1, 255, 204, 104, 2, 115, 148, 108, 218, 49, 139, 81, 34, 3, 235, 241, 103, 243, 23, 25, 167, 186, 2, 250, 127, 78, 0, 192, 191, 7, 216, 133, 9, 156, 195, 254, 198, 111, 209, 146, 241, 228, 101, 68, 26, 108, 141, 1, 192, 200, 250, 220, 26, 87, 81, 248, 106, 13, 55, 98, 103, 202, 98, 195, 182, 152, 58, 89, 184, 106, 204, 20, 237, 61, 202, 19, 78, 104, 17, 210, 70, 209, 15, 156, 88, 28, 196, 254, 193, 210, 237 },
                            RequestorName = "Test User",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("RequestHub.Shared.Ticket", b =>
                {
                    b.HasOne("RequestHub.Shared.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RequestHub.Shared.SiteLocation", "SiteLocation")
                        .WithMany()
                        .HasForeignKey("SiteLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RequestHub.Shared.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("SiteLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RequestHub.Shared.UploadFile", b =>
                {
                    b.HasOne("RequestHub.Shared.Ticket", "Ticket")
                        .WithMany("UploadFiles")
                        .HasForeignKey("TicketId");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("RequestHub.Shared.Ticket", b =>
                {
                    b.Navigation("UploadFiles");
                });
#pragma warning restore 612, 618
        }
    }
}