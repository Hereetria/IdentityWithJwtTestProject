﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithJwtTestProject.DtoLayer.Dtos.UserDtos
{
    public class LoginUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}