using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contagious = table.Column<bool>(type: "bit", nullable: false),
                    Discovery_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AffectedPopulation = table.Column<int>(type: "int", nullable: false),
                    DiseaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CureRate = table.Column<float>(type: "real", nullable: false),
                    FatalityRate = table.Column<float>(type: "real", nullable: false),
                    TreatmentDurationInDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChronic = table.Column<bool>(type: "bit", nullable: false),
                    HasVaccine = table.Column<bool>(type: "bit", nullable: false),
                    CommonAgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommonGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskFactors = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    GeographicSpread = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastOutbreakDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResearchStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreventionMeasures = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabCenterTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCenterTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EffectiveSubstanceId = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StorageConditions = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PrescriptionRequired = table.Column<bool>(type: "bit", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientMediacalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiabetesBinary = table.Column<int>(type: "int", nullable: false),
                    HighBp = table.Column<int>(type: "int", nullable: false),
                    HighChol = table.Column<int>(type: "int", nullable: false),
                    CholCheck = table.Column<int>(type: "int", nullable: false),
                    Bmi = table.Column<float>(type: "real", nullable: false),
                    Smoker = table.Column<int>(type: "int", nullable: false),
                    Stroke = table.Column<int>(type: "int", nullable: false),
                    HeartDiseaseOrAttack = table.Column<int>(type: "int", nullable: false),
                    PhysActivity = table.Column<int>(type: "int", nullable: false),
                    Fruits = table.Column<int>(type: "int", nullable: false),
                    Veggies = table.Column<int>(type: "int", nullable: false),
                    HvyAlcoholConsump = table.Column<int>(type: "int", nullable: false),
                    AnyHealthcare = table.Column<int>(type: "int", nullable: false),
                    NoDocbcCost = table.Column<int>(type: "int", nullable: false),
                    GenHlth = table.Column<int>(type: "int", nullable: false),
                    MentHlth = table.Column<int>(type: "int", nullable: false),
                    PhysHlth = table.Column<int>(type: "int", nullable: false),
                    DiffWalk = table.Column<int>(type: "int", nullable: false),
                    LastVisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMediacalDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "radiologyTestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_radiologyTestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveSubstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChemicalFormula = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiscoveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StabilityConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrimaryUsage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AlternativeNames = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveSubstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveSubstances_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    OnsetPattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChronic = table.Column<bool>(type: "bit", nullable: false),
                    TreatmentRequired = table.Column<bool>(type: "bit", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SideEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AgeRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commonality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Reversibility = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SideEffects_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EffectiveSubstanceDisease",
                columns: table => new
                {
                    EffectiveSubstanceId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveSubstanceDisease", x => new { x.DiseaseId, x.EffectiveSubstanceId });
                    table.ForeignKey(
                        name: "FK_EffectiveSubstanceDisease_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EffectiveSubstanceDisease_EffectiveSubstances_EffectiveSubstanceId",
                        column: x => x.EffectiveSubstanceId,
                        principalTable: "EffectiveSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveSubstanceMedicine",
                columns: table => new
                {
                    EffectiveSubstanceId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MedicineId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveSubstanceMedicine", x => new { x.MedicineId, x.EffectiveSubstanceId });
                    table.ForeignKey(
                        name: "FK_EffectiveSubstanceMedicine_EffectiveSubstances_EffectiveSubstanceId",
                        column: x => x.EffectiveSubstanceId,
                        principalTable: "EffectiveSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EffectiveSubstanceMedicine_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EffectiveSubstanceMedicine_Medicines_MedicineId1",
                        column: x => x.MedicineId1,
                        principalTable: "Medicines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SymptomDiseases",
                columns: table => new
                {
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomDiseases", x => new { x.SymptomId, x.DiseaseId });
                    table.ForeignKey(
                        name: "FK_SymptomDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SymptomDiseases_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SideEffectEffectiveSubstances",
                columns: table => new
                {
                    SideEffectId = table.Column<int>(type: "int", nullable: false),
                    EffectiveSubstanceId = table.Column<int>(type: "int", nullable: false),
                    EffectiveSubstanceId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffectEffectiveSubstances", x => new { x.SideEffectId, x.EffectiveSubstanceId });
                    table.ForeignKey(
                        name: "FK_SideEffectEffectiveSubstances_EffectiveSubstances_EffectiveSubstanceId",
                        column: x => x.EffectiveSubstanceId,
                        principalTable: "EffectiveSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SideEffectEffectiveSubstances_EffectiveSubstances_EffectiveSubstanceId1",
                        column: x => x.EffectiveSubstanceId1,
                        principalTable: "EffectiveSubstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SideEffectEffectiveSubstances_SideEffects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "SideEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SideEffectMedicines",
                columns: table => new
                {
                    SideEffectId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffectMedicines", x => new { x.MedicineId, x.SideEffectId });
                    table.ForeignKey(
                        name: "FK_SideEffectMedicines_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SideEffectMedicines_SideEffects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "SideEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LabAppointmentsNumberDone = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    RadiologyAppointmentsNumberDone = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    TreatmentId = table.Column<int>(type: "int", nullable: true),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: true),
                    FollowUpExpectedPeriod = table.Column<TimeSpan>(type: "time", nullable: true),
                    RadiologyAppointmentNeeded = table.Column<bool>(type: "bit", nullable: true),
                    LabAppointmentNeeded = table.Column<bool>(type: "bit", nullable: true),
                    ClinicAppointmentIdRelatedTo = table.Column<int>(type: "int", nullable: true),
                    LabCenterId = table.Column<int>(type: "int", nullable: true),
                    TestTypeId = table.Column<int>(type: "int", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LabTesterId = table.Column<int>(type: "int", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LabAppointment_ClinicAppointmentIdRelatedTo = table.Column<int>(type: "int", nullable: true),
                    RadiologyCenterId = table.Column<int>(type: "int", nullable: true),
                    RadiologyAppointment_TestTypeId = table.Column<int>(type: "int", nullable: true),
                    RadiologyTesterId = table.Column<int>(type: "int", nullable: true),
                    RadiologyAppointment_TestResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RadiologyAppointment_ClinicAppointmentIdRelatedTo = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Appointments_ClinicAppointmentIdRelatedTo",
                        column: x => x.ClinicAppointmentIdRelatedTo,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Appointments_LabAppointment_ClinicAppointmentIdRelatedTo",
                        column: x => x.LabAppointment_ClinicAppointmentIdRelatedTo,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Appointments_RadiologyAppointment_ClinicAppointmentIdRelatedTo",
                        column: x => x.RadiologyAppointment_ClinicAppointmentIdRelatedTo,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_LabCenterTests_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "LabCenterTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_radiologyTestTypes_RadiologyAppointment_TestTypeId",
                        column: x => x.RadiologyAppointment_TestTypeId,
                        principalTable: "radiologyTestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineUnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    DosageFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispenseStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispensedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispensedAmount = table.Column<int>(type: "int", nullable: false),
                    ClinicAppointmentId = table.Column<int>(type: "int", nullable: false),
                    TimesOfDispensed = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecord_Appointments_ClinicAppointmentId",
                        column: x => x.ClinicAppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecord_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecord_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    SpecializationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicalDepartment = table.Column<int>(type: "int", nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    FinishAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    AppointmentDurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clinicSpecializationHospitals",
                columns: table => new
                {
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    ClinicSpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinicSpecializationHospitals", x => new { x.HospitalId, x.ClinicSpecializationId });
                });

            migrationBuilder.CreateTable(
                name: "ClinicSpecializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSpecializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AmbulanceAvailability = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HospitalAdminId = table.Column<int>(type: "int", nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    BedAvailability = table.Column<int>(type: "int", nullable: false),
                    NumberOfDoctors = table.Column<int>(type: "int", nullable: false),
                    NumberOfNurses = table.Column<int>(type: "int", nullable: false),
                    DepartmentsCount = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    HospitalAdminId2 = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    AppointmentDuration = table.Column<int>(type: "int", nullable: false),
                    LabCenterId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabCenters_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabCenters_LabCenters_LabCenterId",
                        column: x => x.LabCenterId,
                        principalTable: "LabCenters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RadiologyCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    AppointmentDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadiologyCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadiologyCenters_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptions_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabCenterLabTests",
                columns: table => new
                {
                    LabCenterId = table.Column<int>(type: "int", nullable: false),
                    LabCenterTestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCenterLabTests", x => new { x.LabCenterTestId, x.LabCenterId });
                    table.ForeignKey(
                        name: "FK_LabCenterLabTests_LabCenterTests_LabCenterTestId",
                        column: x => x.LabCenterTestId,
                        principalTable: "LabCenterTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabCenterLabTests_LabCenters_LabCenterId",
                        column: x => x.LabCenterId,
                        principalTable: "LabCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationOfRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    AdministrationId = table.Column<int>(type: "int", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    HospitalAdmin_HospitalId = table.Column<int>(type: "int", nullable: true),
                    LabManager_HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LabManager_WorkingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    LabManager_ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    Certifications = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LabCenterId = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncomeCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMediacalDetailsId = table.Column<int>(type: "int", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_HouseName = table.Column<int>(type: "int", nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_StreetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Pharmacist_WorkingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    RadiologyManager_HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RadiologyManager_ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    Certification = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RadiologyManager_WorkingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    RadiologyCenterId = table.Column<int>(type: "int", nullable: true),
                    Receptionist_WorkingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Administrations_AdministrationId",
                        column: x => x.AdministrationId,
                        principalTable: "Administrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Hospitals_HospitalAdmin_HospitalId",
                        column: x => x.HospitalAdmin_HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_LabCenters_LabCenterId",
                        column: x => x.LabCenterId,
                        principalTable: "LabCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_PatientMediacalDetails_PatientMediacalDetailsId",
                        column: x => x.PatientMediacalDetailsId,
                        principalTable: "PatientMediacalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_RadiologyCenters_RadiologyCenterId",
                        column: x => x.RadiologyCenterId,
                        principalTable: "RadiologyCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiagnosisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TreatmentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreatmentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_IdentityUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityUserintId = table.Column<int>(name: "IdentityUser<int>Id", type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_IdentityUsers_IdentityUser<int>Id",
                        column: x => x.IdentityUserintId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsLogin = table.Column<bool>(type: "bit", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicAppointmentIdRelatedTo",
                table: "Appointments",
                column: "ClinicAppointmentIdRelatedTo",
                unique: true,
                filter: "[ClinicAppointmentIdRelatedTo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicId",
                table: "Appointments",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HospitalId",
                table: "Appointments",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LabAppointment_ClinicAppointmentIdRelatedTo",
                table: "Appointments",
                column: "LabAppointment_ClinicAppointmentIdRelatedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LabCenterId",
                table: "Appointments",
                column: "LabCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LabTesterId",
                table: "Appointments",
                column: "LabTesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RadiologyAppointment_ClinicAppointmentIdRelatedTo",
                table: "Appointments",
                column: "RadiologyAppointment_ClinicAppointmentIdRelatedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RadiologyAppointment_TestTypeId",
                table: "Appointments",
                column: "RadiologyAppointment_TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RadiologyCenterId",
                table: "Appointments",
                column: "RadiologyCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RadiologyTesterId",
                table: "Appointments",
                column: "RadiologyTesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TestTypeId",
                table: "Appointments",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_HospitalId",
                table: "Clinics",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_SpecializationId",
                table: "Clinics",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_clinicSpecializationHospitals_ClinicSpecializationId",
                table: "clinicSpecializationHospitals",
                column: "ClinicSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicSpecializations_HospitalId",
                table: "ClinicSpecializations",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveSubstanceDisease_EffectiveSubstanceId",
                table: "EffectiveSubstanceDisease",
                column: "EffectiveSubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveSubstanceMedicine_EffectiveSubstanceId",
                table: "EffectiveSubstanceMedicine",
                column: "EffectiveSubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveSubstanceMedicine_MedicineId1",
                table: "EffectiveSubstanceMedicine",
                column: "MedicineId1");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveSubstances_DiseaseId",
                table: "EffectiveSubstances",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyDepartments_HospitalId",
                table: "EmergencyDepartments",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_HospitalAdminId",
                table: "Hospitals",
                column: "HospitalAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_AdministrationId",
                table: "IdentityUsers",
                column: "AdministrationId",
                unique: true,
                filter: "[AdministrationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_ClinicId",
                table: "IdentityUsers",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_HospitalAdmin_HospitalId",
                table: "IdentityUsers",
                column: "HospitalAdmin_HospitalId",
                unique: true,
                filter: "[HospitalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_HospitalId",
                table: "IdentityUsers",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_LabCenterId",
                table: "IdentityUsers",
                column: "LabCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_PatientMediacalDetailsId",
                table: "IdentityUsers",
                column: "PatientMediacalDetailsId",
                unique: true,
                filter: "[PatientMediacalDetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_PharmacyId",
                table: "IdentityUsers",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_RadiologyCenterId",
                table: "IdentityUsers",
                column: "RadiologyCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_ReceptionId",
                table: "IdentityUsers",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabCenterLabTests_LabCenterId",
                table: "LabCenterLabTests",
                column: "LabCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_LabCenters_HospitalId",
                table: "LabCenters",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_LabCenters_LabCenterId",
                table: "LabCenters",
                column: "LabCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_HospitalId",
                table: "Pharmacies",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecord_ClinicAppointmentId",
                table: "PrescriptionRecord",
                column: "ClinicAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecord_MedicineId",
                table: "PrescriptionRecord",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecord_TreatmentId",
                table: "PrescriptionRecord",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RadiologyCenters_HospitalId",
                table: "RadiologyCenters",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_HospitalId",
                table: "Receptions",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IdentityUser<int>Id",
                table: "Roles",
                column: "IdentityUser<int>Id");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffectEffectiveSubstances_EffectiveSubstanceId",
                table: "SideEffectEffectiveSubstances",
                column: "EffectiveSubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffectEffectiveSubstances_EffectiveSubstanceId1",
                table: "SideEffectEffectiveSubstances",
                column: "EffectiveSubstanceId1");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffectMedicines_SideEffectId",
                table: "SideEffectMedicines",
                column: "SideEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffects_MedicineId",
                table: "SideEffects",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomDiseases_DiseaseId",
                table: "SymptomDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DiseaseId",
                table: "Symptoms",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Hospitals_HospitalId",
                table: "Appointments",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_IdentityUsers_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_IdentityUsers_LabTesterId",
                table: "Appointments",
                column: "LabTesterId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_IdentityUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_IdentityUsers_RadiologyTesterId",
                table: "Appointments",
                column: "RadiologyTesterId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_LabCenters_LabCenterId",
                table: "Appointments",
                column: "LabCenterId",
                principalTable: "LabCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_RadiologyCenters_RadiologyCenterId",
                table: "Appointments",
                column: "RadiologyCenterId",
                principalTable: "RadiologyCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_ClinicSpecializations_SpecializationId",
                table: "Clinics",
                column: "SpecializationId",
                principalTable: "ClinicSpecializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Hospitals_HospitalId",
                table: "Clinics",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clinicSpecializationHospitals_ClinicSpecializations_ClinicSpecializationId",
                table: "clinicSpecializationHospitals",
                column: "ClinicSpecializationId",
                principalTable: "ClinicSpecializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clinicSpecializationHospitals_Hospitals_HospitalId",
                table: "clinicSpecializationHospitals",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicSpecializations_Hospitals_HospitalId",
                table: "ClinicSpecializations",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyDepartments_Hospitals_HospitalId",
                table: "EmergencyDepartments",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_IdentityUsers_HospitalAdminId",
                table: "Hospitals",
                column: "HospitalAdminId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_Clinics_ClinicId",
                table: "IdentityUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_Hospitals_HospitalAdmin_HospitalId",
                table: "IdentityUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_Hospitals_HospitalId",
                table: "IdentityUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LabCenters_Hospitals_HospitalId",
                table: "LabCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Hospitals_HospitalId",
                table: "Pharmacies");

            migrationBuilder.DropForeignKey(
                name: "FK_RadiologyCenters_Hospitals_HospitalId",
                table: "RadiologyCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_Hospitals_HospitalId",
                table: "Receptions");

            migrationBuilder.DropTable(
                name: "clinicSpecializationHospitals");

            migrationBuilder.DropTable(
                name: "EffectiveSubstanceDisease");

            migrationBuilder.DropTable(
                name: "EffectiveSubstanceMedicine");

            migrationBuilder.DropTable(
                name: "EmergencyDepartments");

            migrationBuilder.DropTable(
                name: "LabCenterLabTests");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "PrescriptionRecord");

            migrationBuilder.DropTable(
                name: "SideEffectEffectiveSubstances");

            migrationBuilder.DropTable(
                name: "SideEffectMedicines");

            migrationBuilder.DropTable(
                name: "SymptomDiseases");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "EffectiveSubstances");

            migrationBuilder.DropTable(
                name: "SideEffects");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "LabCenterTests");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "radiologyTestTypes");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "ClinicSpecializations");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "Administrations");

            migrationBuilder.DropTable(
                name: "LabCenters");

            migrationBuilder.DropTable(
                name: "PatientMediacalDetails");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "RadiologyCenters");

            migrationBuilder.DropTable(
                name: "Receptions");
        }
    }
}
