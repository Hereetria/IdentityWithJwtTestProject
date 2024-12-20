﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithJwtTestProject.EntityLayer.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<AppRoleEndpoint> AppRoleEndpoints { get; set; } = new List<AppRoleEndpoint>();
    }
}
