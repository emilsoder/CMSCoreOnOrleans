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
    public class BlogPostController : Controller
    {
        private readonly IClusterClient client;

        public BlogPostController(IClusterClient client)
        {
            this.client = client;
        }

        private IContentGrain _contentGrain => client.GetGrain<IContentGrain>(Guid.NewGuid());

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _contentGrain.BlogPosts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> ByBlogId(string blogId)
        {
            try
            {
                var result = await _contentGrain.BlogPosts(blogId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Find(string id)
        {
            try
            {
                var result = await _contentGrain.BlogPostDetails(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogPostViewModel model)
        {
            try
            {
                var operation = new CreateOperation<BlogPost>(CurrentUserHelper.UserId, model.ToModel());
                var result = await _contentGrain.Create(operation);

                return result.Succeeded ? (IActionResult) Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBlogPostViewModel model)
        {
            try
            {
                var operation = new UpdateOperation<BlogPost>(CurrentUserHelper.UserId, model.Id, model.ToModel());
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
                var operation = new DeleteOperation<BlogPost>(CurrentUserHelper.UserId, id);
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