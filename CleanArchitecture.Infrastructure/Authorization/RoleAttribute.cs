using CleanArchitecture.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Authorization
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        private readonly IUserRoleRepository _userRoleRepositroy;

        public RoleAttribute(string role, IUserRoleRepository userRoleRepositroy)
        {
            _role = role;
            _userRoleRepositroy = userRoleRepositroy;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return; // 401 hatası döner
            }

            var userHasRole = _userRoleRepositroy.GetWhere(p => p.UserId == userIdClaim.Value)
                .Include(p => p.Role)
                .Any(p => p.Role.Name == _role);

            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return; // 401 hatası döner
            }
        }
    }
}
