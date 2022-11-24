using Microsoft.EntityFrameworkCore;

namespace SIVBP_Keširanje.DTOs
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            ValidateCreatePagingInput(pageNumber, pageSize);

            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            ValidateCreatePagingInput(pageNumber, pageSize);

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        private static void ValidateCreatePagingInput(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }
        }
    }
}
