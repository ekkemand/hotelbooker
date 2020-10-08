import {IConvenienceGroup} from "./IConvenienceGroup";
import {IConvenience} from "./IConvenience";

export interface IGroupedConvenience {
    convenienceGroup: IConvenienceGroup
    conveniences: IConvenience[]
}
