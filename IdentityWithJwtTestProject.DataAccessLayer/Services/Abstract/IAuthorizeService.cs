﻿using IdentityWithJwtTestProject.DtoLayer.Dtos.AuthorizationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithJwtTestProject.DataAccessLayer.Services.Abstract
{
    public interface IAuthorizeService
    {
        public Task AssignRoleEndpointAsync(AssignRoleEndpointDto assignRoleEndpointDto);
    }
}
