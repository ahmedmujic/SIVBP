namespace SIVBP_Keširanje.DTOs
{
    public record UserOverviewResponse
    {
        public string DisplayName { get; set; } = null!;
        public string? AboutMe { get; set; }
        public int DownVotes { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }
        public IEnumerable<int> CommentedPosts { get; set; } = Enumerable.Empty<int>();
    }
}
