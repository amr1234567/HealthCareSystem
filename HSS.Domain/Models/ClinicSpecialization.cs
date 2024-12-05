using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Models
{
    public class ClinicSpecialization : BaseClass<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [AllowNull, MaxLength(300)]
        public string Description { get; set; }
    }
}
