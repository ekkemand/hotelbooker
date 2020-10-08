import {IReviewCategory} from "./IReviewCategory";

export interface IReview {
    id: string;
    heading: string;
    content: string;
    newCategoryString: string | null;

    reviewCategoryId: string;
    reviewCategory: IReviewCategory;

    roomTypeId: string;
    roomTypeName: string;

    hotelId: string;
    userId: string;
    userDisplayName: string;
}
