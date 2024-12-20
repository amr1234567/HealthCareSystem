using HSS.DataAccess.Configurations;
using HSS.DataAccess.Helpers;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;

namespace HSS.DataAccess.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,AccountServicesHelpers accountServices) : DbContext(options)
    {
        public DbSet<IdentityUser> IdentityUsers { get; set; }
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
        public DbSet<PrescriptionRecord> PrescriptionRecords { get; set; }
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

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var salt = accountServices.CreateSalt();
            var hashedPassword = accountServices.HashPasswordWithSalt(salt, "@Aa123456789");



            modelBuilder.Entity<HospitalAdmin>().HasData(
                new HospitalAdmin
                {
                    Id = 1,
                    Name = "Admin User",
                    NationalId = "12345678901234",
                    Email = "admin@hospital.com",
                    Salt = salt,
                    Password = hashedPassword,
                    CreatedAt = DateTime.UtcNow,
                    HospitalId = 1
                }
            );

            modelBuilder.Entity<Hospital>().HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "Central Hospital",
                    Location = "123 Main St",
                    Capacity = 300,
                    ContactNumber = "123-456-7890",
                    Email = "info@centralhospital.com",
                    EstablishedDate = DateTime.Now.AddYears(-50),
                    StartAt = new TimeSpan(8, 0, 0),
                    EndAt = new TimeSpan(18, 0, 0),
                    BedAvailability = 100,
                    NumberOfDoctors = 50,
                    NumberOfNurses = 100,
                    DepartmentsCount = 10,
                    Latitude = 40.7128f,
                    Longitude = -74.0060f,
                    WebsiteUrl = "http://www.centralhospital.com",
                    LicenseNumber = "HOSP123456",
                    TaxIdentificationNumber = "TAX123456",
                    Rating = 4.5f,
                    HospitalAdminId = 1
                }
            );

            modelBuilder.Entity<Reception>().HasData(
                new Reception
                {
                    Id = 1,
                    HospitalId = 1,
                    Location = "Ground Floor",
                    StartAt = new TimeSpan(8, 0, 0),
                    EndAt = new TimeSpan(18, 0, 0)
                }
            );

            modelBuilder.Entity<Receptionist>().HasData(
                new Receptionist
                {
                    Id = 2,
                    Name = "receptionist1",
                    WorkingTime = new TimeSpan(9, 0, 0),
                    ReceptionId = 1,
                    NationalId = "11111111111111",
                    CreatedAt = DateTime.UtcNow,
                    Password = hashedPassword,
                    Salt = salt
                }
            );

            modelBuilder.Entity<ClinicSpecialization>().HasData(
               new ClinicSpecialization
               {
                   Id = 1,
                   Name = "Cardiology",
                   Description = "Heart-related treatments and procedures"
               }
           );

            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    HospitalId = 1,
                    SpecializationId = 1,
                    SpecializationName = "Cardiology",
                    Location = "First Floor, Room 101",
                    StartAt = new TimeSpan(9, 0, 0),
                    FinishAt = new TimeSpan(17, 0, 0),
                    AppointmentDurationInMinutes = 30
                }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 3,
                    Name = "Dr. John Doe",
                    NationalId = "22222222222222",
                    Email = "john.doe@hospital.com",
                    Password = hashedPassword,
                    HireDate = DateTime.Now,
                    WorkingTime = new TimeSpan(8, 0, 0),
                    HospitalId = 1,
                    ExperienceYears = 10,
                    Specialization = "Cardiology",
                    ClinicId = 1,
                    Salt = salt
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 4,
                    Name = "Jane Doe",
                    NationalId = "33333333333333",
                    Email = "jane.doe@hospital.com",
                    Password = hashedPassword,
                    ContactNumber = "123-456-7890",
                    Salt = salt,
                    CreatedAt = DateTime.UtcNow,
                }
            );

            //modelBuilder.Entity<Medicine>().HasData(
            //    new Medicine
            //    {
            //        Id = 1,
            //        Name = "Aspirin",
            //        Description = "Pain reliever",
            //        Cost = 20,
            //        Type = MedicineType.Tablet,
                   
            //    }
            //);

            //modelBuilder.Entity<EffectiveSubstance>().HasData(
            //    new EffectiveSubstance
            //    {
            //        Id = 1,
            //        Name = "Acetylsalicylic Acid",
            //        ChemicalFormula = "C9H8O4",
            //        Description = "Active ingredient in Aspirin",
            //        DiscoveryDate = DateTime.Now.AddYears(-100),
            //        ApprovedBy = "FDA",
            //        StabilityConditions = "Store in a cool, dry place"
            //    }
            //);

            //modelBuilder.Entity<Disease>().HasData(
            //    new Disease
            //    {
            //        Id = 1,
            //        Name = "Influenza",
            //        Description = "Viral infection",
            //        Contagious = true,
            //        Discovery_date = DateTime.Now.AddYears(-100),
            //        AffectedPopulation = 1000000,
            //        DiseaseCode = "FLU",
            //        CureRate = 95,
            //        FatalityRate = 1,
            //        TreatmentDurationInDays = "7",
            //        IsChronic = false,
            //        HasVaccine = true,
            //        CommonAgeGroup = AgeGroup.Adult,
            //        RiskFactors = "Weakened immune system",
            //        PreventionMeasures = "Vaccination, hand hygiene"
            //    }
            //);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = ApplicationRole.Admin
                },
                new Role
                {
                    Id = 2,
                    RoleName = ApplicationRole.Receptionist
                },
                new Role
                {
                    Id = 3,
                    RoleName = ApplicationRole.Doctor
                }, 
                new Role
                {
                    Id = 4,
                    RoleName = ApplicationRole.Patient
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { RoleId = 1, UserId = 1 },
                new UserRole() { RoleId = 2, UserId = 2 },
                new UserRole() { RoleId = 3, UserId = 3 },
                new UserRole() { RoleId = 4, UserId = 4 }
                );
        }

        private static void ApplySoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseClass<int> 
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

    }
}
