using Microsoft.EntityFrameworkCore;
using RequestHub.Shared;
using System.Security.Cryptography;
using System.Text;

namespace RequestHub.Server.Data
{
    public class DataContext : DbContext
    {
        private readonly IHostEnvironment _env;


        public DataContext(DbContextOptions<DataContext> options, IHostEnvironment env) : base(options)
        {
            _env = env;
        }

        //Method is for enabling sensitive data loggin on the console
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (_env.IsDevelopment())
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
                
        }

        //data seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Description = "Enter description here",
                    IsValid = true,
                    Timestamp = DateTime.Now,
                    DepartmentId = 1,
                    EquipmentName = "enter equipment name here",
                    UserId = 1,
                    SiteLocationId = 1,
                    Comment = "enter comment here..."
                    //UploadFileId = 1



                }
            );


            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    DepartmentName = "Software Engineering"
                },
                new Department
                {
                    Id = 2,
                    DepartmentName = "Cloud Architect"
                }
            );



            modelBuilder.Entity<SiteLocation>().HasData(
                new SiteLocation
                {
                    Id = 1,
                    Name = "Germany"
                },
                new SiteLocation
                {
                    Id = 2,
					Name = "United States"
                },
                new SiteLocation
                {
                    Id = 3,
					Name = "Switzerland"
                },
                new SiteLocation
                {
                    Id = 4,
					Name = "Romania"
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
                    Email = "testuser@user.com",
                    PasswordHash = passwordHash1,
                    PasswordSalt = passwordSalt1,
                    DateCreated = DateTime.Now,
                    Role = "Admin",
                    RequestorName = "Test User"
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
