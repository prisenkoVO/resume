// <copyright file="InternController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Ingoport.Services;

    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Mentor")]
    public class InternController : ControllerBase
    {
        private readonly UsersService users;

        public InternController()
        {
            this.users = new UsersService();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<string> GetUsers()
        {
            var result = this.users.GetUsers();
            return this.Ok(result);

            // this.mockInterns = new MockInterns();
            // return JsonConvert.SerializeObject(mockInterns.AllInterns);
        }

        //[HttpGet]
        //[Route("roles/{roleId}")]
        //public ActionResult<string> GetUsersByRole(string roleId)
        //{
        //    try
        //    {
        //        var userRoleId = long.TryParse(roleId, out long id) ? id : throw new Exception("Не корректный Id");

        //        var result = this.users.GetUsersByRole(userRoleId);
        //        return this.Ok(result);

        //        // return JsonConvert.SerializeObject(mockInterns.AllInterns);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(ex.Message);
        //    }
        //}
    }
}