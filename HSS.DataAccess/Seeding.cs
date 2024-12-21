using HSS.DataAccess.Helpers;
using HSS.DataAccess.Repositories;
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
        public static void SeedAccounts(this ModelBuilder modelBuilder, AccountServicesHelpers accountServices, Helper helper)
        {
            var salt = accountServices.CreateSalt();
            var hashedPassword = accountServices.HashPasswordWithSalt(salt, "@Aa123456789");


            modelBuilder.Entity<HospitalAdmin>().HasData(
                new HospitalAdmin
                {
                    Id = 1,
                    Name = "ادمن 1 لمستشفي 1",
                    NationalId = helper.GenerateNationalID(true),
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
                   Name = "احمد محمود خالد عبد المجيد",
                   WorkingTime = new TimeSpan(9, 0, 0),
                   ReceptionId = 1,
                   NationalId = helper.GenerateNationalID(true),
                   CreatedAt = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
                   Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                   Salt = "yCO+h7P4PiwQp7rKKiy3mQ=="
               }
           );

            
        }
        public static void SeedDoctorsAndClinics(this ModelBuilder modelBuilder)
        {
            // Seed 10 Clinics
            var clinics = new List<Clinic>
            {
                new Clinic { Id = 1, Name = "عيادة القلب", HospitalId = 1, SpecializationId = 1, StartAt = new TimeSpan(8,0,0), FinishAt = new TimeSpan(16,0,0) },
                new Clinic { Id = 2, Name = "عيادة العظام", HospitalId = 1, SpecializationId = 2, StartAt = new TimeSpan(9,0,0), FinishAt = new TimeSpan(17,0,0) },
                new Clinic { Id = 3, Name = "عيادة الأطفال", HospitalId = 1, SpecializationId = 3, StartAt = new TimeSpan(8,0,0), FinishAt = new TimeSpan(16,0,0) },
                new Clinic { Id = 4, Name = "عيادة المخ والأعصاب", HospitalId = 1, SpecializationId = 4, StartAt = new TimeSpan(10,0,0), FinishAt = new TimeSpan(18,0,0) },
                new Clinic { Id = 5, Name = "عيادة الجلدية", HospitalId = 1, SpecializationId = 5, StartAt = new TimeSpan(9,0,0), FinishAt = new TimeSpan(17,0,0) },
                new Clinic { Id = 6, Name = "عيادة الأنف والأذن والحنجرة", HospitalId = 1, SpecializationId = 6, StartAt = new TimeSpan(8,0,0), FinishAt = new TimeSpan(16,0,0) },
                new Clinic { Id = 7, Name = "عيادة العيون", HospitalId = 1, SpecializationId = 7, StartAt = new TimeSpan(9,0,0), FinishAt = new TimeSpan(17,0,0) },
                new Clinic { Id = 8, Name = "عيادة الأسنان", HospitalId = 1, SpecializationId = 8, StartAt = new TimeSpan(8,0,0), FinishAt = new TimeSpan(16,0,0) },
                new Clinic { Id = 9, Name = "عيادة الطب النفسي", HospitalId = 1, SpecializationId = 9, StartAt = new TimeSpan(10,0,0), FinishAt = new TimeSpan(18,0,0) },
                new Clinic { Id = 10, Name = "عيادة الطب العام", HospitalId = 1, SpecializationId = 10, StartAt = new TimeSpan(8,0,0), FinishAt = new TimeSpan(16,0,0) }
            };

            modelBuilder.Entity<Clinic>().HasData(clinics);

            // Egyptian Names Lists
            var maleNames = new[] { "محمد", "أحمد", "مصطفى", "إبراهيم", "عبدالرحمن", "عبدالله", "حسن", "علي", "عمر", "يوسف", 
                                    "كريم", "محمود", "طارق", "خالد", "سيد", "هشام", "عادل", "رامي", "سامح", "وليد" };
            
            var femaleNames = new[] { "فاطمة", "نور", "سارة", "مريم", "رنا", "هدى", "أمل", "سلمى", "منى", "ريم",
                                    "دينا", "هبة", "رانيا", "ياسمين", "نورهان", "إيمان", "سمر", "ليلى", "عبير", "دعاء" };

            // Seed 20 Doctors (10 male, 10 female)
            var doctors = new List<Doctor>();
            var userRoles = new List<UserRole>();
            var random = new Random();

            for(int i = 10; i <= 30; i++)
            {
                var clinicId = ((i-4) % 10) + 1;
                var isFemale = i <= 13; // First 10 doctors are female
                
                string fullName;
                if(isFemale)
                {
                    // Female doctor (female first name + 4 male names)
                    fullName = $"{femaleNames[random.Next(femaleNames.Length)]} {maleNames[random.Next(maleNames.Length)]} " +
                                $"{maleNames[random.Next(maleNames.Length)]} {maleNames[random.Next(maleNames.Length)]} " +
                                $"{maleNames[random.Next(maleNames.Length)]}";
                }
                else
                {
                    // Male doctor (5 male names)
                    fullName = $"{maleNames[random.Next(maleNames.Length)]} {maleNames[random.Next(maleNames.Length)]} " +
                                $"{maleNames[random.Next(maleNames.Length)]} {maleNames[random.Next(maleNames.Length)]} " +
                                $"{maleNames[random.Next(maleNames.Length)]}";
                }

                doctors.Add(new Doctor
                {
                    Id = i,
                    Name = fullName,
                    NationalId = $"{i}".PadLeft(14, '0'),
                    Email = $"doctor{i}@hospital.com",
                    Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", //@Aa123456789
                    HireDate = new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193),
                    WorkingTime = new TimeSpan(8, 0, 0),
                    HospitalId = 1,
                    ExperienceYears = 5 + (i % 15), // 5-20 years experience
                    Specialization = clinics[clinicId-1].Name.Replace("عيادة ", ""),
                    ClinicId = clinicId,
                    Salt = "yCO+h7P4PiwQp7rKKiy3mQ=="
                });

                userRoles.Add(new UserRole { RoleId = 3, UserId = i });
            }

            modelBuilder.Entity<Doctor>().HasData(doctors);
            modelBuilder.Entity<UserRole>().HasData(userRoles);
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
                new UserRole() { RoleId = 2, UserId = 2 }
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
