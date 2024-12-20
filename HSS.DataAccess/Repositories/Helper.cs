using HSS.DataAccess.Contexts;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Repositories
{
    public class Helper(ApplicationDbContext context)
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
        private List<string> maleNames = new List<string>
            {
                "Ahmed", "Ali", "Omar", "Khalid", "Hassan", "Faisal", "Youssef", "Mohammed", "Sami", "Zaid",
                "Abdullah", "Tariq", "Nasser", "Mustafa", "Ibrahim", "Adel", "Hadi", "Saif", "Bilal", "Salim",
                "Walid", "Amr", "Farid", "Rami", "Kareem", "Bashir", "Jamal", "Ziyad", "Hatem", "Yahya",
                "Ayman", "Majed", "Hamza", "Anwar", "Imran", "Mahmoud", "Suleiman", "Adnan", "Shadi", "Bassam",
                "Fahd", "Samir", "Othman", "Rashid", "Issa", "Tamer", "Riyad", "Safwan", "Mounir", "Habib",
                "Yassin", "Mazen", "Murad", "Musa", "Rafiq", "Ghassan", "Ehab", "Tawfiq", "Jabir", "Ashraf",
                "Ihsan", "Munir", "Zuhair", "Waleed", "Firas", "Hussain", "Malik", "Shaker", "Saad", "Ismail",
                "Zaher", "Fadi", "Nizar", "Hisham", "Elias", "Laith", "Rami", "Kamal", "Naeem", "Murtada",
                "Sami", "Omar", "Nour", "Baraa", "Rakan", "Ameen", "Mujahid", "Izz", "Talal", "Yaser",
                "Abdulrahman", "Qais", "Hamad", "Anas", "Raed", "Khayr", "Hadi", "Nabil", "Zuhair", "Samih"
            };
        private List<string> femaleNames = new List<string>
            {
                "Aisha", "Fatima", "Leila", "Noura", "Amira", "Mariam", "Huda", "Rania", "Salma", "Yasmin",
                "Nadia", "Samira", "Sahar", "Dina", "Hala", "Reem", "Zainab", "Noor", "Lubna", "Sana",
                "Farah", "Bushra", "Lina", "Shadia", "Amani", "Souad", "Nada", "Manal", "Ruqayya", "Rabab",
                "Khadija", "Rana", "Maha", "Dalal", "Ghada", "Basma", "Jamila", "Asma", "Najwa", "Layla",
                "Ibtisam", "Nisreen", "Hind", "Wafaa", "Hanadi", "Rawan", "Marwa", "Sumaya", "Mona", "Hanan",
                "Rima", "Fadwa", "Abeer", "Tala", "Shereen", "Fadia", "Safaa", "Fawzia", "Lamya", "Nourhan",
                "Tahani", "Afaf", "Rasha", "Sawsan", "Ahlam", "Najla", "Arwa", "Maysaa", "Suhair", "Shayma",
                "Zahra", "Muna", "Heba", "Malak", "Wardah", "Rahaf", "Hadeel", "Haya", "Alaa", "Hadeer",
                "Yara", "Rima", "Duaa", "Buthaina", "Zayn", "Jihan", "Eman", "Roua", "Thuraya", "Amani",
                "Lamees", "Amal", "Shaima", "Rehab", "Maysoon", "Siham", "Ferial", "Shams", "Nouran", "Suha"
            };
        private Dictionary<string, string> governorates = new Dictionary<string, string>
            {
                { "Cairo", "01" },
                { "Alexandria", "02" },
                { "Port Said", "03" },
                { "Suez", "04" },
                { "Damietta", "11" },
                { "Dakahlia", "12" },
                { "Sharqia", "13" },
                { "Kafr El Sheikh", "14" },
                { "Gharbia", "15" },
                { "Monufia", "16" },
                { "Beheira", "17" },
                { "Ismailia", "18" },
                { "Giza", "19" },
                { "Beni Suef", "22" },
                { "Fayoum", "23" },
                { "Minya", "24" },
                { "Assiut", "25" },
                { "Sohag", "26" },
                { "Qena", "27" },
                { "Aswan", "28" },
                { "Luxor", "29" },
                { "Red Sea", "31" },
                { "New Valley", "32" },
                { "Matruh", "33" },
                { "North Sinai", "34" },
                { "South Sinai", "35" }
            };


        private DateTime GetRandomDate()
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

        private string GenerateFullNameMale()
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

        private string GenerateFullNameFemale()
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
        private string GenerateStreetName()
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
        private AgeGroup GetAgeGroup(DateTime birthDate)
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
        private Gender GenerateRandomGender()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101); // Generates a number between 1 and 100

            // 1-40 for Female, 41-100 for Male
            return randomNumber <= 40 ? Gender.Female : Gender.Male;
        }




        public static string GenerateNationalID(DateTime birthDate, string governorateCode, bool isMale)
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

        public async Task Seed()
        {
            for (int i = 0; i < 100_000; i++)
            {
                var state = GetRandomElement(governorates);
                var customDate = GetRandomDate();
                
                var gender = GenerateRandomGender();

                var random = new Random();

                var user = new Patient()
                {
                    Address = new Domain.ObjectValues.PatientAddress()
                    {
                        StreetName = GenerateStreetName(),
                        City = state.Key,
                        State = "Egypt"
                    },
                    AgeCategory = GetAgeGroup(customDate),
                    DateOfBirth = customDate,
                    CreatedAt = DateTime.UtcNow,
                    Name = gender == Gender.Female ?
                    GenerateFullNameFemale()
                    : GenerateFullNameMale(),
                    Sex = gender,
                    NationalId = GenerateNationalID(customDate, state.Value, gender == Gender.Male),
                };
                await context.Patients.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
