export interface PaginatedRequest{
    pageSize: number;
    pageNumber: number;
}


export interface PaginatedList<T>{
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}