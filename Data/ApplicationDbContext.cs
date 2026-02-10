using Microsoft.EntityFrameworkCore;
using RealEstatePro.Models;

namespace RealEstatePro.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyRequest> PropertyRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
        });
        
        // Property configuration
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasOne(p => p.User)
                  .WithMany(u => u.Properties)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // PropertyRequest configuration
        modelBuilder.Entity<PropertyRequest>(entity =>
        {
            entity.HasOne(r => r.Property)
                  .WithMany(p => p.Requests)
                  .HasForeignKey(r => r.PropertyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Seed sample data
        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed a demo user (password: Demo123!)
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Email = "demo@object.sa",
            PasswordHash = "$2a$11$K3g6vkqMqh8qPmzBFQJ9/.H8Jg7KbJQJV.mZ1wXYz5h6J9KjK3FKi",
            FullName = "مستخدم تجريبي",
            Phone = "+966501234567",
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
        
        // Seed sample properties in Riyadh
        modelBuilder.Entity<Property>().HasData(
            new Property
            {
                Id = 1,
                Title = "فيلا فاخرة في حي الملقا",
                Description = "فيلا راقية مع مسبح خاص وحديقة. تصميم عصري مع تشطيبات فاخرة.",
                Price = 3500000,
                PropertyType = "sale",
                Category = "villa",
                Location = "حي الملقا، الرياض",
                Latitude = 24.7828,
                Longitude = 46.6264,
                Bedrooms = 5,
                Bathrooms = 6,
                Area = 450,
                ImageUrl = "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800",
                UserId = 1,
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Property
            {
                Id = 2,
                Title = "شقة مودرن في حي النرجس",
                Description = "شقة أنيقة بغرفتي نوم مع إطلالة رائعة. تشطيب فاخر وموقع استراتيجي.",
                Price = 850000,
                PropertyType = "sale",
                Category = "apartment",
                Location = "حي النرجس، الرياض",
                Latitude = 24.8321,
                Longitude = 46.7209,
                Bedrooms = 2,
                Bathrooms = 2,
                Area = 120,
                ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800",
                UserId = 1,
                CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Property
            {
                Id = 3,
                Title = "مكتب تجاري في طريق الملك فهد",
                Description = "مساحة تجارية مميزة مناسبة للمكاتب. موقع حيوي على الطريق الرئيسي.",
                Price = 2500000,
                PropertyType = "sale",
                Category = "commercial",
                Location = "طريق الملك فهد، الرياض",
                Latitude = 24.7116,
                Longitude = 46.6740,
                Bedrooms = 0,
                Bathrooms = 4,
                Area = 350,
                ImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=800",
                UserId = 1,
                CreatedAt = new DateTime(2024, 2, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Property
            {
                Id = 4,
                Title = "أرض سكنية في حي الياسمين",
                Description = "أرض سكنية مميزة في حي راقي. فرصة استثمارية ممتازة.",
                Price = 1200000,
                PropertyType = "sale",
                Category = "land",
                Location = "حي الياسمين، الرياض",
                Latitude = 24.8229,
                Longitude = 46.7037,
                Bedrooms = 0,
                Bathrooms = 0,
                Area = 500,
                ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=800",
                UserId = 1,
                CreatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
