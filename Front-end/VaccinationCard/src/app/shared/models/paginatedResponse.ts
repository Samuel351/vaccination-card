export interface PaginatedResponse<T> {
    items: T[];
    pageNumber: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}