﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithJwtTestProject.DtoLayer.Dtos.UserDtos
{
    public class AssignRoleToUserDto
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
