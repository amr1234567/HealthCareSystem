using HSS.DataAccess.Configurations;
using HSS.Domain.BaseModels;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;

namespace HSS.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<IdentityUser<int>> IdentityUsers { get; set; }
        public DbSet<AdministrationAdmin> AdministrationAdmins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<HospitalAdmin> HospitalAdmins { get; set; }
        public DbSet<LabManager> LabManagers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<RadiologyManager> RadiologyManagers { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ClinicAppointment> ClinicAppointment { get; set; }
        public DbSet<LabAppointment> LabAppointments { get; set; }
        public DbSet<RadiologyAppointment> RadiologyAppointments { get; set; }
        public DbSet<Administration> Administrations { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicSpecialization> ClinicSpecializations { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<EffectiveSubstance> EffectiveSubstances { get; set; }
        public DbSet<EmergencyDepartment> EmergencyDepartments { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<LabCenter> LabCenters { get; set; }
        public DbSet<LabCenterTest> LabCenterTests { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PatientMediacalDetails> PatientMediacalDetails { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<RadiologyCenter> RadiologyCenters { get; set; }
        public DbSet<RadiologyTestType> radiologyTestTypes { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public DbSet<ClinicSpecializationHospital> clinicSpecializationHospitals { get; set; }
        public DbSet<LabCenterLabTest> LabCenterLabTests { get; set; }
        public DbSet<SideEffectEffectiveSubstance> SideEffectEffectiveSubstances { get; set; }
        public DbSet<SideEffectMedicine> SideEffectMedicines { get; set; }
        public DbSet<SymptomDisease> SymptomDiseases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserConfigurations).Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseClass<>).IsAssignableFrom(entityType.ClrType.BaseType))
                {
                    var method = typeof(ApplicationDbContext).GetMethod(nameof(ApplySoftDeleteFilter),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(null, new object[] { modelBuilder });
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        private static void ApplySoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseClass<int> 
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

    }
}
