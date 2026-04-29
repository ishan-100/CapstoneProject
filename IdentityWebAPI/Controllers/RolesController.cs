using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebAPI.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required");

            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role already exists");

            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return Ok("Role created successfully");
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(_roleManager.Roles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound("Role not found");

            await _roleManager.DeleteAsync(role);
            return Ok("Role deleted successfully");
        }
    }
}
