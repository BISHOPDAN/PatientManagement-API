using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientRecord> PatientRecords { get; set; }
    }
}