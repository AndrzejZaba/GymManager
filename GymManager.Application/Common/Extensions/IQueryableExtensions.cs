

using GymManager.Application.Common.Models;

namespace GymManager.Application.Common.Extensions;
public static class IQueryableExtensions
{
    public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
        this IQueryable<T> queryable,
        int pageNumber,
        int pageSize)
        => await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);


}

