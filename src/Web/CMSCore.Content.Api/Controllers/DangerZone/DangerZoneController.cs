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
    [Route(ManageConst.BaseRoute + "/dangerzone")]
    public class DangerZoneController : Controller
    {
        private readonly IClusterClient _client;

        public DangerZoneController(IClusterClient client) => _client = client;

        private IContentManagerGrain _contentManager =>
            _client.GetGrain<IContentManagerGrain>(CurrentUserHelper.UserId);

        [HttpDelete("feed/{id}"), ValidateModel]
        public async Task<IActionResult> DeleteFeed([Required]string id)
        {
            try
            {
                var result = await _contentManager.ConfirmDelete(DeleteFeedViewModel.Initialize(id));
                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("feeditem/{id}"), ValidateModel]
        public async Task<IActionResult> DeleteFeedItem([Required]string id)
        {
            try
            {
                var result = await _contentManager.ConfirmDelete(DeleteFeedItemViewModel.Initialize(id));
                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("page/{id}"), ValidateModel]
        public async Task<IActionResult> DeletePage([Required]string id)
        {
            try
            {
                var result = await _contentManager.ConfirmDelete(DeletePageViewModel.Initialize(id));
                return result.Succeeded ? (IActionResult)Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}