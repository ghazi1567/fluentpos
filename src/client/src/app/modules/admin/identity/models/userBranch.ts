export interface UserBranch {
    userBranchs: UserBranchModel[];
}
export interface UserBranchModel {
    userId: string;
    active: boolean;
    selected: boolean;
    organizationId: string;
    branchId: string;
    branchName: string;
}
