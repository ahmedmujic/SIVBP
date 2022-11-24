namespace SIVBP_Keširanje.DTOs
{
    public record PostOverviewComment
    {
        public string? Text { get; set; }
        public int? UserId { get; set; }
        public string? UserDisplayName { get; set; }
        public int CommentId { get; set; }
    }

    public record PostOverviewResponse
    {
        public PostOverviewResponse()
        {
            Comments = new HashSet<PostOverviewComment>();
        }

        public string? PostText { get; set; }
        public string? PostTitle { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public IEnumerable<PostOverviewComment> Comments { get; set; }
    }
}
