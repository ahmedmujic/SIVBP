using Microsoft.EntityFrameworkCore;
using SIVBP_Keširanje.Constants;
using SIVBP_Keširanje.DTOs;
using SIVBP_Keširanje.Models;
using StackExchange.Redis;

namespace SIVBP_Keširanje.Repositories
{
    public interface IPostsRepository
    {
        Task<PostOverviewResponse?> GetPostWithCommentsByPostIdAsync(int postId, CancellationToken cancellationToken = default);
        Task MarkUserAnswerAsAccepted(AcceptedAnswerMarkRequest request, CancellationToken cancellationToken = default);
    }

    public class PostsRepository : IPostsRepository
    {
        private readonly SIVBPContext _context;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public PostsRepository(SIVBPContext context, IConnectionMultiplexer connectionMultiplexer)
        {
            _context = context;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<PostOverviewResponse?> GetPostWithCommentsByPostIdAsync(int postId, CancellationToken cancellationToken = default)
        {
            var post = await _context
                .Posts
                .OrderByDescending(post => post.AcceptedAnswerId)
                .Select(post => 
                    new {
                        Id = post.Id,
                        Body = post.Body,
                        Title = post.Title,
                        AcceptedAnswerId = post.AcceptedAnswerId
                    })
                .FirstOrDefaultAsync(post => post.Id == postId, cancellationToken);

            if (post is not null)
            {
                var comments = await _context
                .Comments
                .Include(comments => comments.User)
                .OrderByDescending(comment => comment.CreationDate)
                .Where(comment => comment.PostId == postId)
                .Select(comment => new PostOverviewComment
                {
                    Text = comment.Text,
                    UserId = comment.UserId,
                    UserDisplayName = comment.User.DisplayName,
                    CommentId = comment.Id
                })
                .Skip(0)
                .Take(50)
                .ToListAsync(cancellationToken);

                return new PostOverviewResponse
                {
                    PostText = post?.Body,
                    PostTitle = post?.Title,
                    Comments = comments,
                    AcceptedAnswerId = post.AcceptedAnswerId
                };
            }

            return null;
        }

        public async Task MarkUserAnswerAsAccepted(AcceptedAnswerMarkRequest request, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == request.PostId, cancellationToken);
            
            if(post is not null)
            {
                post.AcceptedAnswerId = request.CommentId;
                await _context.SaveChangesAsync(cancellationToken);
                await _connectionMultiplexer.GetSubscriber().PublishAsync(RedisPubSubChannels.CommentAccepted, "");
            }
        }
    }
}
