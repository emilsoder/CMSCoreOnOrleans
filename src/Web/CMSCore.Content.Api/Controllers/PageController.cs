using System;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Api.Models.Content;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly IClusterClient client;

        public PageController(IClusterClient client)
        {
            this.client = client;
        }

        private IContentGrain _contentGrain => client.GetGrain<IContentGrain>(Guid.NewGuid());

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _contentGrain.Pages();
                return Ok(result.Select(x => x.ViewModel()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{pageId}")]
        public async Task<IActionResult> PageById(string pageId)
        {
            try
            {
                var result = await _contentGrain.PageById(pageId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreatePageViewModel model)
        //{
        //    try
        //    {
        //        var operation = new CreateOperation<Page>(CurrentUserHelper.UserId, model.ToModel());
        //        var result = await _contentGrain.Create(operation);

        //        return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdatePageViewModel model)
        //{
        //    try
        //    {
        //        var operation = new UpdateOperation<Page>(CurrentUserHelper.UserId, model.Id, model.ToModel());
        //        var result = await _contentGrain.Update(operation);

        //        return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var operation = new DeleteOperation<Page>(CurrentUserHelper.UserId, id);
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