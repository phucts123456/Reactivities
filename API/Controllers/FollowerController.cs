using Application.Followers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/follow")]
    [ApiController]
    public class FollowerController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowToggle.Command { TargetUsername = username }));


        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username,[FromQuery] string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query { Predicate = predicate, Username = username }));
        }
    }
}
