namespace SIVBP_Keširanje.Constants
{
    public record UserQueries
    {
        public const string UserEfficiency = "SELECT u.Id, u.DisplayName, u.Location, count(*) AS NumAnswers, sum(case when q.AcceptedAnswerId = a.Id then 1 else 0 end) AS NumAccepted,(sum(case when q.AcceptedAnswerId = a.Id then 1 else 0 end)*100.0/count(*)) AS AcceptedPercent FROM Post  a WITH (NOLOCK) INNER JOIN Users  u  WITH (NOLOCK) ON u.Id = a.OwnerUserId INNER JOIN Post  q WITH (NOLOCK) ON a.ParentId = q.Id WHERE (q.OwnerUserId <> u.Id OR q.OwnerUserId IS NULL) GROUP BY u.Id, u.DisplayName, u.Location HAVING count(*) >= 10 ORDER BY AcceptedPercent DESC, NumAnswers DESC";
    }
}
