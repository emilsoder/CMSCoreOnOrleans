using System;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Api.Extensions;
using CMSCore.Content.Api.Models;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IClusterClient _client;

        public AccountController(IClusterClient client) => _client = client;

        private IAccountGrain _accountGrain => _client.GetGrain<IAccountGrain>(Guid.NewGuid());

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await _accountGrain.ToList())?.Select(x => x.ViewModel()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok((await _accountGrain.Find(id))?.ViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
        {
            try
            {
                var operation = new CreateOperation<User>(AdminConst.AdminUserId,
                    new User(model.IdentityUserId ?? Guid.NewGuid().ToString())
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email
                    });

                var result = await _accountGrain.Create(operation);

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserViewModel model)
        {
            try
            {
                var userToUpdate = await _accountGrain.Find(id);
                if (userToUpdate == null) throw new Exception("User not found");

                userToUpdate.Email = model.Email;
                userToUpdate.FirstName = model.FirstName;
                userToUpdate.LastName = model.LastName;

                var operation = new UpdateOperation<User>(AdminConst.AdminUserId, id, userToUpdate);
                var result = await _accountGrain.Update(operation);
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var operation = new DeleteOperation<User>(AdminConst.AdminUserId, id);
                var result = await _accountGrain.Delete(operation);
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}