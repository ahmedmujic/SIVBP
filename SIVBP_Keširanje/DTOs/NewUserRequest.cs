namespace SIVBP_Keširanje.DTOs
{
    public record NewUserRequest
    {
        public string? AboutMe { get; set; }
        public int? Age { get; set; }
        public string DisplayName { get; set; } = null!;
        public int DownVotes { get; set; }
        public string? EmailHash { get; set; }
        public string? Location { get; set; }
        public int Reputation { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }
        public string? WebsiteUrl { get; set; }
        public int? AccountId { get; set; }
    }
}
