export interface IQueryParams {
    // indexer - allows to access unknown properties with foo['bar'].
    [param: string]: any;
}
