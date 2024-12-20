using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class MedicineForDoctorDto
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public float Cost { get; set;}
        public string Description { get; set;}
    }
}
