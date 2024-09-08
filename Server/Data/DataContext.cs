using Microsoft.EntityFrameworkCore;
using RequestHub.Shared;
using System.Security.Cryptography;
using System.Text;

namespace RequestHub.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Method is for enabling sensitive data loggin on the console--remove before production
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder

                .EnableSensitiveDataLogging();
        }

        //data seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Description = "#10",
                    IsValid = true,
                    Timestamp = DateTime.Now,
                    DepartmentId = 1,
                    EquipmentName = "D11",
                    UserId = 1,
                    SiteLocationId = 1,
                    Comment = "'comment here...'"
                    //UploadFileId = 1



                }
            );


            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    DepartmentName = "Controls Engineering"
                },
                new Department
                {
                    Id = 2,
                    DepartmentName = "IT"
                }
            );



            modelBuilder.Entity<SiteLocation>().HasData(
                new SiteLocation
                {
                    Id = 1,
                    TACSite = "TAC-OH"
                },
                new SiteLocation
                {
                    Id = 2,
                    TACSite = "TAC-TN"
                },
                new SiteLocation
                {
                    Id = 3,
                    TACSite = "TAC-AL"
                },
                new SiteLocation
                {
                    Id = 4,
                    TACSite = "TAC-MS"
                }


            );

            //modelBuilder.Entity<UploadFile>().HasData(
            //	new UploadFile
            //	{
            //		Id = 1,
            //		FileName = "None.PNG",
            //		TrustedFileName = "mauiford.ran",
            //		ContentType = "image/png",
            //		Size = 8723,
            //		UploadDate = DateTime.Now,
            //		FilePath = "N/A"

            //	}

            //);






            byte[] passwordHash1, passwordSalt1;
            CreatePasswordHash("hashsalt", out passwordHash1, out passwordSalt1);//hashsalt is the password

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "asilaghi@topreamerica.com",
                    PasswordHash = passwordHash1,
                    PasswordSalt = passwordSalt1,
                    DateCreated = DateTime.Now,
                    Role = "Admin",
                    RequestorName = "Marsh"
                }
            );

        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }







        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SiteLocation> SiteLocations { get; set; }

        public DbSet<UploadFile> UploadFiles { get; set; }
    }
}
