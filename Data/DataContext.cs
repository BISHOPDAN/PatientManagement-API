using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}