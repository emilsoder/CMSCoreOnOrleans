using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/content/feeditems")]
    public class FeedItemController : Controller
    {
        private readonly IClusterClient _client;

        public FeedItemController(IClusterClient client) => _client = client;

        [HttpGet("{feedId}")]
        public async Task<IEnumerable<FeedItemPreviewViewModel>> List(string feedId)
        {
            var grain = _client.GetGrain<IContentReaderGrain>(feedId);
            var result = await grain.FeedItemsByFeedId();
            return result;
        }

        [HttpGet("details/{feedItemId}")]
        public async Task<FeedItemViewModel> Details(string feedItemId)
        {
            var grain = _client.GetGrain<IContentReaderGrain>(feedItemId);
            var result = await grain.FeedItemById();
            return result;
        }

        [HttpPost("comment/{feedItemId}")]
        public async Task<IActionResult> AddComment(string feedItemId, [FromBody] CommentViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                var grain = _client.GetGrain<IContentReaderGrain>(feedItemId);
                var result = await grain.CreateComment(viewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}