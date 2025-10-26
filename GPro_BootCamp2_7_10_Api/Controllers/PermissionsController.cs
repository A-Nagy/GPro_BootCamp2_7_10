using GPro_BootCamp2_7_10_Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PermissionsController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _role;
        public PermissionsController(RoleManager<ApplicationRole> role)
        {
            _role = role;
        }

        [HttpGet("roles")]
        public IActionResult GetRoles() => Ok(_role.Roles.Select(r => new { r.Id, r.Name}).ToList());
        public record AssignRequest(string[] permission);

        [HttpPost("roles/{roleName}/assign")]
        public async Task<IActionResult> Assign(string rolename, AssignRequest Arq) 
        {
            var role = await  _role.FindByNameAsync(rolename);
            var existing = await _role.GetClaimsAsync(role);

            foreach (var p in Arq.permission.Distinct()) ;
            if (!existing.Any(c => c.Type == "Permissions" && c.Value == p))
                await _role.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", p));
           
            return NoContent();




        }

     





    }
}
