using DatingApp.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DatingApp.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage
            , int itemPerPage,int totalItems,int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemPerPage, totalItems, totalPages);
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            response.Headers.Add("Pagination",JsonSerializer.Serialize(paginationHeader,option));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
