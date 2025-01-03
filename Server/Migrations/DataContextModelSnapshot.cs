﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RequestHub.Server.Data;

#nullable disable

namespace RequestHub.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            Timestamp = new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(7805),
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
                            DateCreated = new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(8589),
                            Email = "testuser@user.com",
                            PasswordHash = new byte[] { 27, 93, 65, 209, 239, 24, 183, 167, 177, 103, 161, 197, 98, 112, 28, 48, 103, 82, 62, 210, 132, 26, 216, 85, 138, 59, 103, 174, 255, 103, 158, 99, 27, 39, 174, 151, 58, 1, 141, 18, 171, 200, 129, 29, 97, 174, 144, 211, 138, 25, 24, 22, 232, 229, 238, 110, 7, 222, 193, 121, 93, 59, 127, 218 },
                            PasswordSalt = new byte[] { 122, 86, 217, 83, 209, 104, 65, 15, 25, 2, 54, 127, 137, 164, 125, 38, 27, 62, 27, 240, 45, 39, 24, 30, 199, 158, 35, 124, 113, 158, 89, 45, 78, 190, 169, 179, 168, 63, 95, 123, 90, 241, 109, 77, 172, 212, 171, 160, 160, 34, 156, 207, 78, 51, 252, 238, 191, 86, 63, 110, 21, 189, 80, 69, 172, 187, 251, 54, 153, 167, 95, 111, 226, 58, 216, 25, 233, 74, 108, 104, 241, 254, 8, 71, 159, 158, 58, 227, 26, 54, 69, 227, 255, 31, 137, 152, 193, 80, 210, 68, 11, 16, 72, 90, 98, 104, 65, 85, 73, 34, 242, 32, 8, 12, 141, 76, 91, 211, 45, 195, 235, 108, 11, 33, 58, 202, 134, 13 },
                            RequestorName = "Test User",
                            Role = "Admin",
                            VerifiedAt = new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(8591)
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
