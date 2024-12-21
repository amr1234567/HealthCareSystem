using HSS.DataAccess.Helpers;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess
{
    public static class Seeding
    {
        public static void SeedAccounts(this ModelBuilder modelBuilder,AccountServicesHelpers accountServices)
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
                    Salt = "yCO+h7P4PiwQp7rKKiy3mQ==",
                    Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                    CreatedAt = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
                    HospitalId = 1
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
                   CreatedAt = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
                   Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                   Salt = "yCO+h7P4PiwQp7rKKiy3mQ=="
               }
           );

            modelBuilder.Entity<Doctor>().HasData(
               new Doctor
               {
                   Id = 3,
                   Name = "Dr. John Doe",
                   NationalId = "22222222222222",
                   Email = "john.doe@hospital.com",
                   Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                   HireDate = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
                   WorkingTime = new TimeSpan(8, 0, 0),
                   HospitalId = 1,
                   ExperienceYears = 10,
                   Specialization = "Cardiology",
                   ClinicId = 1,
                   Salt = "yCO+h7P4PiwQp7rKKiy3mQ=="
               }
           );

            modelBuilder.Entity<Patient>().HasData(
             new Patient
             {
                 Id = 4,
                 Name = "Jane Doe",
                 NationalId = "33333333333333",
                 Email = "jane.doe@hospital.com",
                 Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", //@Aa123456789
                 ContactNumber = "123-456-7890",
                 Salt = "yCO+h7P4PiwQp7rKKiy3mQ==",
                 CreatedAt = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
             }
            );
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
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
    
        public static void SeedHospitalDetails(this  ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>().HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "Central Hospital",
                    Location = "123 Main St",
                    Capacity = 300,
                    ContactNumber = "123-456-7890",
                    Email = "info@centralhospital.com",
                    EstablishedDate = new DateTime(1974, 12, 21, 8, 20, 9, 796, DateTimeKind.Local).AddTicks(3592),
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

            modelBuilder.Entity<ClinicSpecializationHospital>().HasData(

                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 1
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 2
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 3
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 4
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 5
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 16
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 19
                },
                new ClinicSpecializationHospital
                {
                    HospitalId = 1,
                    ClinicSpecializationId = 20
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
                    AppointmentDurationInMinutes = 30,
                }
            );
        }
    
        public static void SeedSystemDetails(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClinicSpecialization>().HasData(
              new ClinicSpecialization { Id = 1, Name = "أمراض القلب", Description = "علاجات وإجراءات متعلقة بالقلب",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/heart-disease.svg") 
              },
              new ClinicSpecialization { Id = 2, Name = "الأمراض الجلدية", Description = "علاجات وإجراءات متعلقة بالجلد",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/skin-disease.svg")
              },
              new ClinicSpecialization { Id = 3, Name = "الأعصاب", Description = "علاجات الجهاز العصبي والدماغ" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/neurological-diseases.svg")
              },
              new ClinicSpecialization { Id = 4, Name = "جراحة العظام", Description = "علاجات العظام والمفاصل",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/bone-disease.svg")
              },
              new ClinicSpecialization { Id = 5, Name = "طب الأطفال", Description = "الرعاية الصحية للأطفال" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/children-disease.svg")
              },
              new ClinicSpecialization { Id = 6, Name = "أمراض النساء", Description = "صحة الجهاز التناسلي للمرأة",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/women-disease.svg")
              },
              new ClinicSpecialization { Id = 7, Name = "طب العيون", Description = "رعاية العين والرؤية" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/eye-disease.svg") 
              },    
              new ClinicSpecialization { Id = 8, Name = "الأورام", Description = "علاجات السرطان" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/cancer-disease.svg") 
              },
              new ClinicSpecialization { Id = 9, Name = "الطب النفسي", Description = "علاجات الصحة النفسية",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/mental-disease.svg")
              },
              new ClinicSpecialization { Id = 10, Name = "جراحة المسالك البولية", Description = "علاجات الجهاز البولي",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/urinary-disease.svg")
              },
              new ClinicSpecialization { Id = 11, Name = "أمراض الجهاز الهضمي", Description = "علاجات الجهاز الهضمي",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/digestive-disease.svg")
              },
              new ClinicSpecialization { Id = 13, Name = "أمراض الرئة", Description = "علاجات الجهاز التنفسي والرئتين",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/lung-disease.svg")
              },
              new ClinicSpecialization { Id = 16, Name = "أمراض الدم", Description = "علاجات متعلقة بالدم",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/blood-disease.svg")
              },
              new ClinicSpecialization { Id = 19, Name = "طب الأنف والأذن والحنجرة", Description = "علاجات الأنف والأذن والحنجرة",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/ear-disease.svg")
              },
              new ClinicSpecialization { Id = 20, Name = "جراحة التجميل", Description = "الجراحة التجميلية والترميمية",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/cosmetic-surgery.svg")
              }
          );
        }

        public static string ConvertImageToText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Image file not found.", filePath);

            return File.ReadAllText(filePath);
        }

    }
}
