namespace SIVBP_Keširanje.DTOs
{
    public record UserEfficiencyResponse
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int NumAnswers { get; set; }
        public int NumAccepted { get; set; }
        public float AcceptedPercent { get; set; }
    }
}
