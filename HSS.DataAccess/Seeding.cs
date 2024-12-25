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

        public static async Task SeedDiseasesAndRelatedDataAsync(this ApplicationDbContext context)
        
        {
            if (await context.Set<Disease>().AnyAsync())
                return;

            // Create symptoms
            var symptoms = new List<Symptom>
            {
                new Symptom {
                    Name = "ارتفاع درجة الحرارة",
                    Description = "ارتفاع في درجة حرارة الجسم عن 37 درجة مئوية",
                    Duration = 3,
                    IsChronic = false,
                    TreatmentRequired = true
                },
                new Symptom {
                    Name = "سعال جاف",
                    Description = "سعال متكرر بدون بلغم",
                    Duration = 7,
                    IsChronic = false,
                    TreatmentRequired = true
                },
                new Symptom {
                    Name = "صداع شديد",
                    Age = AgeGroup.Adult,
                    Description = "ألم حاد في الرأس مع احتمال الغثيان",
                    Duration = 12,
                    IsChronic = false,
                    TreatmentRequired = true
                },
                new Symptom {
                    Name = "آلام المفاصل",
                    Description = "ألم وتصلب في المفاصل مع صعوبة في الحركة",
                    Duration = 30,
                    IsChronic = true,
                    TreatmentRequired = true
                },
                new Symptom {
                    Name = "ضيق في التنفس",
                    Description = "صعوبة في التنفس مع الشعور بعدم كفاية الهواء",
                    Duration = 6,
                    IsChronic = false,
                    TreatmentRequired = true
                }
            };

            // Create effective substances
            var effectiveSubstances = new List<EffectiveSubstance>
            {
                new EffectiveSubstance {
                    Name = "باراسيتامول",
                    ChemicalFormula = "C8H9NO2",
                    Description = "مسكن للألم وخافض للحرارة",
                    DiscoveryDate = new DateTime(1877, 1, 1),
                    ApprovedBy = "FDA",
                    StabilityConditions = "يحفظ في درجة حرارة الغرفة",
                    PrimaryUsage = "تخفيف الألم وخفض الحرارة",
                    AlternativeNames = "أسيتامينوفين"
                },
                new EffectiveSubstance {
                    Name = "أموكسيسيلين",
                    ChemicalFormula = "C16H19N3O5S",
                    Description = "مضاد حيوي واسع الطيف",
                    DiscoveryDate = new DateTime(1972, 1, 1),
                    ApprovedBy = "FDA",
                    StabilityConditions = "يحفظ في مكان بارد وجاف",
                    PrimaryUsage = "علاج الالتهابات البكتيرية",
                    AlternativeNames = "أموكسيل"
                },
                new EffectiveSubstance {
                    Name = "ميتفورمين",
                    ChemicalFormula = "C4H11N5",
                    Description = "دواء لعلاج السكري",
                    DiscoveryDate = new DateTime(1922, 1, 1),
                    ApprovedBy = "FDA",
                    StabilityConditions = "يحفظ بعيداً عن الضوء والرطوبة",
                    PrimaryUsage = "تنظيم مستوى السكر في الدم",
                    AlternativeNames = "جلوكوفاج"
                }
            };

            // Create diseases with their relationships
            var diseases = new List<Disease>
            {
                new Disease {
                    Name = "إنفلونزا موسمية",
                    Description = "عدوى فيروسية تصيب الجهاز التنفسي",
                    Severity = Severity.Moderate,
                    Contagious = true,
                    Discovery_date = new DateTime(1918, 1, 1),
                    AffectedPopulation = 1000000,
                    DiseaseCode = "J10",
                    CureRate = 99.5f,
                    FatalityRate = 0.1f,
                    TreatmentDurationInDays = "7",
                    IsChronic = false,
                    HasVaccine = true,
                    CommonAgeGroup = AgeGroup.All,
                    CommonGender = Gender.All,
                    RiskFactors = "كبار السن، ضعف المناعة، الأمراض المزمنة",
                    GeographicSpread = "عالمي",
                    LastOutbreakDate = new DateTime(2023, 1, 1),
                    ResearchStatus = ResearchStatus.Ongoing,
                    PreventionMeasures = "غسل اليدين، ارتداء الكمامة، تجنب المخالطة المباشرة",
                    Notes = "تظهر بشكل موسمي خاصة في فصل الشتاء"
                },
                new Disease {
                    Name = "السكري النوع الثاني",
                    Description = "اضطراب في التمثيل الغذائي يؤثر على كيفية استخدام الجسم للسكر",
                    Severity = Severity.Severe,
                    Contagious = false,
                    Discovery_date = new DateTime(1935, 1, 1),
                    AffectedPopulation = 500000,
                    DiseaseCode = "E11",
                    CureRate = 0f,
                    FatalityRate = 15f,
                    TreatmentDurationInDays = "مدى الحياة",
                    IsChronic = true,
                    HasVaccine = false,
                    CommonAgeGroup = AgeGroup.Adult,
                    CommonGender = Gender.All,
                    RiskFactors = "السمنة، قلة النشاط البدني، التاريخ العائلي",
                    GeographicSpread = "عالمي",
                    ResearchStatus = ResearchStatus.Ongoing,
                    PreventionMeasures = "نظام غذائي صحي، ممارسة الرياضة، الحفاظ على وزن صحي"
                },
                new Disease {
                    Name = "التهاب المفاصل الروماتويدي",
                    Description = "مرض مناعي يسبب التهاب المفاصل",
                    Severity = Severity.Severe,
                    Contagious = false,
                    Discovery_date = new DateTime(1800, 1, 1),
                    AffectedPopulation = 200000,
                    DiseaseCode = "M05",
                    CureRate = 0f,
                    FatalityRate = 5f,
                    TreatmentDurationInDays = "مدى الحياة",
                    IsChronic = true,
                    HasVaccine = false,
                    CommonAgeGroup = AgeGroup.Adult,
                    CommonGender = Gender.Female,
                    RiskFactors = "التدخين، التاريخ العائلي، الجنس الأنثوي",
                    PreventionMeasures = "تجنب التدخين، ممارسة التمارين الخفيفة"
                },
                // ... continuing with more diseases ...
            };

            // Add 7 more diseases following similar pattern
            diseases.AddRange(new[]
            {
                new Disease {
                    Name = "ارتفاع ضغط الدم",
                    Description = "ارتفاع مستمر في ضغط الدم الشرياني",
                    Severity = Severity.Moderate,
                    Contagious = false,
                    Discovery_date = new DateTime(1900, 1, 1),
                    AffectedPopulation = 800000,
                    DiseaseCode = "I10",
                    CureRate = 0f,
                    FatalityRate = 10f,
                    TreatmentDurationInDays = "مدى الحياة",
                    IsChronic = true,
                    HasVaccine = false,
                    CommonAgeGroup = AgeGroup.Adult,
                    CommonGender = Gender.All,
                    RiskFactors = "التدخين، السمنة، قلة النشاط البدني، الإجهاد",
                    PreventionMeasures = "نظام غذائي صحي، ممارسة الرياضة، تقليل الملح"
                },
                new Disease {
                    Name = "الربو",
                    Description = "مرض تنفسي مزمن يسبب صعوبة في التنفس",
                    Severity = Severity.Moderate,
                    Contagious = false,
                    Discovery_date = new DateTime(1850, 1, 1),
                    AffectedPopulation = 300000,
                    DiseaseCode = "J45",
                    CureRate = 0f,
                    FatalityRate = 1f,
                    TreatmentDurationInDays = "مدى الحياة",
                    IsChronic = true,
                    HasVaccine = false,
                    CommonAgeGroup = AgeGroup.Child,
                    CommonGender = Gender.All,
                    RiskFactors = "التاريخ العائلي، الحساسية، التدخين",
                    PreventionMeasures = "تجنب المحفزات، استخدام الأدوية الوقائية"
                }
                // ... Add the remaining diseases here
            });


            foreach (var disease in diseases)
            {
                // Assign relationships to diseases
                disease.Symptoms = symptoms.Take(Random.Shared.Next(2, 4)).ToList();
                disease.EffectiveSubstances = effectiveSubstances.Take(Random.Shared.Next(1, 3)).ToList();

                // Ensure TreatmentDurationInDays is valid
                if (int.TryParse(disease.TreatmentDurationInDays, out int days))
                {
                    disease.TreatmentDurationInDays = days > 0 && days < 3650
                        ? days.ToString()
                        : "7"; // Default to 7 days if out of range
                }
                else
                {
                    disease.TreatmentDurationInDays = "7"; // Default value
                }
            }


            await context.AddRangeAsync(symptoms);
            await context.AddRangeAsync(effectiveSubstances);
            await context.AddRangeAsync(diseases);
            await context.SaveChangesAsync();
        }
       
        public static async Task SeedMedicineAsync(this ApplicationDbContext context){
        if (await context.Set<Disease>().AnyAsync())
                return;
            var medicines = new List<Medicine>
        {
            new Medicine
            {
                Id = 1,
                Name = "Advil",
                Description = "مسكن للآلام وخافض للحرارة.",
                Type = MedicinesType.Tablet,
                Manufacturer = "Pfizer",
                ApprovalDate = new DateTime(1974, 1, 1),
                StorageConditions = "يحفظ بعيدًا عن متناول الأطفال.",
                PrescriptionRequired = false,
                Cost = 5.5f
            },
            new Medicine
            {
                Name = "Panadol",
                Description = "مسكن للآلام وخافض للحرارة.",
                Type = MedicinesType.Tablet,
                Manufacturer = "GSK",
                ApprovalDate = new DateTime(1955, 1, 1),
                StorageConditions = "يحفظ في مكان بارد وجاف.",
                PrescriptionRequired = false,
                Cost = 3.0f
            },
            new Medicine
            {
                Name = "Aspirin",
                Description = "يستخدم لتخفيف الألم وتقليل الحمى.",
                Type = MedicinesType.Tablet,
                Manufacturer = "Bayer",
                ApprovalDate = new DateTime(1899, 1, 1),
                StorageConditions = "يحفظ في درجة حرارة الغرفة.",
                PrescriptionRequired = true,
                Cost = 4.5f
            },
            new Medicine
            {
                Name = "Zyrtec",
                Description = "مضاد للحساسية يخفف أعراض العطس والحكة.",
                Type = MedicinesType.Tablet,
                Manufacturer = "UCB",
                ApprovalDate = new DateTime(1995, 1, 1),
                StorageConditions = "يحفظ في مكان بارد وجاف.",
                PrescriptionRequired = false,
                Cost = 6.0f
            },
            new Medicine
            {
                Name = "Nexium",
                Description = "دواء لعلاج الحموضة وارتجاع المريء.",
                Type = MedicinesType.Tablet,
                Manufacturer = "AstraZeneca",
                ApprovalDate = new DateTime(2000, 1, 1),
                StorageConditions = "يحفظ بعيدًا عن الحرارة والرطوبة.",
                PrescriptionRequired = true,
                Cost = 8.0f
            },
            new Medicine
            {
                Name = "Crestor",
                Description = "دواء لخفض الكوليسترول في الدم.",
                Type = MedicinesType.Tablet ,
                Manufacturer = "AstraZeneca",
                ApprovalDate = new DateTime(2003, 1, 1),
                StorageConditions = "يحفظ في مكان جاف وبارد.",
                PrescriptionRequired = true,
                Cost = 12.0f
            },
            new Medicine
            {
                Name = "Ventolin",
                Description = "بخاخ لتخفيف أعراض الربو وضيق التنفس.",
                Type = MedicinesType.Tablet,
                Manufacturer = "GSK",
                ApprovalDate = new DateTime(1980, 1, 1),
                StorageConditions = "يحفظ في درجة حرارة الغرفة.",
                PrescriptionRequired = false,
                Cost = 10.0f
            },
            new Medicine
            {
                Name = "Tamiflu",
                Description = "دواء مضاد للفيروسات لعلاج الإنفلونزا.",
                Type = MedicinesType.Tablet,
                Manufacturer = "Roche",
                ApprovalDate = new DateTime(1999, 1, 1),
                StorageConditions = "يحفظ في الثلاجة عند الحاجة.",
                PrescriptionRequired = true,
                Cost = 15.0f
            }
        };
            await context.Medicines.AddRangeAsync(medicines);
            await context.SaveChangesAsync();   
        }

        public static async Task SeedLabTestsAsync(this ApplicationDbContext context)
        {
            if (await context.Set<LabCenterTest>().AnyAsync())
                return;

            var labTests = new List<LabCenterTest>
            {
                new LabCenterTest 
                {
                    Name = "Complete Blood Count (CBC)",
                    Description = "فحص شامل لمكونات الدم يشمل خلايا الدم الحمراء والبيضاء والصفائح الدموية"
                },
                new LabCenterTest 
                {
                    Name = "Blood Sugar Test",
                    Description = "قياس مستوى السكر في الدم (صائم وفاطر)"
                },
                new LabCenterTest 
                {
                    Name = "Liver Function Test",
                    Description = "تقييم وظائف الكبد من خلال قياس الإنزيمات والبروتينات"
                },
                new LabCenterTest 
                {
                    Name = "Kidney Function Test",
                    Description = "فحص وظائف الكلى يشمل قياس الكرياتينين واليوريا"
                },
                new LabCenterTest 
                {
                    Name = "Thyroid Function Test",
                    Description = "قياس مستويات هرمونات الغدة الدرقية"
                },
                new LabCenterTest 
                {
                    Name = "Lipid Profile",
                    Description = "قياس مستويات الدهون في الدم بما في ذلك الكوليسترول والدهون الثلاثية"
                },
                new LabCenterTest 
                {
                    Name = "Urine Analysis",
                    Description = "تحليل شامل للبول للكشف عن الالتهابات والأمراض"
                },
                new LabCenterTest 
                {
                    Name = "HbA1c Test",
                    Description = "قياس متوسط مستوى السكر في الدم خلال 3 أشهر"
                },
                new LabCenterTest 
                {
                    Name = "Vitamin D Test",
                    Description = "قياس مستوى فيتامين د في الدم"
                },
                new LabCenterTest 
                {
                    Name = "Iron Studies",
                    Description = "قياس مستويات الحديد والفيريتين في الدم"
                }
            };

            await context.AddRangeAsync(labTests);
            await context.SaveChangesAsync();
        }

        public static async Task SeedRadiologyTestTypesAsync(this ApplicationDbContext context)
        {
            if (await context.Set<RadiologyTestType>().AnyAsync())
                return;

            var radiologyTests = new List<RadiologyTestType>
            {
                new RadiologyTestType 
                {
                    Name = "X-Ray (الأشعة السينية)",
                    Description = "تصوير تشخيصي باستخدام الأشعة السينية للعظام والصدر والمفاصل"
                },
                new RadiologyTestType 
                {
                    Name = "CT Scan (الأشعة المقطعية)",
                    Description = "تصوير مقطعي محوسب يوفر صور ثلاثية الأبعاد للأعضاء الداخلية"
                },
                new RadiologyTestType 
                {
                    Name = "MRI (التصوير بالرنين المغناطيسي)",
                    Description = "تصوير تفصيلي للأنسجة الرخوة والمفاصل والدماغ باستخدام المجال المغناطيسي"
                },
                new RadiologyTestType 
                {
                    Name = "Ultrasound (الموجات فوق الصوتية)",
                    Description = "تصوير باستخدام الموجات الصوتية للأعضاء الداخلية والجنين"
                },
                new RadiologyTestType 
                {
                    Name = "Mammography (تصوير الثدي)",
                    Description = "تصوير خاص للكشف عن سرطان الثدي وأمراض الثدي الأخرى"
                },
                new RadiologyTestType 
                {
                    Name = "PET Scan (التصوير المقطعي بالإصدار البوزيتروني)",
                    Description = "تصوير متقدم يستخدم للكشف عن الأورام ومتابعة العلاج"
                },
                new RadiologyTestType 
                {
                    Name = "Fluoroscopy (التنظير الفلوري)",
                    Description = "تصوير متحرك في الوقت الحقيقي للأعضاء الداخلية"
                },
                new RadiologyTestType 
                {
                    Name = "Bone Densitometry (قياس كثافة العظام)",
                    Description = "فحص خاص لقياس كثافة العظام والكشف عن هشاشة العظام"
                },
                new RadiologyTestType 
                {
                    Name = "Angiography (تصوير الأوعية الدموية)",
                    Description = "تصوير خاص للأوعية الدموية باستخدام الصبغة"
                },
                new RadiologyTestType 
                {
                    Name = "Nuclear Medicine Scan (التصوير النووي)",
                    Description = "تصوير باستخدام المواد المشعة للكشف عن وظائف الأعضاء وبعض الأمراض"
                }
            };

            await context.AddRangeAsync(radiologyTests);
            await context.SaveChangesAsync();
        }
    }
}
