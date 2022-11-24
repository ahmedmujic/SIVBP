using Microsoft.AspNetCore.Mvc;
using SIVBP_Keširanje.DTOs;
using SIVBP_Keširanje.Repositories;

namespace SIVBP_Keširanje.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetPostWithComments(int postId, CancellationToken cancellationToken)
        {
            return Ok(await _postsRepository.GetPostWithCommentsByPostIdAsync(postId, cancellationToken));
        }

        [HttpPost("accept-answer")]
        public async Task<IActionResult> MarkCommentAsAnswerAsync([FromBody] AcceptedAnswerMarkRequest request, CancellationToken cancellationToken)
        {
            await _postsRepository.MarkUserAnswerAsAccepted(request, cancellationToken);
            return NoContent();
        }
    }
}
