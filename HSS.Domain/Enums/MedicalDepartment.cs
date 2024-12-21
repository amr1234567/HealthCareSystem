using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Enums
{
    public enum MedicalDepartment
    {
        [EnumMember(Value = "التخدير")]
        Anesthesiology,

        [EnumMember(Value = "أمراض القلب")]
        Cardiology,

        [EnumMember(Value = "الأمراض الجلدية")]
        Dermatology,

        [EnumMember(Value = "الطب الطارئ")]
        EmergencyMedicine,

        [EnumMember(Value = "الغدد الصماء")]
        Endocrinology,

        [EnumMember(Value = "الطب العائلي")]
        FamilyMedicine,

        [EnumMember(Value = "أمراض الجهاز الهضمي")]
        Gastroenterology,

        [EnumMember(Value = "الجراحة العامة")]
        GeneralSurgery,

        [EnumMember(Value = "طب المسنين")]
        Geriatrics,

        [EnumMember(Value = "أمراض الدم")]
        Hematology,

        [EnumMember(Value = "الأمراض المعدية")]
        InfectiousDiseases,

        [EnumMember(Value = "الطب الباطني")]
        InternalMedicine,

        [EnumMember(Value = "الطب الوراثي")]
        MedicalGenetics,

        [EnumMember(Value = "أمراض الكلى")]
        Nephrology,

        [EnumMember(Value = "طب الأعصاب")]
        Neurology,

        [EnumMember(Value = "النساء والولادة")]
        ObstetricsAndGynecology,

        [EnumMember(Value = "الأورام")]
        Oncology,

        [EnumMember(Value = "طب العيون")]
        Ophthalmology,

        [EnumMember(Value = "جراحة العظام")]
        Orthopedics,

        [EnumMember(Value = "الأنف والأذن والحنجرة")]
        Otorhinolaryngology, // ENT

        [EnumMember(Value = "طب الأطفال")]
        Pediatrics,

        [EnumMember(Value = "الطب الطبيعي وإعادة التأهيل")]
        PhysicalMedicineAndRehabilitation,

        [EnumMember(Value = "جراحة التجميل")]
        PlasticSurgery,

        [EnumMember(Value = "الطب النفسي")]
        Psychiatry,

        [EnumMember(Value = "أمراض الرئة")]
        Pulmonology,

        [EnumMember(Value = "الأشعة")]
        Radiology,

        [EnumMember(Value = "الروماتيزم")]
        Rheumatology,

        [EnumMember(Value = "جراحة المسالك البولية")]
        Urology
    }
    public class MedicineDepartment
    {
        public static string GetArabicName(MedicalDepartment department)
        {
            var type = typeof(MedicalDepartment);
            var member = type.GetMember(department.ToString())[0];
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(member, typeof(EnumMemberAttribute));
            return attribute != null ? attribute.Value : department.ToString();
        }
    }
    
}
