using HSS.DataAccess.Contexts;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Repositories
{
    public class Helper
    {
        public KeyValuePair<TKey, TValue> GetRandomElement<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
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
        public static List<string> maleNames = new List<string>
            {
                "أحمد", "علي", "عمر", "خالد", "حسن", "فيصل", "يوسف", "محمد", "سامي", "زيد",
                "عبدالله", "طارق", "ناصر", "مصطفى", "إبراهيم", "عادل", "هادي", "سيف", "بلال", "سليم",
                "وليد", "عمرو", "فريد", "رامي", "كريم", "بشير", "جمال", "زياد", "حاتم", "يحيى",
                "أيمن", "ماجد", "حمزة", "أنور", "عمران", "محمود", "سليمان", "عدنان", "شادي", "بسام",
                "فهد", "سمير", "عثمان", "راشد", "عيسى", "تامر", "رياض", "صفوان", "منير", "حبيب",
                "ياسين", "مازن", "مراد", "موسى", "رفيق", "غسان", "إيهاب", "توفيق", "جابر", "أشرف",
                "إحسان", "منير", "زهير", "وليد", "فراس", "حسين", "مالك", "شاكر", "سعد", "إسماعيل",
                "زاهر", "فادي", "نزار", "هشام", "إلياس", "ليث", "رامي", "كمال", "نعيم", "مرتضى",
                "سامي", "عمر", "نور", "براء", "راكان", "أمين", "مجاهد", "عز", "طلال", "ياسر",
                "عبدالرحمن", "قيس", "حمد", "أنس", "رائد", "خير", "هادي", "نبيل", "زهير", "سميح"
            };
        public static List<string> femaleNames = new List<string>
            {
                "عائشة", "فاطمة", "ليلى", "نورة", "أميرة", "مريم", "هدى", "رانيا", "سلمى", "ياسمين",
                "نادية", "سميرة", "سحر", "دينا", "هالة", "ريم", "زينب", "نور", "لبنى", "سناء",
                "فرح", "بشرى", "لينا", "شادية", "أماني", "سعاد", "ندى", "منال", "رقية", "رباب",
                "خديجة", "رنا", "مها", "دلال", "غادة", "بسمة", "جميلة", "أسماء", "نجوى", "ليلى",
                "ابتسام", "نسرين", "هند", "وفاء", "هنادي", "روان", "مروة", "سمية", "منى", "حنان",
                "ريما", "فدوى", "عبير", "تالا", "شيرين", "فادية", "صفاء", "فوزية", "لمياء", "نورهان",
                "تهاني", "عفاف", "رشا", "سوسن", "أحلام", "نجلاء", "أروى", "ميساء", "سهير", "شيماء",
                "زهراء", "منى", "هبة", "ملك", "وردة", "رهف", "هديل", "هيا", "علاء", "هدير",
                "يارا", "ريما", "دعاء", "بثينة", "زين", "جيهان", "إيمان", "روعة", "ثريا", "أماني",
                "لميس", "أمل", "شيماء", "رحاب", "ميسون", "سهام", "فريال", "شمس", "نوران", "سهى"
            };
        public Dictionary<string, string> governorates = new Dictionary<string, string>
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


        public static DateTime GetRandomDate()
        {
            Random random = new Random();
            int startYear = 1960;
            int endYear = 2005;

            // Generate a random year between 1960 and 2005
            int year = random.Next(startYear, endYear + 1);

            // Generate a random month (1 to 12)
            int month = random.Next(1, 13);

            // Generate a random day based on the month and year
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = random.Next(1, daysInMonth + 1);

            return new DateTime(year, month, day);
        }

        public static DateTime GetRandomDate(DateTime? date = null)
        {
            Random random = new Random();
            int startYear = 1960;
            int endYear = 2005;

            // Generate a random year between 1960 and 2005
            int year = random.Next(startYear, endYear + 1);

            // Generate a random month (1 to 12)
            int month = random.Next(1, 13);

            // Generate a random day based on the month and year
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = random.Next(1, daysInMonth + 1);

            return new DateTime(year, month, day);
        }

        public static string GenerateFullNameMale()
        {
            Random random = new Random();
            List<string> fullName = new List<string>();
            string previousName = null;

            for (int i = 0; i < 5; i++)
            {
                // Filter out the previous name to avoid repetition
                var availableNames = maleNames.Where(name => name != previousName).ToList();

                // Randomly select a name from the available names
                string selectedName = availableNames[random.Next(availableNames.Count)];
                fullName.Add(selectedName);

                // Update the previous name to the current one
                previousName = selectedName;
            }

            return string.Join(" ", fullName);
        }

        public static string GenerateFullNameFemale()
        {
            Random random = new Random();
            var firstName = femaleNames[random.Next(femaleNames.Count)];
            List<string> fullName = new List<string> { firstName };
            string previousName = firstName;

            for (int i = 0; i < 4; i++)
            {
                // Filter out the previous name to avoid repetition
                var availableNames = maleNames.Where(name => name != previousName).ToList();

                // Randomly select a name from the available names
                string selectedName = availableNames[random.Next(availableNames.Count)];
                fullName.Add(selectedName);

                // Update the previous name to the current one
                previousName = selectedName;
            }

            return string.Join(" ", fullName);
        }
        public static string GenerateStreetName()
        {
            if (maleNames.Count < 2)
            {
                throw new ArgumentException("The list must contain at least 2 names.");
            }

            Random random = new Random();

            // Randomly select two different names from the list
            string firstName = maleNames[random.Next(maleNames.Count)];
            string secondName;
            do
            {
                secondName = maleNames[random.Next(maleNames.Count)];
            } while (secondName == firstName); // Ensure the names are not the same

            // Create the street name by combining the two names and adding "Street"
            string streetName = $"{firstName} {secondName} Street";

            return streetName;
        }
        public static AgeGroup GetAgeGroup(DateTime birthDate)
        {
            var today = DateTime.Today;
            var ageInDays = (today - birthDate).TotalDays;
            var ageInYears = (int)(ageInDays / 365.25); // Account for leap years

            if (ageInDays <= 28)
            {
                return AgeGroup.Neonate;
            }
            else if (ageInYears < 1)
            {
                return AgeGroup.Infant;
            }
            else if (ageInYears >= 1 && ageInYears <= 3)
            {
                return AgeGroup.Toddler;
            }
            else if (ageInYears >= 4 && ageInYears <= 12)
            {
                return AgeGroup.Child;
            }
            else if (ageInYears >= 13 && ageInYears <= 18)
            {
                return AgeGroup.Adolescent;
            }
            else if (ageInYears >= 19 && ageInYears <= 64)
            {
                return AgeGroup.Adult;
            }
            else
            {
                return AgeGroup.Senior;
            }
        }
        public static Gender GenerateRandomGender()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101); // Generates a number between 1 and 100

            // 1-40 for Female, 41-100 for Male
            return randomNumber <= 40 ? Gender.Female : Gender.Male;
        }




        public static string GenerateNationalID(bool isMale, bool test = false)
        {
            #region Random Date
            Random random = new Random();
            int startYear = 1960;
            int endYear = 2005;

            // Generate a random year between 1960 and 2005
            int year = random.Next(startYear, endYear + 1);

            // Generate a random month (1 to 12)
            int month = random.Next(1, 13);

            // Generate a random day based on the month and year
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = random.Next(1, daysInMonth + 1);

            DateTime birthDate = new DateTime(year, month, day);
            #endregion

            #region GovernmentCode
            Dictionary<string, string> governorates = new Dictionary<string, string>
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

            int randomIndex = random.Next(governorates.Count);
            var governorateCode = "";
            // Get the element at the random index
            foreach (var kvp in governorates)
            {
                if (randomIndex == 0)
                {
                    governorateCode = kvp.Value;
                }
                randomIndex--;
            } 
            #endregion

            // Step 1: Determine the century code
            int centuryCode = birthDate.Year >= 2000 ? 3 : 2;

            // Step 2: Extract year, month, and day of birth
            string yearOfBirth = birthDate.Year.ToString().Substring(2, 2); // Last 2 digits of the year
            string monthOfBirth = birthDate.Month.ToString("D2"); // Month as 2 digits
            string dayOfBirth = birthDate.Day.ToString("D2"); // Day as 2 digits

            // Step 3: Validate governorate code
            if (!IsValidGovernorateCode(governorateCode))
            {
                throw new ArgumentException("Invalid governorate code.");
            }

            // Step 4: Generate a serial number
            int serialNumber = random.Next(1, 10_000); // Random number between 1 and 999
            string serialNumberString = serialNumber.ToString("D3");

            // Adjust gender based on serial number's last digit
            if (isMale && serialNumber % 2 == 0) serialNumber++; // Make it odd for male
            if (!isMale && serialNumber % 2 != 0) serialNumber--; // Make it even for female
            serialNumberString = serialNumber.ToString("D3");

            // Step 5: Concatenate the parts
            string partialID = $"{centuryCode}{yearOfBirth}{monthOfBirth}{dayOfBirth}{governorateCode}{serialNumberString}";

            // Step 6: Calculate the checksum
            int checksum = CalculateChecksum(partialID);

            // Combine everything into the full ID
            return $"{partialID}{checksum}";
        }

        //public static string GenerateNationalID(bool isMale)
        //{
        //    DateTime birthDate = GetRandomDate();
        //    string governorateCode = GetRandomElement(Helper.governorates).Value;

        //    // Step 1: Determine the century code
        //    int centuryCode = birthDate.Year >= 2000 ? 3 : 2;

        //    // Step 2: Extract year, month, and day of birth
        //    string yearOfBirth = birthDate.Year.ToString().Substring(2, 2); // Last 2 digits of the year
        //    string monthOfBirth = birthDate.Month.ToString("D2"); // Month as 2 digits
        //    string dayOfBirth = birthDate.Day.ToString("D2"); // Day as 2 digits

        //    // Step 3: Validate governorate code
        //    if (!IsValidGovernorateCode(governorateCode))
        //    {
        //        throw new ArgumentException("Invalid governorate code.");
        //    }

        //    // Step 4: Generate a serial number
        //    Random random = new Random();
        //    int serialNumber = random.Next(1, 10_000); // Random number between 1 and 999
        //    string serialNumberString = serialNumber.ToString("D3");

        //    // Adjust gender based on serial number's last digit
        //    if (isMale && serialNumber % 2 == 0) serialNumber++; // Make it odd for male
        //    if (!isMale && serialNumber % 2 != 0) serialNumber--; // Make it even for female
        //    serialNumberString = serialNumber.ToString("D3");

        //    // Step 5: Concatenate the parts
        //    string partialID = $"{centuryCode}{yearOfBirth}{monthOfBirth}{dayOfBirth}{governorateCode}{serialNumberString}";

        //    // Step 6: Calculate the checksum
        //    int checksum = CalculateChecksum(partialID);

        //    // Combine everything into the full ID
        //    return $"{partialID}{checksum}";
        //}

        public static string GenerateNationalID(DateTime birthDate, string governorateCode, bool isMale )
        {

            // Step 1: Determine the century code
            int centuryCode = birthDate.Year >= 2000 ? 3 : 2;

            // Step 2: Extract year, month, and day of birth
            string yearOfBirth = birthDate.Year.ToString().Substring(2, 2); // Last 2 digits of the year
            string monthOfBirth = birthDate.Month.ToString("D2"); // Month as 2 digits
            string dayOfBirth = birthDate.Day.ToString("D2"); // Day as 2 digits

            // Step 3: Validate governorate code
            if (!IsValidGovernorateCode(governorateCode))
            {
                throw new ArgumentException("Invalid governorate code.");
            }

            // Step 4: Generate a serial number
            Random random = new Random();
            int serialNumber = random.Next(1, 10_000); // Random number between 1 and 999
            string serialNumberString = serialNumber.ToString("D3");

            // Adjust gender based on serial number's last digit
            if (isMale && serialNumber % 2 == 0) serialNumber++; // Make it odd for male
            if (!isMale && serialNumber % 2 != 0) serialNumber--; // Make it even for female
            serialNumberString = serialNumber.ToString("D3");

            // Step 5: Concatenate the parts
            string partialID = $"{centuryCode}{yearOfBirth}{monthOfBirth}{dayOfBirth}{governorateCode}{serialNumberString}";

            // Step 6: Calculate the checksum
            int checksum = CalculateChecksum(partialID);

            // Combine everything into the full ID
            return $"{partialID}{checksum}";
        }

        private static bool IsValidGovernorateCode(string code)
        {
            // List of valid governorate codes
            List<string> validCodes = new List<string>
        {
            "01", "02", "03", "04", "11", "12", "13", "14", "15", "16", "17",
            "18", "19", "21", "22", "23", "24", "25", "26", "27", "28", "29","30","31","32","33", "34","35","88"
        };
            return validCodes.Contains(code);
        }

        private static int CalculateChecksum(string partialID)
        {
            // Luhn algorithm for checksum calculation
            int sum = 0;
            bool doubleDigit = true;

            for (int i = partialID.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(partialID[i].ToString());

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            return (10 - (sum % 10)) % 10;
        }

        
   
       
    }
}
