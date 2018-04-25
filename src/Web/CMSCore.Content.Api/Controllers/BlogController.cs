using System;
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
    public class BlogController : Controller
    {
        private readonly IClusterClient client;

        public BlogController(IClusterClient client)
        {
            this.client = client;
        }

        private IContentGrain _contentGrain => client.GetGrain<IContentGrain>(Guid.NewGuid());

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _contentGrain.Blogs();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBlogViewModel model)
        {
            try
            {
                var operation = new UpdateOperation<Blog>(CurrentUserHelper.UserId, model.Id, model.ToModel());
                var result = await _contentGrain.Update(operation);

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
                var operation = new DeleteOperation<Blog>(CurrentUserHelper.UserId, id);
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