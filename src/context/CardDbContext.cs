using Microsoft.EntityFrameworkCore;
using src.model;
using src.model.student;

namespace src.context;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<CardModel> Cards { get; set; }
    public DbSet<UserModel> Users {get;set;}
    
    public DbSet<RolModel> Roles {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentModel>().HasIndex(u=>u.Rut).IsUnique();
        modelBuilder.Entity<StudentModel>()
            .HasOne(s => s.Card)
            .WithOne(c => c.Student)
            .HasForeignKey<CardModel>(c => c.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardModel>()
            .HasIndex(c => c.StudentId)
            .IsUnique();
        
        modelBuilder.Entity<UserModel>()
            .HasOne(r=>r.Rol)
            .WithMany(r=>r.Users)
            .HasForeignKey(u=>u.RolId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<RolModel>().HasData(
            new RolModel
            {
                Id = 1,
                Name = "Empresa"
            },
            new RolModel
            {
                Id = 2,
                Name = "CEAL"
            },
            new RolModel
            {
                Id = 3,
                Name = "Admin"
            }
        );
    }
}
