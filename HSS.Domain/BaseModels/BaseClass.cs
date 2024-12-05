using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.BaseModels
{
    public abstract class BaseClass<T> where T: struct
    {
        [Key]
        [Required]
        public T Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
