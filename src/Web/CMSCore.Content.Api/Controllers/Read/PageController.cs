using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;
 using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/content/page")]
    public class PageController : Controller
    {
        private readonly IClusterClient _client;

        public PageController(IClusterClient client) => _client = client;

        [HttpGet]
        public async Task<IEnumerable<PageTreeViewModel>> List()
        {
            var grain = _client.GetGrain<IContentReaderGrain>(Guid.NewGuid().ToString());
            var result = await grain.PagesToList();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<PageViewModel> Get(string id)
        {
            var grain = _client.GetGrain<IContentReaderGrain>(id);
            var result = await grain.PageById();
            return result;
        }

        [HttpGet("title/{title}")]
        public async Task<PageViewModel> GetByName(string title)
        {
            var grain = _client.GetGrain<IContentReaderGrain>(title);
            var result = await grain.PageByName();
            return result;
        }
    }
}