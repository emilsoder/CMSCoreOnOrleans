using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CMSCore.Content.Api.Attributes;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers.Manage
{
    [Route(ManageConst.BaseRoute + "/feed")]
    public class FeedManagerController : Controller
    {
        private readonly IClusterClient _client;

        public FeedManagerController(IClusterClient client) => _client = client;

        private IContentManagerGrain _contentManager =>
            _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> Update([Required]string id, [FromBody, Required] UpdateFeedViewModel model)
        {
            try
            {
                var result = await _contentManager.Update(model, id);
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
                var result = await _contentManager.Delete(DeleteFeedViewModel.Initialize(id));
                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}