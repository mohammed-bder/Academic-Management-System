using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task02_MVC.Models
{
    public class CompanyContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Trainee> Trainee { get; set; }
        public DbSet<CrsResult> CrsResult { get; set; }
        public CompanyContext():base()
        {

        }

        public CompanyContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=. ;Initial Catalog = Task02_MVC;Integrated Security= True; Encrypt= False;Trust Server Certificate=True ");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
