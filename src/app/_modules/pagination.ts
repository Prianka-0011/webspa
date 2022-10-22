export interface Pagination{
    currentPage:number;
    itemPerPage:number;
    totalItems:number;
    totalPages:number;
}
export class PaginatedResult<T>{
result:T;
pagination:Pagination;
}
//response, int currntPage
//,int itemPerPage,int totalItems,int totalPages)