using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SIVBP_Keširanje.DTOs
{
    public record PaginatedRequestQuery
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 20;
    }
}
