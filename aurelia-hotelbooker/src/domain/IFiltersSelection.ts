import {IOwnerCompany} from "./IOwnerCompany";
import {IConvenience} from "./IConvenience";
import {IReviewCategory} from "./IReviewCategory";

export interface IFiltersSelection {
    ownerCompanySelection: IOwnerCompany[];
    convenienceSelection: IConvenience[];
    reviewCategorySelection: IReviewCategory[];
}
