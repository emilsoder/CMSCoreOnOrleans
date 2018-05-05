using System;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/content/feeds")]
    public class FeedsController : Controller
    {
        private readonly IClusterClient _client;

        public FeedsController(IClusterClient client) => this._client = client;

        [HttpGet]
        public async Task<IActionResult> List()
        {
            JsonResult jsonResult = null;
            try
            {
                var grain = _client.GetGrain<IContentReaderGrain>(Guid.NewGuid().ToString());
                var result = await grain.FeedsToList();
                jsonResult = Json(result);
                return jsonResult;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}