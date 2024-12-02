﻿using IdentityWithJwtTestProject.DataAccessLayer.Services.Abstract;
using IdentityWithJwtTestProject.DtoLayer.Dtos.AuthorizationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJwtTestProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IAuthorizeService _authorizationService;

        public AuthorizationController(IAuthorizeService authorizationService, IApplicationService applicationService)
        {
            _authorizationService = authorizationService;
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointDto endpointDto)
        {
            endpointDto.ActionMenus = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            await _authorizationService.AssignRoleEndpointAsync(endpointDto);
            return Ok();
        }
    }
}