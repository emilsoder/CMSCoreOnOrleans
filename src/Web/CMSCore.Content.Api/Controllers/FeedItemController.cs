using System;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/[controller]")]
    public class FeedItemController : Controller
    {
        private readonly IClusterClient client;

        public FeedItemController(IClusterClient client)
        {
            this.client = client;
        }

        private IContentGrain _contentGrain => client.GetGrain<IContentGrain>(Guid.NewGuid());


        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok((await _contentGrain.FeedItems())?.Select(x => x.ViewModel()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{feedItemId}")]
        public async Task<IActionResult> Details(string feedItemId)
        {
            try
            {
                return Ok((await _contentGrain.FeedItemDetails(feedItemId)).ViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFeedItemViewModel model)
        {
            try
            {
                var result = await _contentGrain.Update(new UpdateOperation<FeedItem>(CurrentUserHelper.UserId,
                    model.Id,
                    model.UpdateFeedItem()));

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedItemViewModel model)
        {
            try
            {
                var result =
                    await _contentGrain.Create(new CreateOperation<FeedItem>(CurrentUserHelper.UserId,
                        model.CreateFeedItem()));
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
                var operation = new DeleteOperation<Feed>(CurrentUserHelper.UserId, id);
                var result = await _contentGrain.Delete(operation);

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}