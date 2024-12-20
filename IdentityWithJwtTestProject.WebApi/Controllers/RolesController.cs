﻿using IdentityWithJwtTestProject.DataAccessLayer.Attributes;
using IdentityWithJwtTestProject.DataAccessLayer.Services.Abstract;
using IdentityWithJwtTestProject.DtoLayer.Dtos.RoleDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJwtTestProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [AuthorizeDefinition(ControllerName = nameof(RolesController), MethodName = nameof(GetRoles))]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return roles != null 
                ? Ok(roles) 
                : NotFound("Roles not found");
        }

        [HttpGet("{id}")]
        [AuthorizeDefinition(ControllerName = nameof(RolesController), MethodName = nameof(GetRoleById))]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return role != null 
                ? Ok(role) 
                : NotFound($"Role with ID {id} not found");
        }

        [HttpPost]
        [AuthorizeDefinition(ControllerName = nameof(RolesController), MethodName = nameof(CreateRole))]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createDto)
        {
            var result = await _roleService.CreateRoleAsync(createDto);
            return result 
                ? Ok("Role created successfully") 
                : BadRequest("Failed to create role");
        }

        [HttpPut]
        [AuthorizeDefinition(ControllerName = nameof(RolesController), MethodName = nameof(UpdateRole))]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto updateDto)
        {
            var result = await _roleService.UpdateRoleAsync(updateDto);
            return result 
                ? Ok("Role updated successfully") 
                : BadRequest("Failed to update role");
        }

        [HttpDelete("{id}")]
        [AuthorizeDefinition(ControllerName = nameof(RolesController), MethodName = nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return result 
                ? Ok("Role deleted successfully") 
                : BadRequest("Failed to delete role");
        }
    }

}
