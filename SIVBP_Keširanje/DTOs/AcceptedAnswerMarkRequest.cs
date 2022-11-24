namespace SIVBP_Keširanje.DTOs
{
    public record AcceptedAnswerMarkRequest
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
    }
}
