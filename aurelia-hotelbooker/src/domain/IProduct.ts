import {IRoomType} from "./IRoomType";
import {IProductGroup} from "./IProductGroup";

export interface IProduct {
    id: string;
    name: string;
    description: string | null;

    productGroupId: string;
    productGroupName: string;
    productGroupSelection: IProductGroup[];

    roomTypeId: string | null;
    roomTypeName: string | null
    roomTypeSelection: IRoomType[] | null;
}
