using Microsoft.EntityFrameworkCore;

namespace CompanyClassLibrary.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Workstation> Workstations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkstationEmployeeLink> WorkstationEmployeeLinks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<Workstation>().ToTable("Workstation");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<WorkstationEmployeeLink>().ToTable("WorkstationEmployeeLink");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<EquipmentCategory>().ToTable("EquipmentCategory");
        }*/
    }
}
