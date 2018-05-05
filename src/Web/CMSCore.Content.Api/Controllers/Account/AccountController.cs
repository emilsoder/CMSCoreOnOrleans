using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Api.Extensions;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;
  using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace CMSCore.Content.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IClusterClient _client;

        public AccountController(IClusterClient client) => _client = client;

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var grain = _client.GetGrain<IAccountReaderGrain>(Guid.NewGuid().ToString());
            return await grain.UsersToList();
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> Find(string id)
        {
            var grain = _client.GetGrain<IAccountReaderGrain>(id);
            return await grain.UserById();
        }
    }
}