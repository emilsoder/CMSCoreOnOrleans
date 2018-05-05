using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CMSCore.Content.Api.Attributes;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountManagerController : Controller
    {
        private readonly IClusterClient _client;

        public AccountManagerController(IClusterClient client) => _client = client;

        private IAccountManagerGrain AccountManagerGrain =>
            _client.GetGrain<IAccountManagerGrain>(CurrentUserHelper.UserId);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
        {
            try
            {
                var result = await AccountManagerGrain.Create(model);
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> Update([Required] string id, [FromBody] UpdateUserViewModel model)
        {
            try
            {
                var result = await AccountManagerGrain.Update(model, id);
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}"), ValidateModel]
        public async Task<IActionResult> Delete([Required] string id)
        {
            try
            {
                var result = await AccountManagerGrain.Delete(DeleteUserViewModel.Initialize(id));
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}