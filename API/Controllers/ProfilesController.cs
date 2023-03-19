using Application.Photos;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfiles(string username) 
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));

        }
        [HttpPut]
        public async Task<IActionResult> UpdateProflie([FromBody] UpdateProfile profile)
        {          
            return HandleResult(await Mediator.Send(new Edit.Command { Profile = profile }));
        }
        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username,
            string predicate)
        {
            return HandleResult(await Mediator.Send(new ListActivities.Query 
            { Username = username, Predicate = predicate }));
        }
    }
}
