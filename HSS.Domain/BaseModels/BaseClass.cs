﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.BaseModels
{
    public abstract class BaseClass<T> where T: struct
    {
        public T Id { get; set; }
    }
}
