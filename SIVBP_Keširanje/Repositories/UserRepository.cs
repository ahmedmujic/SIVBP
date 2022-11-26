using Dapper;
using Microsoft.EntityFrameworkCore;
using SIVBP_Keširanje.Constants;
using SIVBP_Keširanje.DTOs;
using SIVBP_Keširanje.Models;
using SIVBP_Keširanje.Services;

namespace SIVBP_Keširanje.Repositories
{
    public interface IUserRepository
    {
        Task<PaginatedList<UserOverviewResponse>> GetUserListAsync(PaginatedRequestQuery paginatedRequestQuery, CancellationToken cancellationToken);
        Task<IEnumerable<UserEfficiencyResponse>> GetUserEfficiencyAsync(CancellationToken cancellationToken);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SIVBPContext _context;
        private readonly ICacheRepository _cacheRepository;

        public UserRepository(SIVBPContext context, ICacheRepository cacheRepository)
        {
            _context = context;
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<UserEfficiencyResponse>> GetUserEfficiencyAsync(CancellationToken cancellationToken)
        {
            var result = await _cacheRepository.GetByKeyAsync<IEnumerable<UserEfficiencyResponse>>(RedisKeys.UserEfficiency, cancellationToken);

            if(result is null)
            {
                var dbConntection = _context.Database.GetDbConnection();
                result = await dbConntection.QueryAsync<UserEfficiencyResponse>(UserQueries.UserEfficiency, commandTimeout: 540);
                await _cacheRepository.SetByKeyAsync(RedisKeys.UserEfficiency, result, cancellationToken);
            }

            return result;
        }

        public async Task<PaginatedList<UserOverviewResponse>> GetUserListAsync(PaginatedRequestQuery paginatedRequestQuery, CancellationToken cancellationToken)
        {
            var usersQuery = _context
                .Users
                .Include(user => user.Comments)
                .OrderBy(user => user.CreationDate)
                .Where(user => !string.IsNullOrEmpty(user.AboutMe))
                .Select(user => new UserOverviewResponse
                {
                    AboutMe = user.AboutMe,
                    DisplayName = user.DisplayName,
                    DownVotes = user.DownVotes,
                    UpVotes = user.UpVotes,
                    Views = user.Views,
                    CommentedPosts = user.Comments.Select(c => c.PostId).ToList()
                });

            return await PaginatedList<UserOverviewResponse>.CreateAsync(usersQuery, paginatedRequestQuery.PageNumber, paginatedRequestQuery.PageSize, cancellationToken);
        }
    }
}
