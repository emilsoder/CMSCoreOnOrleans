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
    [Route(ManageConst.BaseRoute + "/page")]
    public class PageManagerController : Controller
    {
        private readonly IClusterClient _client;

        public PageManagerController(IClusterClient client) => this._client = client;

        [HttpPost, ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreatePageViewModel model)
        {
            try
            {
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);
                var result = await grain.Create(model);

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> Update([Required]string id, [FromBody] UpdatePageViewModel model)
        {
            try
            {
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);
                var result = await grain.Update(model, id);

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}"), ValidateModel]
        public async Task<IActionResult> Delete([Required]string id)
        {
            try
            {
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);
                var result = await grain.Delete(DeletePageViewModel.Initialize(id));

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}