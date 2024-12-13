using HSS.DataAccess.Contexts;
using HSS.Domain.BaseModels;
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

        private Dictionary<string, int> governorates = new Dictionary<string, int>
            {
                { "Cairo", 1 },
                { "Alexandria", 2 },
                { "Port Said", 3 },
                { "Suez", 4 },
                { "Damietta", 11 },
                { "Dakahlia", 12 },
                { "Sharqia", 13 },
                { "Kafr El Sheikh", 14 },
                { "Gharbia", 15 },
                { "Monufia", 16 },
                { "Beheira", 17 },
                { "Ismailia", 18 },
                { "Giza", 19 },
                { "Outside Republic", 21 },
                { "Beni Suef", 22 },
                { "Fayoum", 23 },
                { "Minya", 24 },
                { "Assiut", 25 },
                { "Sohag", 26 },
                { "Qena", 27 },
                { "Aswan", 28 },
                { "Luxor", 29 },
                { "Red Sea", 31 },
                { "New Valley", 32 },
                { "Matruh", 33 },
                { "North Sinai", 34 },
                { "South Sinai", 35 },
                { "Outside Republic", 88 }
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

        private string GenerateFullNameMale(List<string> namesList)
        {
            if (namesList.Count < 5)
            {
                throw new ArgumentException("The list must contain at least 5 unique names.");
            }

            Random random = new Random();
            List<string> fullName = new List<string>();
            string previousName = null;

            for (int i = 0; i < 5; i++)
            {
                // Filter out the previous name to avoid repetition
                var availableNames = namesList.Where(name => name != previousName).ToList();

                // Randomly select a name from the available names
                string selectedName = availableNames[random.Next(availableNames.Count)];
                fullName.Add(selectedName);

                // Update the previous name to the current one
                previousName = selectedName;
            }

            return string.Join(" ", fullName);
        }

        private string GenerateFullNameFemale(string firstName, List<string> namesList)
        {
            if (namesList.Count < 4)
            {
                throw new ArgumentException("The list must contain at least 4 unique names.");
            }

            Random random = new Random();
            List<string> fullName = new List<string> { firstName };
            string previousName = firstName;

            for (int i = 0; i < 4; i++)
            {
                // Filter out the previous name to avoid repetition
                var availableNames = namesList.Where(name => name != previousName).ToList();

                // Randomly select a name from the available names
                string selectedName = availableNames[random.Next(availableNames.Count)];
                fullName.Add(selectedName);

                // Update the previous name to the current one
                previousName = selectedName;
            }

            return string.Join(" ", fullName);
        }
        private string GenerateStreetName(List<string> namesList)
        {
            if (namesList.Count < 2)
            {
                throw new ArgumentException("The list must contain at least 2 names.");
            }

            Random random = new Random();

            // Randomly select two different names from the list
            string firstName = namesList[random.Next(namesList.Count)];
            string secondName;
            do
            {
                secondName = namesList[random.Next(namesList.Count)];
            } while (secondName == firstName); // Ensure the names are not the same

            // Create the street name by combining the two names and adding "Street"
            string streetName = $"{firstName} {secondName} Street";

            return streetName;
        }
        
        public async Task Seed()
        {
            for (int i = 0; i < 100_000; i++)
            {
                var state = GetRandomElement(governorates);
                var user = new Patient()
                {
                    Address = new Domain.ObjectValues.PatientAddress()
                    {
                        StreetName = GenerateStreetName(maleNames),
                        City = state.Key,
                        State = "Egypt"
                    },
                    
                };
            }
            
        }
    }
}
