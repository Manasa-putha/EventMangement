using EventManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagment.context
{

    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        // public DbSet<GuestList> GuestLists { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Budget)
                .WithOne(b => b.Event)
                .HasForeignKey<Budget>(b => b.EventId);

            base.OnModelCreating(modelBuilder);

            // Configure relationships
            //modelBuilder.Entity<Event>()
            //    .HasOne(e => e.User)
            //    .WithMany()
            //    .HasForeignKey(e => e.UserId)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues

            //modelBuilder.Entity<Guest>()
            //    .HasOne(gl => gl.Event)
            //    .WithMany()
            //    .HasForeignKey(gl => gl.EventId)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues

            //modelBuilder.Entity<Guest>()
            //    .HasOne(gl => gl.UserId)
            //    .WithMany()
            //    .HasForeignKey(gl => gl.UserID)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues

            //modelBuilder.Entity<Budget>()
            //    .HasOne(b => b.Event)
            //    .WithMany()
            //    .HasForeignKey(b => b.EventId)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues
            //  modelBuilder.Entity<Event>()
            //.HasOne(e => e.User)
            //.WithMany(u => u.)
            //.HasForeignKey(e => e.UserId)
            //.OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues

            //modelBuilder.Entity<Guest>()
            //    .HasOne(g => g.Event)
            //    .WithMany(e => e.Guests)
            //    .HasForeignKey(g => g.EventId)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete issues

            //modelBuilder.Entity<Budget>()
            //    .HasOne(b => b.Event)
            //    .WithMany(e => e.Budget)
            //    .HasForeignKey(b => b.EventId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                userName = "Admin",
                Email = "admin@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Organizer,
                Password = "admin1234",
                CreatedAt = new DateTime(2024, 06, 11, 13, 28, 12),
                UpdatedAt = new DateTime(2024, 07, 03, 09, 20, 12),
                PinCode = "12345",
                City = "Kadapa",
                Address = "AP",
                AlternativeNumber = "12345",


            },
            new User()
            {
                Id = 2,
                userName = "sai",
                Email = "sai@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Attendee,
                Password = "sai1234",
                CreatedAt = new DateTime(2024, 03, 06, 13, 30, 12),
                UpdatedAt = new DateTime(2024, 06, 12, 09, 20, 12),
                PinCode = "54321",
                City = "Kurnool",
                Address = "AP",
                AlternativeNumber = "12345",

            },
            new User()
            {
                Id = 3,
                userName = "manu",
                Email = "manu@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Attendee,
                Password = "manu4321",
                CreatedAt = new DateTime(2024, 01, 06, 14, 30, 12),
                UpdatedAt = new DateTime(2024, 09, 03, 09, 20, 12),
                PinCode = "33333",
                City = "Guntur",
                Address = "AP",
                AlternativeNumber = "12345",


            },
              new User()
              {
                  Id = 4,
                  userName = "Rani",
                  Email = "rani@gmail.com",
                  MobileNumber = "1234567890",
                  UserType = UserType.Attendee,
                  Password = "rani43",
                  CreatedAt = new DateTime(2024, 03, 06, 20, 40, 12),
                  UpdatedAt = new DateTime(2024, 06, 03, 09, 20, 12),
                  PinCode = "55555",
                  City = "Anantpur",
                  Address = "AP",
                  AlternativeNumber = "12345",

              }
        );

            // Seeding initial data for Event
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    EventName = "Annual Tech Conference",
                    Date = new DateTime(2024, 10, 15),
                    Time = "10:00 AM",
                    Location = "Convention Center",
                    Description = "A conference about the latest in technology.",
                    UserId = 1
                },
                new Event
                {
                    Id = 2,
                    EventName = "Health & Wellness Workshop",
                    Date = new DateTime(2024, 11, 20),
                    Time = "09:00 AM",
                    Location = "Community Hall",
                    Description = "A workshop focused on health and wellness.",
                    UserId = 1
                },
                  new Event
                  {
                      Id = 3,
                      EventName = "Birthday Party",
                      Date = new DateTime(2024, 09, 2),
                      Time = "09:00 AM",
                      Location = "Community Hall",
                      Description = "Party to be fun",
                      UserId = 1
                  },
                    new Event
                    {
                        Id = 4,
                        EventName = "Sangeeth Haldi functions",
                        Date = new DateTime(2024, 08, 10),
                        Time = "06:00 PM",
                        Location = "Begmoeta Hall",
                        Description = "Fun filled Together night ",
                        UserId = 1
                    }
            );

            // Seeding initial data for GuestList
            //modelBuilder.Entity<GuestList>().HasData(
            //    new GuestList
            //    {
            //        ID = 1,
            //        EventID = 1,
            //        UserID = 2
            //    },
            //    new GuestList
            //    {
            //        ID = 2,
            //        EventID = 1,
            //        UserID = 3
            //    },
            //    new GuestList
            //    {
            //        ID = 3,
            //        EventID = 2,
            //        UserID = 4
            //    }
            //);

            // Seeding initial data for Budget
            modelBuilder.Entity<Budget>().HasData(
                new Budget
                {
                    Id = 1,
                    EventId = 1,
                    Expenses = 5000.00m,
                    Revenue = 10000.00m
                },
                new Budget
                {
                    Id = 2,
                    EventId = 2,
                    Expenses = 3000.00m,
                    Revenue = 7000.00m
                }
            );

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<UserType>().HaveConversion<string>();
            //configurationBuilder.Properties<BillStatus>().HaveConversion<string>();
             configurationBuilder.Properties<UserType>().HaveConversion<string>();
        }
    }
}
