export interface IAPIResponse<T> {
    code: number;
    hasError: boolean;
    wasHandledError?: boolean;
    message: string;

    data?: T;

}