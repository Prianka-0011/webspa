using DatingApp.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DatingApp.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, int currntPage
            ,int itemPerPage,int totalItems,int totalPages)
        {
            var paginationHeader = new PaginationHeader(currntPage, itemPerPage, totalItems, totalPages);
            response.Headers.Add("Pagination",JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
