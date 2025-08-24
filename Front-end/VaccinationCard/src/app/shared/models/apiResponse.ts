export interface ApiResponse {
    success: boolean;
    message: string;
    statusCode: number;
    timestamp: Date;
    details: string[];
}