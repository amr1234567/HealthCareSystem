using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Domain.Models.Aggregates;

namespace HSS.Domain.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        public List<PrescriptionRecord> PrescriptionRecords { get; set; }
    }
}
