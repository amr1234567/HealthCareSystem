﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Helpers
{
    public class JwtHelper
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpireMinutes { get; set; }
        public int RefreshTokenExpireDays { get; set; }
    }

}
