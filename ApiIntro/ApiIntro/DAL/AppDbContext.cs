using ApiIntro.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt) { }
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees=> Set<Employee>();

    }
}
