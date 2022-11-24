using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIVBP_Keširanje.DTOs;
using SIVBP_Keširanje.Models;
using SIVBP_Keširanje.Repositories;

namespace SIVBP_Keširanje.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PaginatedRequestQuery requestQuery, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserListAsync(requestQuery, cancellationToken);
            return Ok(result);
        }

        [HttpGet("efficiency")]
        public async Task<IActionResult> GetUserEfficiency(CancellationToken cancellationToken)
        {
            return Ok(await _userRepository.GetUserEfficiencyAsync(cancellationToken));
        }

    }
}
