﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Models
{
    public class User:IdentityUser<int>
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}
