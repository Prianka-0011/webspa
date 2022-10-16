export interface Pagination{
    currentPage:number;
    itemsPerPAge;
    totalItems:number;
    totalPages:number;
}
export class PaginatedResult<T>{
result:T;
pagination:Pagination;
}