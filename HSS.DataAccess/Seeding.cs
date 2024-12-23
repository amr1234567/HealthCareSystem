using HSS.DataAccess.Contexts;
using HSS.DataAccess.Helpers;
using HSS.DataAccess.Repositories;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using HSS.Services.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HSS.DataAccess
{
    public static class Seeding
    {
        public static async Task SeedAccountsAsync(this ApplicationDbContext context, AccountServicesHelpers accountServices)
        {
            if (await context.HospitalAdmins.AnyAsync() || await context.Receptionists.AnyAsync())
                return;

            var salt = accountServices.CreateSalt();
            var hashedPassword = accountServices.HashPasswordWithSalt(salt, "@Aa123456789");

            if (await context.Hospitals.AnyAsync())
                return;

            var hospital = new Hospital
            {
                Name = "المستشفي المركزي بالقاهرة الكبري",
                Location = "منطقة السادس من اكتوبر ، الحي الرابع",
                Capacity = 300,
                ContactNumber = "123-456-7890",
                Email = "info@centralhospital.com",
                EstablishedDate = new DateTime(1974, 12, 21, 8, 20, 9, 796, DateTimeKind.Local),
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
            };

            var reception = new Reception
            {
                Hospital = hospital,
                Location = "الطابق الارضي",
                StartAt = new TimeSpan(8, 0, 0),
                EndAt = new TimeSpan(18, 0, 0)
            };

            await context.Hospitals.AddAsync(hospital);
            await context.Receptions.AddAsync(reception);

            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == ApplicationRole.HospitalAdmin);
            var hospitalAdmin = new HospitalAdmin
            {
                Name = "ادمن 1 لمستشفي 1",
                NationalId = Helper.GenerateNationalID(true),
                Email = "admin@hospital.com",
                Salt = "yCO+h7P4PiwQp7rKKiy3mQ==",
                Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                CreatedAt = DateTime.UtcNow,
                Hospital = hospital,
                Roles = [adminRole]
            };
            await context.HospitalAdmins.AddAsync(hospitalAdmin);
            await context.SaveChangesAsync();

            var receptionistRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == ApplicationRole.Receptionist);

            var receptionist = new Receptionist
            {
                Name = "احمد محمود خالد عبد المجيد",
                WorkingTime = new TimeSpan(9, 0, 0),
                NationalId = "26204131375238",
                CreatedAt = DateTime.UtcNow,
                Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",//@Aa123456789
                Salt = "yCO+h7P4PiwQp7rKKiy3mQ==",
                Reception = reception,
                Roles = [receptionistRole]
            };

            await context.Receptionists.AddAsync(receptionist);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDoctorsAndClinicsAsync(this ApplicationDbContext context)
        {
            if (await context.Clinics.AnyAsync() || await context.Doctors.AnyAsync())
                return;

            var clinics = new List<Clinic>();
            var shifts = new[]
            {
                (start: new TimeSpan(8,0,0), end: new TimeSpan(14,0,0)),  // صباحي
                (start: new TimeSpan(14,0,0), end: new TimeSpan(20,0,0)), // مسائي
            };

            var random = new Random();
            var hospital = await context.Hospitals.FirstOrDefaultAsync();
            // إنشاء 3 عيادات لكل تخصص
            var specs = await context.ClinicSpecializations.ToListAsync();
            foreach (var spec in specs)
            {
                for (int shiftIndex = 0; shiftIndex < shifts.Length; shiftIndex++)
                {
                    var shift = shifts[shiftIndex];
                    var shiftName = shiftIndex == 0 ? "صباحي" : (shiftIndex == 1 ? "مسائي" : "ليلي");

                    clinics.Add(new Clinic
                    {
                        Hospital = hospital,
                        Name = $"عيادة {spec.Name} ({shiftName})",
                        Specialization = spec,
                        StartAt = shift.start,
                        FinishAt = shift.end,
                        AppointmentDurationInMinutes = 30,
                    });
                }
            }

            await context.Clinics.AddRangeAsync(clinics);
            await context.SaveChangesAsync();

            // إضافة الأطباء
            var doctors = new List<Doctor>();
            var role = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == ApplicationRole.Doctor);

            foreach (var clinic in await context.Clinics.ToListAsync())
            {
                for (int i = 0; i < 2; i++)
                {
                    var isFemale = random.Next(2) == 0;
                    var fullName = GenerateRandomName(isFemale, random);
                    var nationalId = Helper.GenerateNationalID(!isFemale);

                    doctors.Add(new Doctor
                    {
                        Hospital = await context.Hospitals.FirstOrDefaultAsync(),
                        Name = fullName,
                        NationalId = nationalId,
                        Email = $"doctor.{nationalId}@hospital.com",
                        Password = "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=",
                        HireDate = DateTime.UtcNow.AddYears(-random.Next(1, 10)),
                        ExperienceYears = 5 + random.Next(15),
                        Salt = "yCO+h7P4PiwQp7rKKiy3mQ==",
                        CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 1_000)),
                        StartAt = shifts[i].start,
                        WorkingTime = TimeSpan.FromHours(6),
                        Clinic = clinic,
                        Roles = [role],
                    });
                }
            }

            await context.Doctors.AddRangeAsync(doctors);
            await context.SaveChangesAsync();
        }

        public static async Task SeedRolesAsync(this ApplicationDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var roles = new List<Role>
            {
                new Role { RoleName = ApplicationRole.AdminstrationAdmin },
                new Role {  RoleName = ApplicationRole.HospitalAdmin },
                new Role { RoleName = ApplicationRole.Receptionist },
                new Role { RoleName = ApplicationRole.Doctor },
                new Role { RoleName = ApplicationRole.Patient },
                new Role { RoleName = ApplicationRole.Pharmacist },
                new Role { RoleName = ApplicationRole.LabManager },
                new Role { RoleName = ApplicationRole.RadiologyManager }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        public static async Task SeedSystemDetailsAsync(this ApplicationDbContext context)
        {
            if (await context.ClinicSpecializations.AnyAsync())
                return;

            var specializations = new List<ClinicSpecialization>
            {
              new ClinicSpecialization { Name = "أمراض القلب", Description = "علاجات وإجراءات متعلقة بالقلب",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/heart-disease.svg")
              },
              new ClinicSpecialization { Name = "الأمراض الجلدية", Description = "علاجات وإجراءات متعلقة بالجلد",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/skin-disease.svg")
              },
              new ClinicSpecialization {  Name = "الأعصاب", Description = "علاجات الجهاز العصبي والدماغ" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/neurological-diseases.svg")
              },
              new ClinicSpecialization { Name = "جراحة العظام", Description = "علاجات العظام والمفاصل",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/bone-disease.svg")
              },
              new ClinicSpecialization { Name = "طب الأطفال", Description = "الرعاية الصحية للأطفال" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/children-disease.svg")
              },
              new ClinicSpecialization {    Name = "أمراض النساء", Description = "صحة الجهاز التناسلي للمرأة",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/women-disease.svg")
              },
              new ClinicSpecialization { Name = "طب العيون", Description = "رعاية العين والرؤية" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/eye-disease.svg")
              },
              new ClinicSpecialization { Name = "الأورام", Description = "علاجات السرطان" ,
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/cancer-disease.svg")
              },
              new ClinicSpecialization { Name = "الطب النفسي", Description = "علاجات الصحة النفسية",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/mental-disease.svg")
              },
              new ClinicSpecialization { Name = "جراحة المسالك البولية", Description = "علاجات الجهاز البولي",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/urinary-disease.svg")
              },
              new ClinicSpecialization { Name = "أمراض الجهاز الهضمي", Description = "علاجات الجهاز الهضمي",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/digestive-disease.svg")
              },
              new ClinicSpecialization { Name = "أمراض الرئة", Description = "علاجات الجهاز التنفسي والرئتين",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/lung-disease.svg")
              },
              new ClinicSpecialization { Name = "أمراض الدم", Description = "علاجات متعلقة بالدم",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/blood-disease.svg")
              },
              new ClinicSpecialization { Name = "طب الأنف والأذن والحنجرة", Description = "علاجات الأنف والأذن والحنجرة",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/ear-disease.svg")
              },
              new ClinicSpecialization { Name = "جراحة التجميل", Description = "الجراحة التجميلية والترميمية",
                  Icon = ConvertImageToText("wwwroot/icons/medical-icons/cosmetic-surgery.svg")
              }
            };
            await context.ClinicSpecializations.AddRangeAsync(specializations);
            await context.SaveChangesAsync();

            var hospital = await context.Hospitals.Include(h => h.ClinicSpecializations).FirstOrDefaultAsync();
            foreach (var spec in specializations)
            {
                if (hospital.ClinicSpecializations == null)
                    hospital.ClinicSpecializations = [spec];
                hospital.ClinicSpecializations.Add(spec);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedClinicAppointmentsAsync(this ApplicationDbContext context)
        {
            if (await context.ClinicAppointment.AnyAsync())
                return;
            var appointments = new List<ClinicAppointment>();
            var random = new Random();
            var startDate = DateTime.UtcNow.Date.AddDays(-100);

            // الحصول على جميع العيادات والأطباء والمرضى مرة واحدة لتحسين الأداء
            var clinics = await context.Clinics.ToListAsync();
            var doctors = await context.Doctors.ToListAsync();
            var patients = await context.Patients.ToListAsync();

            if (!clinics.Any() || !doctors.Any() || !patients.Any())
                return;

            foreach (var clinic in clinics)
            {
                var clinicDoctors = doctors.Where(d => d.ClinicId == clinic.Id).ToList();
                if (!clinicDoctors.Any()) continue;

                var appointmentDuration = TimeSpan.FromMinutes(clinic.AppointmentDurationInMinutes);
                var workingMinutes = (clinic.FinishAt - clinic.StartAt).TotalMinutes;
                var appointmentsPerDay = (int)(workingMinutes / appointmentDuration.TotalMinutes);

                for (int day = 0; day < 150; day++)
                {
                    var currentDate = startDate.AddDays(day);

                    for (int slot = 0; slot < appointmentsPerDay; slot++)
                    {
                        
                        if(random.NextDouble() > 0.6)
                        {
                            var appointmentTime = clinic.StartAt.Add(TimeSpan.FromMinutes(slot * appointmentDuration.TotalMinutes));
                            var appointmentDate = currentDate.Add(appointmentTime);

                            var doctor = clinicDoctors[random.Next(clinicDoctors.Count)];
                            var patient = patients[random.Next(patients.Count)];

                            var appointment = new ClinicAppointment
                            {
                                HospitalId = clinic.HospitalId,
                                PatientId = patient.Id,
                                ClinicId = clinic.Id,
                                DoctorId = doctor.Id,
                                AppointmentDate = appointmentDate,
                                Duration = appointmentDuration,
                                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30)),
                                UpdatedAt = DateTime.UtcNow,
                                Notes = GenerateRandomNotes(random),
                                ReasonForVisit = GetRandomReason(random),
                                AppointmentType = GetRandomAppointmentType(random),
                                IsConfirmed = appointmentDate < DateTime.UtcNow || (appointmentDate > DateTime.UtcNow && appointmentDate <= DateTime.UtcNow.AddHours(2)),
                                ClinicAppointmentRelatedTo = null,
                                IsEnd = appointmentDate < DateTime.UtcNow,
                            };
                            appointments.Add(appointment);
                        }

                        if(appointments.Count > 100)
                        {
                            await context.ClinicAppointment.AddRangeAsync(appointments);
                            await context.SaveChangesAsync();
                            appointments.Clear();
                        }
                    }
                }
            }
            if (appointments.Any())
            {
                await context.ClinicAppointment.AddRangeAsync(appointments);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedPatients(this ApplicationDbContext context)
        {
            var patients = new List<Patient>();
            var role = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == ApplicationRole.Patient);
            for (int i = 0; i < 5_000; i++)
            {
                var state = GetRandomElement(GetGovs());
                var customDate = Helper.GetRandomDate();

                var gender = Helper.GenerateRandomGender();

                var random = new Random();

                var user = new Patient()
                {
                    Address = new Domain.ObjectValues.PatientAddress()
                    {
                        StreetName = Helper.GenerateStreetName(),
                        City = state.Key,
                        State = "Egypt"
                    },
                    AgeCategory = Helper.GetAgeGroup(customDate),
                    DateOfBirth = customDate,
                    CreatedAt = DateTime.UtcNow,
                    Name = gender == Gender.Female ?
                    Helper.GenerateFullNameFemale()
                    : Helper.GenerateFullNameMale(),
                    Sex = gender,
                    NationalId = Helper.GenerateNationalID(customDate, state.Value, gender == Gender.Male),
                    Roles = [role],
                    
                };
                patients.Add(user);
            }

            await context.Patients.AddRangeAsync(patients);
            await context.SaveChangesAsync();
        }

        private static string GenerateRandomName(bool isFemale, Random random)
        {
            var maleNames = new[] { "محمد", "أحمد", "علي", "عمر", "خالد", "عبدالله", "يوسف", "حسن", "إبراهيم", "سعيد" };
            var femaleNames = new[] { "فاطمة", "عائشة", "مريم", "زينب", "سارة", "نور", "ليلى", "هدى", "أمل", "رنا" };
            var lastNames = new[] { "محمد", "أحمد", "علي", "حسن", "إبراهيم", "عمر", "خالد", "سعيد", "عبدالله", "يوسف" };

            var firstName = isFemale ? femaleNames[random.Next(femaleNames.Length)] : maleNames[random.Next(maleNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];

            return $"{firstName} {lastName}";
        }

        private static string GetSpecializationName(int specId)
        {
            return specId switch
            {
                1 => "أمراض القلب",
                2 => "الأمراض الباطنية",
                3 => "طب الأطفال",
                4 => "النساء والتوليد",
                5 => "العظام",
                16 => "الأنف والأذن والحنجرة",
                19 => "المسالك البولية",
                20 => "الأمراض الجلدية",
                _ => "تخصص غير معروف"
            };
        }

        private static string GenerateRandomNotes(Random random)
        {
            var notes = new[]
            {
                "فحص روتيني",
                "متابعة العلاج",
                "تجديد الوصفة الطبية",
                "شكوى من آلام",
                "فحص نتائج التحاليل",
                "استشارة طبية",
                "متابعة بعد العملية",
                "تقييم الحالة الصحية"
            };

            return notes[random.Next(notes.Length)];
        }

        private static string GetRandomReason(Random random)
        {
            var reasons = new[]
            {
                "آلام في الصدر",
                "صداع مستمر",
                "ارتفاع ضغط الدم",
                "متابعة دورية",
                "فحص عام",
                "تعب وإرهاق",
                "مشاكل في المعدة",
                "حساسية جلدية",
                "مشاكل في التنفس",
                "آلام في المفاصل"
            };

            return reasons[random.Next(reasons.Length)];
        }

        private static AppointmentType GetRandomAppointmentType(Random random)
        {
            var types = new[]
            {
                AppointmentType.FirstTime,
                AppointmentType.FollowUp,
                AppointmentType.ReExamination
            };

            return types[random.Next(types.Length)];
        }
        public static string ConvertImageToText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Image file not found.", filePath);

            return File.ReadAllText(filePath);
        }
        private static Dictionary<string, string> GetGovs()
        {
            return new Dictionary<string, string>
            {
                { "القاهرة", "01" },
                { "الإسكندرية", "02" },
                { "بورسعيد", "03" },
                { "السويس", "04" },
                { "دمياط", "11" },
                { "الدقهلية", "12" },
                { "الشرقية", "13" },
                { "كفر الشيخ", "14" },
                { "الغربية", "15" },
                { "المنوفية", "16" },
                { "البحيرة", "17" },
                { "الإسماعيلية", "18" },
                { "الجيزة", "19" },
                { "بني سويف", "22" },
                { "الفيوم", "23" },
                { "المنيا", "24" },
                { "أسيوط", "25" },
                { "سوهاج", "26" },
                { "قنا", "27" },
                { "أسوان", "28" },
                { "الأقصر", "29" },
                { "البحر الأحمر", "31" },
                { "الوادي الجديد", "32" },
                { "مطروح", "33" },
                { "شمال سيناء", "34" },
                { "جنوب سيناء", "35" }
            };
        }
        public static KeyValuePair<TKey, TValue> GetRandomElement<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                throw new ArgumentException("The dictionary must not be null or empty.");
            }

            Random random = new Random();
            int randomIndex = random.Next(dictionary.Count);

            // Get the element at the random index
            foreach (var kvp in dictionary)
            {
                if (randomIndex == 0)
                {
                    return kvp;
                }
                randomIndex--;
            }

            // This line should never be reached
            throw new InvalidOperationException("Failed to get a random element from the dictionary.");
        }

    }
}
