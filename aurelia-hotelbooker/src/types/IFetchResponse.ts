export interface IFetchResponse<TData> {
    statusCode: number;
    errorMessage?: string; // can be undefined
    data?: TData
}
