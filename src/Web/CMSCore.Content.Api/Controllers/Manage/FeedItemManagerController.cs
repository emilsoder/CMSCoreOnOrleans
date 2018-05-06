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
    [Route(ManageConst.BaseRoute + "/feeditem")]
    public class FeedItemManagerController : Controller
    {
        private readonly IClusterClient _client;

        public FeedItemManagerController(IClusterClient client)
        {
            _client = client;
        }

        [HttpPost, ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateFeedItemViewModel model)
        {
            try
            {
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);
                var result = await grain.Create(model);

                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> Update(
            [Required] string id,
            [FromBody] UpdateFeedItemViewModel viewModel)
        {
            try
            {
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);
                var result = await grain.Update(viewModel, id);

                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
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
                var grain = _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);

                var result = await grain.Delete(DeleteFeedItemViewModel.Initialize(id));

                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}